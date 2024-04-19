using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PracticeReactApp.Server.Models
{
    public class DataTableColumnModel
    {
        [JsonProperty(PropertyName = "filter")]
        public bool? Filter { get; set; }

        [JsonProperty(PropertyName = "label")]
        public string? Label { get; set; }
        
        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }

        [JsonProperty(PropertyName = "searchable")]
        public bool? Searchable { get; set; }
    }
}