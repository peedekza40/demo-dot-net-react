import { object, string } from 'zod';
import UserProfile from "src/models/UserProfile";
import config from 'src/config.json';
import { isExistsEmail } from 'src/apis/services/Accout';

export default class RegisterForm extends UserProfile {
    public password: string;
    public confirmPassword: string;
}

export const registerFormSchema: any = object({
    email: string().nonempty("Email is required")
        .email('Email must be a valid email address')
        .refine(async (value) => {
            try {
                const response = await isExistsEmail(value);
                return response.data == false;
            } catch (error: any) {
                throw new Error('Failed to check email existence');
            }
        }, { message: "Already have this email in system" }),
    firstName: string().nonempty("First name is required"),
    lastName: string().nonempty("Last name is required"),
    password: string().nonempty("Password is required")
        .min(config.passwordMinLength, `Password must be more than ${config.passwordMinLength} characters`),
    confirmPassword: string({ required_error: "Confirm password is required" })
})
    .refine((schema) => schema.password === schema.confirmPassword, {
        message: "Passwords don't match",
        path: ["confirmPassword"]
    });