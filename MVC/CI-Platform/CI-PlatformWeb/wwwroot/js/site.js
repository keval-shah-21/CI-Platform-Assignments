// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const drop = document.querySelector(".cms-dropdown");
if (drop) {
    $.ajax({
        url: "/Volunteer/Cms/GetCmsList",
        success: (result) => {
            for (var cms of result) {
                drop.innerHTML += `<li><a href="/Volunteer/Cms/CmsPage/${cms.cmsPageId}" class="dropdown-item">${cms.title}</a></li>`;
            }
        },
        error: error => {
            console.log(error);
        }
    });
}
$('#logoutBtn').click(() => {
    Swal.fire({
        title: 'Are you sure?',
        icon: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Logout!'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.replace("/Volunteer/User/logout");
        }
    })
});

const contactBtn = document.querySelector("#contactBtn");
if (contactBtn) {
    $("#contactBtn").click(() => {
        const userId = $('#sessionUserId').val();
        if (userId != "" && userId != null) {
            $.ajax({
                url: "/volunteer/user/contact-partial",
                success: result => {
                    $('#partialContactContainer').html(result);
                    $("#contactModal").modal("show");
                    $("#contactForm").on("submit", (e) => {
                        e.preventDefault();
                        $('#contactForm').valid();
                        if ($('#contactForm').valid()) {
                            $("#contactModal").modal("hide");
                            $.ajax({
                                url: "/volunteer/user/contact-admin",
                                method: "POST",
                                data: $('#contactForm').serialize(),
                                success: result => {
                                    Swal.fire({
                                        position: 'top-end',
                                        icon: 'success',
                                        title: 'Thank you for contacting us!',
                                        showConfirmButton: false,
                                        timer: 1500
                                    })
                                },
                                error: error => {
                                    console.log(error);
                                }
                            });
                        }
                    })
                },
                error: error => {
                    console.log(error)
                }
            });
        } else {
            Swal.fire({
                icon: 'error',
                title: 'Oops...',
                text: 'You need to Login to contact admin!',
                footer: '<a href="/volunteer/user/login">Login here</a>'
            })
        }
    });
}
