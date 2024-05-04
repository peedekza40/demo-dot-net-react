import { object, string } from 'zod';
import { isExists } from 'src/apis/services/RoleManagement';

export default class RoleForm {
    public id: string;
    public name: string;
}

export const roleFormSchema: any = object({
    id: string()
        .nonempty("ID is required")
        .refine(async (value) => {
            try {
                const response = await isExists(value);
                return response.data == false;
            } catch (error: any) {
                throw new Error('Failed to check role existence');
            }
        }, { message: "Already have this role in system" }),
    name: string().nonempty("Name is required")
});