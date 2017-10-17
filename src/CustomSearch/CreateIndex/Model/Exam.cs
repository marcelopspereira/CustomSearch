using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CreateIndex.Model
{
    public class Exam
    {
        [Key]
        [IsFilterable]
        [JsonProperty("id")]
        public string Id { get; set; }

        [IsSearchable, IsSortable]
        [JsonProperty("acronym")]

        public string Acronym { get; set; }

        [IsSearchable, IsSortable]
        [Analyzer(AnalyzerName.AsString.PtBrMicrosoft)]
        [JsonProperty("name")]

        public String Name { get; set; }

        [IsSearchable, IsSortable]
        [Analyzer(AnalyzerName.AsString.PtBrMicrosoft)]
        [JsonProperty("synonym")]

        public String Synonym { get; set; }

        [IsSearchable, IsSortable]
        [JsonProperty("keyword")]

        public String KeyWord { get; set; }

        [IsFilterable]
        [JsonProperty("method")]

        public String Method { get; set; }

        [IsSearchable]
        [JsonProperty("complement")]

        public String Complement { get; set; }

        [IsFilterable]
        [JsonProperty("bodyregion")]

        public String BodyRegion { get; set; }

        [IsFilterable]
        [JsonProperty("incidence")]

        public String Incidence { get; set; }

        [IsFilterable]
        [JsonProperty("material")]

        public String Material { get; set; }


        public override string ToString()
        {
            var builder = new StringBuilder();

            builder.AppendFormat("ID: {0}\t", Id.ToString());

            if (!String.IsNullOrEmpty(Acronym))
            {
                builder.AppendFormat("Acronym: {0}\t", Acronym);
            }

            if (!String.IsNullOrEmpty(Name))
            {
                builder.AppendFormat("Name: {0}\t", Name);
            }

            if (!String.IsNullOrEmpty(Synonym))
            {
                builder.AppendFormat("Synonym: {0}\t", Synonym);
            }

            if (!String.IsNullOrEmpty(KeyWord))
            {
                builder.AppendFormat("KeyWord: {0}\t", KeyWord);
            }

            if (!String.IsNullOrEmpty(Method))
            {
                builder.AppendFormat("Method: {0}\t", Method);
            }

            if (!String.IsNullOrEmpty(Complement))
            {
                builder.AppendFormat("Complement: {0}\t", Complement);
            }

            if (!String.IsNullOrEmpty(BodyRegion))
            {
                builder.AppendFormat("BodyRegion: {0}\t", BodyRegion);
            }

            if (!String.IsNullOrEmpty(Incidence))
            {
                builder.AppendFormat("Incidence: {0}\t", Incidence);
            }

            if (!String.IsNullOrEmpty(Material))
            {
                builder.AppendFormat("Material: {0}\t", Material);
            }

            return builder.ToString();
        }

    }
}
