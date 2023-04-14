function simpleAlert(title, icon) {
    Swal.fire({
        position: 'top-end',
        icon: icon,
        title: title,
        showConfirmButton: false,
        timer: 1500
    })
}

function alertWithOk(text, icon) {
    Swal.fire({
        icon: icon,
        title: 'Oops...',
        text: text
    })
}

function loginAlert(text) {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: text,
        footer: '<a href="/volunteer/user/login">Login here</a>'
    })
}