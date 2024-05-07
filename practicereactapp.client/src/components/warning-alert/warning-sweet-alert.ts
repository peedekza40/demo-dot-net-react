/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable react-hooks/exhaustive-deps */
import Swal from 'sweetalert2';

function warningSweetAlert(message: string | null) {
    if (message) {
        Swal.fire({
            icon: "warning",
            title: message,
            showConfirmButton: true
        });
    }
}

export default warningSweetAlert;