// import { object, string } from 'zod';
import config from '../config.json';

export default class LoginForm {
    public email: string | undefined;
    public password: string | undefined;
}

// export const loginFormSchema = object({
//     email: string()
//         .nonempty("Email is required")
//         .email('Email must be a valid email address'),
//     password: string()
//         .nonempty("Password is required")
//         .min(config.passwordMinLength, `Password must be more than ${config.passwordMinLength} characters`)
// });