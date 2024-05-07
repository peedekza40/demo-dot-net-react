/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable react-hooks/exhaustive-deps */
import Swal from 'sweetalert2';
import { ErrorResponse } from "src/utils/global-type";

function errorSweetAlert(isHasError: boolean, errorResponse: ErrorResponse | null) {
    const errorMessage: string = errorResponse != null ? errorResponse?.errors[0]?.description : "Have a someting error. Contact you administator.";
    if (isHasError) {
        Swal.fire({
            icon: "error",
            title: errorMessage,
            showConfirmButton: true
        });
    }
}

export default errorSweetAlert;