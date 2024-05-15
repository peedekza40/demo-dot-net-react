/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable @typescript-eslint/no-explicit-any */
import { useState } from 'react';
import { UseFormRegister, FieldErrors } from 'react-hook-form';
import Card from '@mui/material/Card';
import Button from '@mui/material/Button';
import Container from '@mui/material/Container';
import Typography from '@mui/material/Typography';
import CircularProgress from '@mui/material/CircularProgress';
import Stack from '@mui/material/Stack';
import TextField from '@mui/material/TextField';
import MenuItem from '@mui/material/MenuItem';
import MUIDataTable from "mui-datatables/dist";
import { plainToClass } from "class-transformer"; 

import Iconify from 'src/components/iconify';
import { errorSweetAlert } from 'src/components/error-alert';
import { successSweetAlert } from 'src/components/success-alert';
import { warningSweetAlert } from 'src/components/warning-alert';

import ActionMode from 'src/constants/action-mode';
import DataTableActionType from 'src/constants/data-table-action-type';
import FormDialog from 'src/components/form-dialog';
import { search as searchApi } from "@/apis/services/RoleManagement";
import RoleForm, { roleFormSchema } from 'src/models/RoleForm';

import { saveRole as saveRoleApi } from "@/apis/services/RoleManagement";
import { getById as getByIdApi } from "@/apis/services/RoleManagement";
import { ErrorResponse } from "src/utils/global-type";
import { IEnumItem, getEnumList } from "src/utils/enum-list";

type DataTableOption = {
    page: number;
    count: number;
    isLoading: boolean;
    rowsPerPage: number;
    sortOrder: any
}

