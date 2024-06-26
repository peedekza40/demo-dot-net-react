using Newtonsoft.Json;

namespace PracticeReactApp.Core.Models
{
    public class DataTableRequestModel
    {
        [JsonProperty(PropertyName = "columns")]
        public List<DataTableColumnModel> Columns { get; set; } = new List<DataTableColumnModel>();

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "rowsPerPage")]
        public int RowsPerPage { get; set; }

        [JsonProperty(PropertyName = "searchText")]
        public string? SearchText { get; set; }

        [JsonProperty(PropertyName = "sortOrder")]
        public DataTableSortOrderModel? SortOrder { get; set; }
    }
}