using Newtonsoft.Json;
using PracticeReactApp.Core.Extensions;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace PracticeReactApp.Core.Models
{
    public class DataTableResponseModel<T>
    {
        [JsonProperty(PropertyName = "data")]
        public List<T> Data { get; set; } = new List<T>();

        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; } = 0;

        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; } = 0;

        [JsonProperty(PropertyName = "rowsPerPage")]
        public int RowsPerPage { get; set; } = 0;

        [JsonProperty(PropertyName = "sortOrder")]
        public DataTableSortOrderModel? SortOrder { get; set; }

        public void FilterAndOrderBy(IQueryable<T> queryable, DataTableRequestModel request)
        {
            Count = queryable.Count();
            Page = request.Page;
            RowsPerPage = request.RowsPerPage;
            SortOrder = request.SortOrder;

            if (string.IsNullOrEmpty(request.SearchText) == false)
            {
                var whereClauses = new List<string>();
                var columns = request.Columns.Where(x => x.Searchable == true);
                foreach (var column in columns)
                {
                    whereClauses.Add($"{column.Name}.ToLower().Contains(@0)");
                }

                string combinedWhereClause = string.Join(" || ", whereClauses);
                queryable = queryable.Where(combinedWhereClause, request.SearchText.ToLower());
            }

            if (string.IsNullOrEmpty(request.SortOrder?.Direction) == false && string.IsNullOrEmpty(request.SortOrder?.Name) == false)
            {
                var direction = request.SortOrder.Direction.ToLower() == "asc" ? IQueryableExtensions.Order.Asc : IQueryableExtensions.Order.Desc;
                queryable = queryable.OrderByDynamic(request.SortOrder.Name, direction);
            }

            var start = request.Page * RowsPerPage;
            Data = queryable.Skip(start).Take(request.RowsPerPage).ToList();
        }

        public DataTableResponseModel(IQueryable<T> queryable, DataTableRequestModel request)
        {
            FilterAndOrderBy(queryable, request);
        }

        public DataTableResponseModel(IQueryable<T> queryable, DataTableRequestModel request, string? where = null, object[]? whereParams = null)
        {
            if (!string.IsNullOrEmpty(where))
                queryable = queryable.Where(where, whereParams ?? []);

            FilterAndOrderBy(queryable, request);
        }

        public DataTableResponseModel(IQueryable<T> queryable, DataTableRequestModel request, Expression<Func<T, bool>>? where = null)
        {
            if (where != null)
                queryable = queryable.Where(where);

            FilterAndOrderBy(queryable, request);
        }

        public DataTableResponseModel(IQueryable<T> queryable, DataTableRequestModel request, IQueryable<T> queryFilter)
        {
            FilterAndOrderBy(queryFilter, request);
        }
    }
}