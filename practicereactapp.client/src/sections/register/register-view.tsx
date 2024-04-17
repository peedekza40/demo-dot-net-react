/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable react-hooks/exhaustive-deps */
import { useEffect, useState } from 'react';

import Box from '@mui/material/Box';
import Link from '@mui/material/Link';
import Card from '@mui/material/Card';
import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';
import Divider from '@mui/material/Divider';
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import LoadingButton from '@mui/lab/LoadingButton';
import { alpha, useTheme } from '@mui/material/styles';
import InputAdornment from '@mui/material/InputAdornment';
import Alert from '@mui/material/Alert';

import Logo from 'src/components/logo';
import Iconify from 'src/components/iconify';
import { bgGradient } from 'src/theme/css';

import { zodResolver } from '@hookform/resolvers/zod';
import { useForm, SubmitHandler, FormProvider } from 'react-hook-form';

import { useRouter } from 'src/routes/hooks';
import RegisterForm, { registerFormSchema } from "src/models/RegisterForm";
import { register as registerApi } from "src/apis/services/Accout";

interface IErrorResponse {
    errors: {
       code: string;
       description: string; 
    }[];
    succeeded: boolean
}

export default function RegisterView() {
    const theme = useTheme();
    const router = useRouter();

    const [showPassword, setShowPassword] = useState(false);
    const [showConfirmPassword, setShowConfirmPassword] = useState(false);
    const [isLoading, setIsLoading] = useState(false);
    const [isHasError, setIsHasError] = useState(false);
    const [errorResponse, setErrorRerponse] = useState<IErrorResponse | null>(null);

    //initial handle form
    const methods = useForm<RegisterForm>({
        resolver: zodResolver(registerFormSchema),
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
    }, [isSubmitSuccessful]);

    //handle submit
    const onSubmitHandler: SubmitHandler<RegisterForm> = (values: RegisterForm) => {
        setIsLoading(true);
        registerApi(values,
            (response: any) => {
                if (response.status == 200) {
                    setIsHasError(false);
                    setErrorRerponse(null);
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

    //handle error
    const renderAlert = () => {
        const errorMessage: string = errorResponse != null ? errorResponse?.errors[0]?.description : "Have a someting error. Contact you administator.";
        if (isHasError) {
            return (<Alert severity="error" sx={{ my: 3 }}>{errorMessage}</Alert>);
        }
    }

    const renderForm = (
        <FormProvider {...methods}>
            <Box
                component='form'
                noValidate
                autoComplete='off'
                onSubmit={handleSubmit(onSubmitHandler)}
            >
                {renderAlert()}
                <Stack spacing={3} sx={{ my: 3 }}>
                    <TextField
                        label="First name *"
                        {...register('firstName')}
                        name="firstName"
                        error={!!errors.firstName}
                        helperText={errors.firstName?.message}
                    />

                    <TextField
                        label="Last name *"
                        {...register('lastName')}
                        name="lastName"
                        error={!!errors.lastName}
                        helperText={errors.lastName?.message}
                    />

                    <TextField
                        label="Email address *"
                        {...register('email')}
                        name="email"
                        error={!!errors.email}
                        helperText={errors.email?.message}
                    />

                    <TextField
                        label="Password *"
                        type={showPassword ? 'text' : 'password'}
                        InputProps={{
                            endAdornment: (
                                <InputAdornment position="end">
                                    <IconButton onClick={() => setShowPassword(!showPassword)} edge="end">
                                        <Iconify icon={showPassword ? 'eva:eye-fill' : 'eva:eye-off-fill'} />
                                    </IconButton>
                                </InputAdornment>
                            ),
                        }}
                        {...register('password')}
                        name="password"
                        error={!!errors.password}
                        helperText={errors.password?.message}
                    />

                    <TextField
                        label="Confirm password *"
                        type={showConfirmPassword ? 'text' : 'password'}
                        InputProps={{
                            endAdornment: (
                                <InputAdornment position="end">
                                    <IconButton onClick={() => setShowConfirmPassword(!showConfirmPassword)} edge="end">
                                        <Iconify icon={showConfirmPassword ? 'eva:eye-fill' : 'eva:eye-off-fill'} />
                                    </IconButton>
                                </InputAdornment>
                            ),
                        }}
                        {...register('confirmPassword')}
                        name="confirmPassword"
                        error={!!errors.confirmPassword}
                        helperText={errors.confirmPassword?.message}
                    />
                </Stack>

                <LoadingButton
                    fullWidth
                    size="large"
                    type="submit"
                    variant="contained"
                    color="inherit"
                    loading={isLoading}
                >
                    Register
                </LoadingButton>
            </Box>
        </FormProvider>
    );
    
    return (
        <Box
            sx={{
                ...bgGradient({
                    color: alpha(theme.palette.background.default, 0.9),
                    imgUrl: '/assets/background/overlay_4.jpg',
                }),
                height: 1,
            }}
        >
            <Logo
                sx={{
                    position: 'fixed',
                    top: { xs: 16, md: 24 },
                    left: { xs: 16, md: 24 },
                }}
            />

            {renderAlert()}
            <Stack alignItems="center" justifyContent="center" sx={{ height: 1 }}>
                <Card
                    sx={{
                        p: 5,
                        width: 1,
                        maxWidth: 600,
                    }}
                >
                    <Typography variant="h4">Register for Minimal</Typography>
                    <Divider sx={{ my: 3 }}></Divider>

                    {renderForm}
                </Card>
            </Stack>
        </Box>
    );
}