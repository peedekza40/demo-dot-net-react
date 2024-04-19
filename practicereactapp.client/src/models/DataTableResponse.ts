import DataTableSortOrder from "src/models/DataTableSortOrder";

export default class DataTableResponse {
    public data: any[];
    public count: number;
    public page: number;
    public rowsPerPage: number;
    public sortOrder: DataTableSortOrder;
}