/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable react-hooks/exhaustive-deps */
import Alert from '@mui/material/Alert';
import { ErrorResponse } from "src/utils/global-type";

function ErrorAlert({ isHasError, errorResponse }: { isHasError: boolean, errorResponse: ErrorResponse | null }) {
    const errorMessage: string = errorResponse != null ? errorResponse?.errors[0]?.description : "Have a someting error. Contact you administator.";
    if (isHasError) {
        return (<Alert severity="error" sx={{ my: 3 }}>{errorMessage}</Alert>);
    }

    return (<></>);
}

export default ErrorAlert;