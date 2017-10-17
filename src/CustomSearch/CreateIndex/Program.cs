using CreateIndex.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Azure.Search.Models;
using Microsoft.Azure.Search;
using Microsoft.Extensions.Configuration;
using Microsoft.Rest.Azure;

namespace CreateIndex
{
    public class Program
    {

        public static IConfigurationRoot configuration;
        public static string searchServiceName;
        public static string adminApiKey;
        public static string indexName;
        public const string SynonymMapName = "exam-synonym";

        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            configuration = builder.Build();

            searchServiceName = configuration["AzureSearch:ServiceName"];
            adminApiKey = configuration["AzureSearch:ApiKey"];
            indexName = configuration["AzureSearch:IndexName"];

            string[] files = { configuration["AzureSearch:ExamsCSV"], configuration["AzureSearch:SynonymCSV"], configuration["AzureSearch:DictionaryCSV"] };

            Task.Factory.StartNew(() => { MainAsync(files); }).GetAwaiter().GetResult();

        }

        private static void MainAsync(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Invalid call!! ");
                return;
            }

            //Readling Files
            IEnumerable<Exam> exams = GetExams(args[0], true);
            IEnumerable<Synonym> synonyms = GetExamSynonym(args[1], true);
            IEnumerable<Dictionary> dictionary = GetDictionary(args[2], true);

            //Joing Synonym in Exams
            exams = MergeExamsSynonym(exams, synonyms);

            var search = CreateSearchServiceClient(searchServiceName, adminApiKey);

            var synomymMap = CreateSynonym(search, dictionary, SynonymMapName);

            if (synomymMap == null)
                throw new Exception("Error occurred!!");

            CreateIndex<Exam>(search, indexName, exams);

            string[] fields = { "name", "synonym" };
            EnableSynonymsIndex(search, indexName, fields, SynonymMapName);

            Upload(search, indexName, exams);

        }

        private static void Upload(SearchServiceClient search, string indexName, IEnumerable<Exam> exams)
        {
            ISearchIndexClient index = search.Indexes.GetClient(indexName);

            int size = 100;

            var listExams = exams.ToList();
            var splitExams = new List<List<Exam>>();

            for (int i = 0; i < listExams.Count; i += size)
            {
                splitExams.Add(listExams.GetRange(i, Math.Min(size, listExams.Count - i)));
            }


            foreach(var list in splitExams)
            {
                var batch = IndexBatch.Upload(list);

                try
                {
                    index.Documents.Index(batch);
                }
                catch (IndexBatchException ex)
                {
                    Console.WriteLine("Failed to index some of the documents: {0}",
                        String.Join(", ", ex.IndexingResults.Where(r => !r.Succeeded).Select(r => r.Key)));
                }
            }
            
        }

        private static IEnumerable<Exam> MergeExamsSynonym(IEnumerable<Exam> exams, IEnumerable<Synonym> synonyms)
        {
            var result = new List<Exam>();
            foreach (Exam exam in exams)
            {
                var examSynomym = from s in synonyms where s.Id.Equals(exam.Id) select s;
                var strSynonym = string.Empty;
                try
                {
                    if (examSynomym.Count() > 0)
                    {
                        foreach (Synonym synonym in examSynomym)
                        {
                            strSynonym += $"{synonym.Description},";
                        }

                        exam.Synonym = strSynonym.Substring(0, strSynonym.Length - 1);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                result.Add(exam);
            }

            return result;

        }

        private static IEnumerable<Dictionary> GetDictionary(string filePath, bool ignoreHeaders)
        {

            CSVHelper dictionarySynonym = new CSVHelper(filePath, ignoreHeaders);
            IEnumerable<Dictionary> dictionary = null;
            try
            {
                dictionary = dictionarySynonym.GetDictionary();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occurred processing synonym.csv file: " + ex.Message);
                Console.WriteLine("Changes are that the file doesn't have the right columns formation, please check the Synonym template file and try again!");
            }

            return dictionary;
        }

        private static IEnumerable<Synonym> GetExamSynonym(string filePath, bool ignoreHeaders)
        {
            CSVHelper helperSynonym = new CSVHelper(filePath, ignoreHeaders);
            IEnumerable<Synonym> synonyms = null;
            try
            {
                synonyms = helperSynonym.GetSynonyms();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occurred processing synonym.csv file: " + ex.Message);
                Console.WriteLine("Changes are that the file doesn't have the right columns formation, please check the Synonym template file and try again!");
            }

            return synonyms;
        }

        private static IEnumerable<Exam> GetExams(string filePath, bool ignoreHeaders)
        {
            CSVHelper helperExams = new CSVHelper(filePath, ignoreHeaders);

            IEnumerable<Exam> exams = null;

            try
            {
                exams = helperExams.GetExams();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error Occurred processing Exam.csv file: " + ex.Message);
                Console.WriteLine("Changes are that the file doesn't have the right columns formation, please check the exam template file and try again!");
            }

            return exams;

        }

        private static SearchServiceClient CreateSearchServiceClient(string searchServiceName, string searchAdminKey)
        {
            SearchServiceClient serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(searchAdminKey));
            return serviceClient;
        }

        private static void CreateIndex<T>(SearchServiceClient serviceClient, string indexName, IEnumerable<Exam> exams)
        {

            if (serviceClient.Indexes.Exists(indexName))
            {
                serviceClient.Indexes.Delete(indexName);
            }


            var definition = new Index()
            {
                Name = indexName,
                Fields = FieldBuilder.BuildForType<T>()
            };

            serviceClient.Indexes.Create(definition);

        }

        private static SynonymMap CreateSynonym(SearchServiceClient serviceClient, IEnumerable<Dictionary> dictionary, string synonymMapName)
        {
            string strSynonym = string.Empty;

            foreach (var d in dictionary)
            {
                strSynonym += $"{ d.Variations}=>{d.Term}\n";
            }

            if (String.IsNullOrEmpty(strSynonym))
                throw new Exception("strSynonym is empty!");

            var synonymMap = new SynonymMap
            {
                Name = synonymMapName,
                Format = "solr",
                Synonyms = strSynonym
            };

            try
            {
                return serviceClient.SynonymMaps.CreateOrUpdate(synonymMap);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return null;

        }

        private static async void EnableSynonymsIndex(SearchServiceClient serviceClient, string indexName, string[] fields, string synonymMapName)
        {
            int MaxNumTries = 3;

            for (int i = 0; i < MaxNumTries; ++i)
            {
                try
                {
                    Index index = serviceClient.Indexes.Get(indexName);
                    if (index != null)
                        index = AddSynonymMapsToFields(fields, index, synonymMapName);

                    //await serviceClient.Indexes.CreateOrUpdateAsync(index, accessCondition: AccessCondition.IfNotChanged(index));
                    serviceClient.Indexes.CreateOrUpdate(index, accessCondition: AccessCondition.IfNotChanged(index));

                    Console.WriteLine("Updated the index successfully.\n");
                    break;
                }
                catch (CloudException e) when (e.IsAccessConditionFailed())
                {
                    Console.WriteLine($"Index update failed : {e.Message}. Attempt({i}/{MaxNumTries}).\n");
                }
            }
        }

        private static Index AddSynonymMapsToFields(string[] fields, Index index, string synonymMapName)
        {

            foreach (string name in fields)
            {
                index.Fields.First(f => f.Name.Equals(name)).SynonymMaps = new[] { synonymMapName };
            }
            return index;

        }
    }
}
