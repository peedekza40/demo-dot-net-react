import { Helmet } from 'react-helmet-async';
import { RoleManagementView } from 'src/sections/role-management/view';

function RoleManagementPage() {
    return (
        <>
            <Helmet>
                <title> Role | Minimal UI </title>
            </Helmet>

            <RoleManagementView />
        </>
    );
}

export default RoleManagementPage;