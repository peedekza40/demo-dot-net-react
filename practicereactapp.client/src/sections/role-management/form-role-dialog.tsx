/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable @typescript-eslint/no-explicit-any */
import { useState, useEffect } from 'react';
import { useForm, SubmitHandler, FormProvider } from 'react-hook-form';
import Button from '@mui/material/Button';
import LoadingButton from '@mui/lab/LoadingButton';
import TextField from '@mui/material/TextField';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Grid from '@mui/material/Grid';
import Stack from '@mui/material/Stack';
import Box from '@mui/material/Box';

import ErrorAlert from 'src/components/error-alert';

import { zodResolver } from '@hookform/resolvers/zod';
import RoleForm, { roleFormSchema } from "src/models/RoleForm";
import { saveRole as saveRoleApi } from "@/apis/services/RoleManagement";

import ActionMode from 'src/constants/action-mode';
import { ErrorResponse } from "src/utils/global-type";

function FormRoleDialog({ actionMode, isOpen, onClose }: { actionMode: ActionMode, isOpen: boolean, onClose: any }){
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [isHasError, setIsHasError] = useState(false);
    const [errorResponse, setErrorRerponse] = useState<ErrorResponse | null>(null);

    const dialogTitle: string = actionMode == ActionMode.Add ? "Create Role" : "Edit Role";

    //initial handle form
    const methods = useForm<RoleForm>({
        resolver: zodResolver(roleFormSchema),
    });

    const {
        reset,
        handleSubmit,
        register,
        formState: { isSubmitSuccessful, errors },
    } = methods;

    useEffect(() => {
        if (isSubmitSuccessful) {
            // reset();
        }
    }, [isSubmitSuccessful, reset]);

    //handle submit
    const onSubmitHandler: SubmitHandler<RoleForm> = (values: RoleForm) => {
        setIsLoading(true);
        saveRoleApi(values,
            (response: any) => {
                if (response.status == 200) {
                    setIsHasError(false);
                    setErrorRerponse(null);
                    onClose();
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
    };

    //handle close
    const handleClose = () => {
        reset();
        setIsHasError(false);
        setErrorRerponse(null);
        onClose();
    }

    return (
        <Dialog 
            open={isOpen} 
            onClose={handleClose} 
            aria-labelledby="form-dialog-title"
            fullWidth>
            <FormProvider {...methods}>
                <Box
                    component='form'
                    noValidate
                    autoComplete='off'
                    onSubmit={handleSubmit(onSubmitHandler)}
                >
                    <DialogTitle id="form-dialog-title">{dialogTitle}</DialogTitle>
                    <DialogContent>
                        <ErrorAlert 
                            isHasError={isHasError} 
                            errorResponse={errorResponse}/>
                        <Stack spacing={3} sx={{ my: 3 }}>
                            <TextField
                                size="small"
                                label="ID *"
                                {...register('id')}
                                name="id"
                                error={!!errors.id}
                                helperText={errors.id?.message}
                            />
                            <TextField
                                size="small"
                                label="Name *"
                                {...register('name')}
                                name="name"
                                error={!!errors.name}
                                helperText={errors.name?.message}
                            />
                        </Stack>
                    </DialogContent>
                    <DialogActions>
                    <Grid container spacing={2}>
                        <Grid item md={6}>
                            <Button 
                                fullWidth
                                onClick={handleClose} 
                                size="medium"
                                color="error">
                                Cancel
                            </Button>
                        </Grid>
                        <Grid item md={6}>
                            <LoadingButton
                                fullWidth
                                size="medium"
                                type="submit"
                                variant="contained"
                                color="inherit"
                                loading={isLoading}
                            >
                                Save
                            </LoadingButton>
                        </Grid>
                    </Grid>
                    </DialogActions>
                </Box>
            </FormProvider>
        </Dialog>
  );
}

export default FormRoleDialog;