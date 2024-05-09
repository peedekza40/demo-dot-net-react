/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable @typescript-eslint/no-explicit-any */
import { useState } from 'react';
import { UseFormRegister, FieldErrors } from 'react-hook-form';
import { useRouter } from 'src/routes/hooks';
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
    const router = useRouter();

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
    const [formRoleActionMode, setFormRoleActionMode] = useState<ActionMode>(ActionMode.Add);
    const [formRoleData, setFormRoleData] = useState<RoleForm | null>(null);

    //set action mode list
    const actionModes: IEnumItem[] = getEnumList(ActionMode);

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
            name: 'Id',
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

    const search = (tableState: object) => {
        setDataTableOption({ ...dataTableOption, isLoading: true });

        searchApi(tableState,
            (response: any) => {
                if (response.data.isSuccess) {
                    const data = JSON.parse(response.data.data);
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
        onTableInit: (action: string, tableState: object) => {
            search(tableState);
        },
        onTableChange: (action: string, tableState: object) => {
            if (action == DataTableActionType.change
                || action == DataTableActionType.changePage
                || action == DataTableActionType.changeRowsPerPage
                || action == DataTableActionType.filterChange
                || action == DataTableActionType.search) {
                search(tableState);
            }
        }
    };

    //handle form section
    const handleOpenFormRole = (actionMode: ActionMode, roleId: string | null) => {
        setFormRoleActionMode(actionMode);

        if (actionMode == ActionMode.Edit) {
            getByIdApi(roleId ?? "",
                (response: any) => {
                    if (response.data.isSuccess) {
                        const roleData: RoleForm = plainToClass(RoleForm, JSON.parse(response.data.data));
                        const tempRoleData = formRoleData ?? new RoleForm();
                        tempRoleData.id = roleData.id;
                        tempRoleData.name = roleData.name;

                        setFormRoleData(tempRoleData);
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
            setFormRoleData(null);
            setFormRoleIsOpen(true);
        }
    };

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
                    value={formRoleData?.id}
                    InputProps={{
                        readOnly: formRoleActionMode == ActionMode.Edit
                    }}
                />
                <TextField
                    size="small"
                    label="Name *"
                    {...register('name')}
                    name="name"
                    error={!!errors?.name}
                    helperText={errors?.name?.message}
                    value={formRoleData?.name}
                />
                <TextField
                    select
                    type="number"
                    size="small"
                    {...register('mode', { valueAsNumber: true })}
                    name="mode"
                    error={!!errors?.mode}
                    helperText={errors?.mode?.message}
                    value={formRoleActionMode}
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

    const onSubmit = (
        dataSubmit: RoleForm,
        setIsLoading: (value: boolean) => void,
        setIsHasError: (value: boolean) => void,
        setErrorRerponse: (value: ErrorResponse | null) => void
    ) => {
        setIsLoading(true);
        dataSubmit.mode = formRoleActionMode;
        saveRoleApi(dataSubmit,
            (response: any) => {
                if (response.status == 200) {
                    setIsHasError(false);
                    setErrorRerponse(null);
                    setFormRoleIsOpen(false);
                    successSweetAlert(() => router.reload());
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
                dialogTitle={formRoleActionMode == ActionMode.Add ? "Create Role" : "Edit Role"}
                isOpen={formRoleIsOpen}
                onSubmit={onSubmit}
                onClose={() => setFormRoleIsOpen(false)}
                renderField={renderField}/>
        </Container>
    );
}

export default RoleManagementView;