/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable react-hooks/exhaustive-deps */
import Swal from 'sweetalert2';

function successSweetAlert(onClosed?: () => void) {
    Swal.fire({
        icon: "success",
        title: "Save success",
        showConfirmButton: false,
        timer: 1500
    }).then(() => {
        if (onClosed !== undefined) {
            onClosed();
        }
    });
}

export default successSweetAlert;