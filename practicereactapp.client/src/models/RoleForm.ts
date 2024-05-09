import { object, string, nativeEnum } from 'zod';
import { isExists } from 'src/apis/services/RoleManagement';
import ActionMode from 'src/constants/action-mode';

export default class RoleForm {
    public id: string;
    public name: string;
    public mode: ActionMode;
}

export const roleFormSchema = object({
    id: string()
        .nonempty("ID is required"),
    name: string().nonempty("Name is required"),
    mode: nativeEnum(ActionMode).optional()
}).refine(async (schema) => {
    try {
        if(schema.mode == ActionMode.Add){
            const response = await isExists(schema.id);
            return response.data == false;
        }
        return true;
    } catch (error: any) {
        throw new Error('Failed to check role existence');
    }
}, {
    message: "Already have this role in system",
    path: ["id"]
});