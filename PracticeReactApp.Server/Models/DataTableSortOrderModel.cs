using Newtonsoft.Json;

namespace PracticeReactApp.Server.Models
{
    public class DataTableSortOrderModel
    {
        [JsonProperty(PropertyName = "direction")]
        public string? Direction { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string? Name { get; set; }
    }
}