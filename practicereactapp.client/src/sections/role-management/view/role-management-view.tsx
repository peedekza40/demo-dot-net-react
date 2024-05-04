/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable @typescript-eslint/no-explicit-any */
import { useState } from 'react';
import Card from '@mui/material/Card';
import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';
import CircularProgress from '@mui/material/CircularProgress';
import MUIDataTable from "mui-datatables/dist";

import Iconify from 'src/components/iconify';

import ActionMode from 'src/constants/action-mode';
import DataTableActionType from 'src/constants/data-table-action-type';
import FormRoleDialog from 'src/sections/role-management/form-role-dialog';
import { search as searchApi } from "@/apis/services/RoleManagement";

type DataTableOption = {
  page: number;
  count: number; 
  isLoading: boolean; 
  rowsPerPage: number; 
  sortOrder: any
}

function RoleManagementView() {

  const [dataTableOption, setDataTableOption] = useState<DataTableOption>({
    page: 0,
    count: 0,
    isLoading: false, 
    rowsPerPage: 0, 
    sortOrder: {}
  });

  const [data, setData] = useState([]);
  const [formRoleIsOpen, setFormRoleIsOpen] = useState<boolean>(false);
  const [formRoleActionMode, setFormRoleActionMode] = useState<ActionMode>(ActionMode.Add);

  const handleOpenFormRole = (actionMode: ActionMode) => {
    setFormRoleIsOpen(true);
    setFormRoleActionMode(actionMode);
  };

  const columns = [
    {
      name: 'Id',
      label: 'ID',
      options: {},
    },
    {
      name: 'Name',
      label: 'Name',
      options: {},
    },
    {
      name: 'NormalizedName',
      label: 'Normalized Name',
      options: {},
    },
    {
      name: 'Action',
      label: 'Action',
      options: {
        searchable: false,
        customBodyRender: (value: any, tableMeta: any, updateValue: any) => (
          <Button 
            variant="contained" 
            color="inherit" 
            startIcon={<Iconify icon="lucide:edit" />}
            onClick={() => handleOpenFormRole(ActionMode.Edit)}>
            Edit
          </Button>
        )
      },
    },
  ];

  const search = (tableState: object) => {
    setDataTableOption({...dataTableOption, isLoading: true});

    searchApi(tableState,
      (response: any) => {
        if(response.data){
          setData(response.data.data);
          setDataTableOption({
            page: response.data.page,
            count: response.data.count,
            isLoading: false,
            rowsPerPage: response.data.rowsPerPage,
            sortOrder: response.data.sortOrder
          });
        }
      },
      (error: any) => {
        console.log(error);
      },
      () => {}
    );
  }

  const options = {
    filterType: 'checkbox',
    download: false,
    print: false,
    viewColumns: false,
    caseSensitive: true,
    responsive: "standard",
    searchAlwaysOpen: true,
    searchPlaceholder: "Search role...",
    serverSide: true,
    count: dataTableOption?.count,
    onTableInit: (action: string, tableState: object) => {
      search(tableState);
    },
    onTableChange: (action: string, tableState: object) => {
      if(action == DataTableActionType.change 
        || action == DataTableActionType.changePage
        || action == DataTableActionType.changeRowsPerPage
        || action == DataTableActionType.filterChange
        || action == DataTableActionType.search){
        search(tableState);
      }
    }
  };

  return (
    <Container>
      <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
        <Typography variant="h4">Roles</Typography>

        <Button 
          variant="contained" 
          color="inherit" 
          startIcon={<Iconify icon="eva:plus-fill" />}
          onClick={() => handleOpenFormRole(ActionMode.Add)}>
          New Role
        </Button>
      </Stack>

      <Card>
        <MUIDataTable
          sx={{ minWidth: 800 }}
          data={data}
          columns={columns}
          options={options}
          title={
            <Typography variant="h6">
              {(dataTableOption.isLoading) && <CircularProgress size={24} style={{ marginLeft: 15, position: 'relative', top: 4 }} />}
            </Typography>
          }
        />
      </Card>

      <FormRoleDialog
        actionMode={formRoleActionMode}
        isOpen={formRoleIsOpen}
        onClose={() => setFormRoleIsOpen(false)}/>
    </Container>
  );
}

export default RoleManagementView;