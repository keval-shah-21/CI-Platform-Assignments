const missionError = document.querySelector("#missionError").value;
if (missionError == "true") {
    Swal.fire({
        icon: 'error',
        title: 'Oops...',
        text: 'Please participate in any mission to share story!',
        footer: '<a href="/">Checkout missions to apply</a>'
    })
}

$("#shareStoryBtn").click((e) => {
    if ($('#userId').val() == 0) {
        e.preventDefault();
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'You need to Login to Share story!',
            footer: '<a href="/volunteer/user/login">Login here</a>'
        })
    }
})