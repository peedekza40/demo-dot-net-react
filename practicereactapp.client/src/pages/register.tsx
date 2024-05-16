import { RegisterView } from 'src/sections/register';
import { Helmet } from 'react-helmet-async';

export default function Register() {
    return (
        <>
            <Helmet>
                <title> Register | Minimal UI </title>
            </Helmet>

            <RegisterView />
        </>
    );
}