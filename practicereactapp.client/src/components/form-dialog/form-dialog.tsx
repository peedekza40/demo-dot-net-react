/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable @typescript-eslint/no-explicit-any */
import React, { useState, useEffect } from 'react';
import { useForm, SubmitHandler, FormProvider, FieldValues, UseFormRegister, FieldErrors } from 'react-hook-form';
import Button from '@mui/material/Button';
import LoadingButton from '@mui/lab/LoadingButton';
import Dialog from '@mui/material/Dialog';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import DialogTitle from '@mui/material/DialogTitle';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import { zodResolver } from '@hookform/resolvers/zod';

import ErrorAlert from 'src/components/error-alert';

import { ErrorResponse } from "src/utils/global-type";

function FormDialog<T extends FieldValues>({ formSchema, dialogTitle, isOpen, renderField, onSubmit, onClose }: {
    formSchema: any,
    dialogTitle?: string,
    isOpen: boolean,
    renderField: (register: UseFormRegister<T>, errors?: FieldErrors<T>) => React.ReactNode,
    onSubmit?: (
        dataSubmit: T,
        setIsLoading: (value: boolean) => void,
        setIsHasError: (value: boolean) => void,
        setErrorRerponse: (value: ErrorResponse | null) => void
    ) => void,
    onClose?: () => void
}) {
    const [isLoading, setIsLoading] = useState<boolean>(false);
    const [isHasError, setIsHasError] = useState(false);
    const [errorResponse, setErrorRerponse] = useState<ErrorResponse | null>(null);

    //initial handle form
    const methods = useForm<T>({
        resolver: zodResolver(formSchema),
    });

    const {
        reset,
        handleSubmit,
        register,
        formState: { isSubmitSuccessful, errors },
    } = methods;

    useEffect(() => {
        if(isOpen)
        {
            reset();
        }
    }, [isSubmitSuccessful, reset, isOpen]);

    //handle submit
    const onSubmitHandler: SubmitHandler<T> = (dataSubmit: T) => {
        if (onSubmit !== undefined) {
            onSubmit(dataSubmit, setIsLoading, setIsHasError, setErrorRerponse);
        }
    };

    //handle close
    const handleClose = () => {
        setIsHasError(false);
        setErrorRerponse(null);

        if (onClose !== undefined) {
            onClose();
        }
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
                            errorResponse={errorResponse} />

                        {renderField(register, errors)}

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

export default FormDialog;