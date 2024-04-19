using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PracticeReactApp.Server.Models;

namespace PracticeReactApp.Server.Infrastructures.Extensions
{
    public static class DataTableExtension
    {
        public static DataTableResponseModel<TModel> ToDataTablesResponse<TModel>(this DbSet<TModel> entity, DataTableRequestModel dataTableReq) where TModel : class
        {
            return new DataTableResponseModel<TModel>(entity, dataTableReq);
        }

        public static DataTableResponseModel<TModel> ToDataTablesResponse<TModel>(this IQueryable<TModel> queryable, DataTableRequestModel dataTableReq) where TModel : class
        {
            return new DataTableResponseModel<TModel>(queryable, dataTableReq);
        }

        public static DataTableResponseModel<TModel> ToDataTablesResponse<TModel>(this DbSet<TModel> entity, DataTableRequestModel dataTableReq, Expression<Func<TModel, bool>> where) where TModel : class
        {
            return new DataTableResponseModel<TModel>(entity, dataTableReq, where);
        }

        public static DataTableResponseModel<TModel> ToDataTablesResponse<TModel>(this IQueryable<TModel> queryable, DataTableRequestModel dataTableReq, Expression<Func<TModel, bool>> where) where TModel : class
        {
            return new DataTableResponseModel<TModel>(queryable, dataTableReq, where);
        }

        public static DataTableResponseModel<TModel> CreateDataTablesResponse<TModel>(this DbSet<TModel> entity, DataTableRequestModel dataTableReq, string? where = null, object[]? whereParams = null) where TModel : class
        {
            return new DataTableResponseModel<TModel>(entity, dataTableReq, where, whereParams);
        }

        public static DataTableResponseModel<TModel> CreateDataTablesResponse<TModel>(this IQueryable<TModel> queryable, DataTableRequestModel dataTableReq, string? where = null, object[]? whereParams = null) where TModel : class
        {
            return new DataTableResponseModel<TModel>(queryable, dataTableReq, where, whereParams);
        }

        public static DataTableResponseModel<TModel> ToDataTablesResponse<TModel>(this DbSet<TModel> entity, DataTableRequestModel dataTableReq, IQueryable<TModel> queryFilter) where TModel : class
        {
            return new DataTableResponseModel<TModel>(entity, dataTableReq, queryFilter);
        }

        public static DataTableResponseModel<TModel> ToDataTablesResponse<TModel>(this IQueryable<TModel> queryable, DataTableRequestModel dataTableReq, IQueryable<TModel> queryFilter) where TModel : class
        {
            return new DataTableResponseModel<TModel>(queryable, dataTableReq, queryFilter);
        }
    }
}