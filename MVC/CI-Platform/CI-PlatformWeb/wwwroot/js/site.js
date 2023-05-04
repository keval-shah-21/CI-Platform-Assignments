// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(".ham").click(() => {
    $(".nav-overlay").removeClass("d-none");
    $(".nav-sidebar").addClass("nav-sidebar-open");
})
$(".btn-close").click(() => {
    $(".nav-overlay").addClass("d-none");
    $(".nav-sidebar").removeClass("nav-sidebar-open");
})
$(".nav-overlay").click(() => {
    $(".btn-close").click();
});

const userId = $("#userId").val();
if (userId != null && userId != "") {
    $.ajax({
        url: "/Volunteer/Notification/GetNotificationPartialByUserId",
        method: "get",
        data: { userId },
        success: (result) => {
            $("#notificationContainer").html(result);
            let count = $("#unread").val();
            if (count == 0)
                $(".notification-count").addClass("d-none");
            else
                $(".notification-count").text(count);

          $("#NotificationSetting").click(() => {
                $(".notification-div").toggleClass("d-none");
                $(".notification-setting-div").removeClass("d-none");
            });
            $(".cancel-notification-setting").click(() => {
                $(".notification-setting-div").addClass("d-none");
            })

            handleBellClick();
            handleClearAllNotification();
            markAsReadNotification();
            handleSettingForm();
        },
        error: (error) => {
            console.log(error);
        }
    });
}
function handleBellClick() {
    $("#bell").click(() => {
        $(".notification-div").toggleClass("d-none");
        $(".notification-setting-div").addClass("d-none");
        $.ajax({
            url: "/Volunteer/Notification/UpdateLastCheck",
            method: "put",
            data: { userId },
            error: error => {
                console.log(error);
            }
        });
    });
}
function handleSettingForm() {
    $("#NotificationSettingForm").on("submit", (e) => {
        e.preventDefault();
        $(".notification-setting-div").addClass("d-none");
        $.ajax({
            url: "/Volunteer/Notification/UpdateNotificationSetting",
            method: "put",
            data: $("#NotificationSettingForm").serialize(),
            success: (_) => {
                simpleAlert("Successfully updated the settings!", "success");
            },
            error: (error) => {
                console.log(error);
                simpleAlert("Something went wrong!", "error");
            }
        });
    });
}
function handleClearAllNotification() {
    $(".clear-all-notification").click(() => {
        $(".notification-content").text("");
        $("#NoNotifications").removeClass("d-none");
        $(".notification-count").addClass("d-none");
        $.ajax({
            url: "/Volunteer/Notification/ClearAllNotification",
            method: "delete",
            data: { userId },
            error: error => {
                console.log(error);
                simpleAlert("Something went wrong!", "error");
            }
        });
    })
}
function markAsReadNotification() {
    $(".orange-dot").click(function () {
        let count = $(".notification-count").text();
        if (count - 1 == 0)
            $(".notification-count").addClass("d-none");
        else
            $(".notification-count").text(count - 1);
        $.ajax({
            url: "/Volunteer/Notification/MarkAsReadNotification",
            method: "put",
            data: { notificationId: $(this).data("id") },
            success: _ => {
                $(this).replaceWith('<img src="/images/static/checked.png" alt="check" class="flex-shrink-0">');
            },
            error: error => {
                console.log(error);
                simpleAlert("Something went wrong!", "error");
            }
        });
    });
}
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
