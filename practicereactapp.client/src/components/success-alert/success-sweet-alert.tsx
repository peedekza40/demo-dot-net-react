/* eslint-disable @typescript-eslint/no-explicit-any */
/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable react-hooks/exhaustive-deps */
import Swal from 'sweetalert2';

function successSweetAlert() {
    Swal.fire({
        icon: "success",
        title: "Save success",
        showConfirmButton: false,
        timer: 1500
    });
}

export default successSweetAlert;