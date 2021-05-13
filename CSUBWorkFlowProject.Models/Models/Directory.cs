using Newtonsoft.Json;

namespace CSUBWorkFlowProject.Shared.Models
{
    public partial class Directory
    {

        [JsonProperty("l")]
        public string l { get; set; }

        [JsonProperty("f")]
        public string f { get; set; }

        [JsonProperty("m")]
        public string m { get; set; }

        [JsonProperty("e")]
        public string e { get; set; }

        [JsonProperty("t")]
        public string t { get; set; }

        [JsonProperty("b")]
        public string b { get; set; }

        [JsonProperty("r")]
        public string r { get; set; }

        [JsonProperty("d")]
        public string d { get; set; }

        [JsonProperty("o")]
        public string o { get; set; }

        [JsonProperty("u")]
        public string u { get; set; }
    }
}
