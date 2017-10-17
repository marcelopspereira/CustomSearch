using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using CreateIndex.Model;

namespace CreateIndex
{
    public class CSVHelper
    {
        private string[] _lines;

        public CSVHelper(string pathCSV, bool ignoreHeader = false)
        {
            if (string.IsNullOrEmpty(pathCSV)) throw new ArgumentNullException(nameof(pathCSV));
            if (!File.Exists(pathCSV)) throw new FileNotFoundException();

            _lines = File.ReadAllLines(pathCSV);
            if (ignoreHeader)
                _lines = _lines.Skip(1).ToArray();
        }

        public IEnumerable<Exam> GetExams()
        {
            var exams = from w in _lines
                        select new Exam()
                        {
                            Id = w.Split(';')[0],
                            Acronym = w.Split(';')[1],
                            Name = w.Split(';')[2],
                            KeyWord = w.Split(';')[3],
                            Method = w.Split(';')[4],
                            Complement = w.Split(';')[5],
                            BodyRegion = w.Split(';')[6],
                            Incidence= w.Split(';')[7],
                            Material = w.Split(';')[8],
                        };
            return exams;
        }

        public IEnumerable<Synonym> GetSynonyms()
        {
            var synonyms = from w in _lines
                        select new Synonym()
                        {
                            Id = w.Split(';')[0],
                            Description = w.Split(';')[1]
                        };
            return synonyms;
        }

        public IEnumerable<Dictionary> GetDictionary()
        {
            var dictionary = from w in _lines
                           select new Dictionary()
                           {
                               Term = w.Split(';')[0],
                               Variations= w.Split(';')[1]
                           };
            return dictionary;
        }
    }
}