function RoleManagementView() {
    //state table
    const [dataTableOption, setDataTableOption] = useState<DataTableOption>({
        page: 0,
        count: 0,
        isLoading: false,
        rowsPerPage: 0,
        sortOrder: {}
    });
    const [data, setData] = useState([]);

    //state form
    const [formRoleIsOpen, setFormRoleIsOpen] = useState<boolean>(false);
    const [formRoleData, setFormRoleData] = useState<RoleForm>(new RoleForm());

    //set action mode list
    const actionModes: IEnumItem[] = getEnumList(ActionMode);

    const columns = [
        {
            name: 'id',
            label: 'ID',
            options: {},
        },
        {
            name: 'name',
            label: 'Name',
            options: {},
        },
        {
            name: 'normalizedName',
            label: 'Normalized Name',
            options: {},
        },
        {
            name: 'id',
            label: 'Action',
            options: {
                searchable: false,
                customBodyRender: (value: any, tableMeta: any, updateValue: any) => (
                    <Button
                        variant="contained"
                        color="inherit"
                        startIcon={<Iconify icon="lucide:edit" />}
                        onClick={() => handleOpenFormRole(ActionMode.Edit, value)}>
                        Edit
                    </Button>
                )
            },
        },
    ];

    const search = (tableState: DataTableOption) => {
        setDataTableOption({ 
            ...dataTableOption, 
            sortOrder: tableState.sortOrder,
            isLoading: true 
        });

        searchApi(tableState,
            (response: any) => {
                if (response.data.isSuccess) {
                    const data = response.data.data;
                    setData(data.data);
                    setDataTableOption({
                        page: data.page,
                        count: data.count,
                        isLoading: false,
                        rowsPerPage: data.rowsPerPage,
                        sortOrder: data.sortOrder
                    });
                }
            },
            (error: any) => {
                console.log(error);
            },
            () => { }
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
        onTableInit: (action: string, tableState: DataTableOption) => {
            search(tableState);
        },
        onTableChange: (action: string, tableState: DataTableOption) => {
            if (action == DataTableActionType.change
                || action == DataTableActionType.changePage
                || action == DataTableActionType.changeRowsPerPage
                || action == DataTableActionType.filterChange
                || action == DataTableActionType.search
                || action == DataTableActionType.sort) {
                search(tableState);
            }
        }
    };

    //handle form section
    const handleOpenFormRole = (actionMode: ActionMode, roleId: string | null) => {
        if (actionMode == ActionMode.Edit) {
            getByIdApi(roleId ?? "",
                (response: any) => {
                    if (response.data.isSuccess) {
                        const roleData: RoleForm = plainToClass(RoleForm, response.data.data);
                        setFormRoleData({
                            ...formRoleData,
                            id: roleData.id,
                            name: roleData.name,
                            mode: roleData.mode
                        });
                        setFormRoleIsOpen(true);
                    }
                    else {
                        warningSweetAlert(response.data.errorMessage);
                    }
                },
                (error: any) => {
                    errorSweetAlert(true, error);
                });
        }
        else {
            setFormRoleData(new RoleForm());
            setFormRoleIsOpen(true);
        }
    };

    const onSubmit = (
        dataSubmit: RoleForm,
        setIsLoading: (value: boolean) => void,
        setIsHasError: (value: boolean) => void,
        setErrorRerponse: (value: ErrorResponse | null) => void
    ) => {
        setIsLoading(true);
        saveRoleApi(formRoleData,
            (response: any) => {
                if (response.status == 200) {
                    setIsHasError(false);
                    setErrorRerponse(null);
                    setFormRoleIsOpen(false);
                    successSweetAlert(() => search(dataTableOption));
                }
            },
            (error: any) => {
                setIsHasError(true);
                if (error.response.status == 400 && error.response.data.errors.length >= 1) {
                    setErrorRerponse(error.response.data);
                }
            },
            () => setIsLoading(false)
        );
    }

    const renderField = (register: UseFormRegister<RoleForm>, errors?: FieldErrors<RoleForm>) => {
        return (
            <Stack spacing={3} sx={{ my: 3 }}>

                <TextField
                    size="small"
                    label="ID *"
                    {...register('id')}
                    name="id"
                    error={!!errors?.id}
                    helperText={errors?.id?.message}
                    value={formRoleData.id}
                    onChange={(event) => setFormRoleData({
                        ...formRoleData,
                        id: event.target.value
                    })}
                    InputProps={{
                        readOnly: formRoleData.mode == ActionMode.Edit
                    }}
                />
                <TextField
                    size="small"
                    label="Name *"
                    {...register('name')}
                    name="name"
                    error={!!errors?.name}
                    helperText={errors?.name?.message}
                    value={formRoleData.name}
                    onChange={(event) => setFormRoleData({
                        ...formRoleData,
                        name: event.target.value
                    })}
                />
                <TextField
                    select
                    type="number"
                    size="small"
                    {...register('mode', { valueAsNumber: true })}
                    name="mode"
                    error={!!errors?.mode}
                    helperText={errors?.mode?.message}
                    value={formRoleData.mode}
                    InputProps={{ readOnly: true }}
                    sx={{ 
                        display: 'none' 
                    }}
                >
                    {actionModes.map((item, index) => (
                        <MenuItem key={index} value={item.value}>
                            {item.key}
                        </MenuItem>
                    ))}
                </TextField>
            </Stack>
        )
    }

    return (
        <Container>
            <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                <Typography variant="h4">Roles</Typography>

                <Button
                    variant="contained"
                    color="inherit"
                    startIcon={<Iconify icon="eva:plus-fill" />}
                    onClick={() => handleOpenFormRole(ActionMode.Add, null)}>
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

            <FormDialog<RoleForm>
                formSchema={roleFormSchema}
                dialogTitle={formRoleData.mode == ActionMode.Add ? "Create Role" : "Edit Role"}
                isOpen={formRoleIsOpen}
                onSubmit={onSubmit}
                onClose={() => setFormRoleIsOpen(false)}
                renderField={renderField}/>
        </Container>
    );
}

export default RoleManagementView;