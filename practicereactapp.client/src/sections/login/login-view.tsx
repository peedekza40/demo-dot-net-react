/* eslint-disable @typescript-eslint/no-explicit-any */
import { useEffect, useState } from 'react';

import Box from '@mui/material/Box';
import Link from '@mui/material/Link';
import Card from '@mui/material/Card';
import Stack from '@mui/material/Stack';
//import Button from '@mui/material/Button';
import Divider from '@mui/material/Divider';
import TextField from '@mui/material/TextField';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import LoadingButton from '@mui/lab/LoadingButton';
import { alpha, useTheme } from '@mui/material/styles';
import InputAdornment from '@mui/material/InputAdornment';
import Alert from '@mui/material/Alert';

import { useRouter } from 'src/routes/hooks';

import { bgGradient } from 'src/theme/css';

import Logo from 'src/components/logo';
import Iconify from 'src/components/iconify';

import { object, string, TypeOf } from 'zod';
import { zodResolver } from '@hookform/resolvers/zod';
import { useForm, SubmitHandler, FormProvider } from 'react-hook-form';
import { useAuth } from "src/hooks/use-auth";

// ----------------------------------------------------------------------

const loginSchema = object({
    email: string()
        .nonempty('Email is required')
        .email('Email must be a valid email address'),
    password: string()
        .nonempty('Password is required')
        .min(8, 'Password must be more than 8 characters'),
});
type loginInput = TypeOf<typeof loginSchema>;

export default function LoginView() {
    const theme = useTheme();
    const router = useRouter();
    const auth = useAuth();

    const [showPassword, setShowPassword] = useState(false);
    const [isUnauthorize, setIsUnauthorize] = useState(false);
    const [authorizeMessage, setAuthorizeMessage] = useState("");
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [isLoading, setIsLoading] = useState(false);

    //check is logged in redirect to landing page
    const checkUserIsLogin = () => {
        auth.isLogin(
            () => {
                setIsLoggedIn(true);
                router.push('/');
            },
            () => {
                setIsLoggedIn(false);
            });
    }

    useEffect(() => {
        checkUserIsLogin();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [isLoggedIn]);

    const methods = useForm<loginInput>({
        resolver: zodResolver(loginSchema),
    });

    const {
        //reset,
        handleSubmit,
        register,
        formState: { isSubmitSuccessful, errors },
    } = methods;

    const resetStatus = () => {
        setIsUnauthorize(false);
        setIsLoggedIn(false);
    };

    useEffect(() => {
        if (isSubmitSuccessful) {
            //reset();
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [isSubmitSuccessful]);

    //handle submit
    const onSubmitHandler: SubmitHandler<loginInput> = (values: loginInput) => {
        setIsLoading(true);
        auth.login(values,
            (response: any) => {
                if (response.status == 200) {
                    setIsUnauthorize(false);
                    setIsLoggedIn(true);
                    router.push('/');
                }
            },
            (error: any) => {
                if (error.response.status == 401) {
                    setIsUnauthorize(true);
                    if (error.response.data.detail == "Failed") {
                        setAuthorizeMessage("Email or password is invalid.");
                    } else if (error.response.data.detail == "LockedOut") {
                        setAuthorizeMessage("This user is locked out.");
                    } else {
                        setAuthorizeMessage(error.response.data.detail);
                    }
                }
            },
            () => setIsLoading(false)
        );
    };

    //handle error
    const renderAlert = () => {
        if (isUnauthorize) {
            return (<Alert severity="error" sx={{ my: 3 }}>{authorizeMessage}</Alert>);
        } else if (isLoggedIn) {
            return (<Alert severity="success" sx={{ my: 3 }}>Login success.</Alert>);
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
                <Stack spacing={3}>
                    <TextField
                        label="Email address *"
                        {...register('email')}
                        name="email"
                        error={!!errors.email}
                        helperText={errors.email?.message}
                        onChange={resetStatus}
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
                        onChange={resetStatus}
                    />
                </Stack>

                <Stack direction="row" alignItems="center" justifyContent="flex-end" sx={{ my: 3 }}>
                    <Link variant="subtitle2" underline="hover">
                        Forgot password?
                    </Link>
                </Stack>

                <LoadingButton
                    fullWidth
                    size="large"
                    type="submit"
                    variant="contained"
                    color="inherit"
                    loading={isLoading}
                >
                    Login
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

            <Stack alignItems="center" justifyContent="center" sx={{ height: 1 }}>
                <Card
                    sx={{
                        p: 5,
                        width: 1,
                        maxWidth: 420,
                    }}
                >
                    <Typography variant="h4">Sign in to Minimal</Typography>

                    <Typography variant="body2" sx={{ mt: 2, mb: 5 }}>
                        Don’t have an account?
                        <Link variant="subtitle2" sx={{ ml: 0.5 }}>
                            Get started
                        </Link>
                    </Typography>

                    <Divider sx={{ my: 3 }}>
                        <Typography variant="body2" sx={{ color: 'text.secondary' }}>
                            OR
                        </Typography>
                    </Divider>

                    {renderForm}
                </Card>
            </Stack>
        </Box>
    );
}