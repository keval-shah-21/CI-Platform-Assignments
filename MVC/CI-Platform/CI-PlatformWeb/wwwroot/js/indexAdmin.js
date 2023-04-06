const partialContainer = $('#partialAdminPageContainer');
let currentSidebarItem = document.querySelector("[data-item='cms']");
const sidebarItems = document.querySelectorAll("[data-item]");
addSidebarEvents();
toggleSidebarCSS(currentSidebarItem);
loadCMSPage();

function toggleSidebarCSS(item) {
    currentSidebarItem.classList.remove('active-sidebar-item');
    const currentData = currentSidebarItem.dataset.item;
    switch (currentData) {
        case "user":
            currentSidebarItem.firstElementChild.src = "/images/static/person-white.svg";
            break;
        case "cms":
            currentSidebarItem.firstElementChild.src = "/images/static/file-earmark-text-white.svg";
            break;
        case "mission":
            currentSidebarItem.firstElementChild.src = "/images/static/bullseye-white.svg";
            break;
        case "theme":
            currentSidebarItem.firstElementChild.src = "/images/static/puzzle-white.svg";
            break;
        case "skill":
            currentSidebarItem.firstElementChild.src = "/images/static/tools-white.svg";
            break;
        case "application":
            currentSidebarItem.firstElementChild.src = "/images/static/folder-white.svg";
            break;
        case "timesheet":
            currentSidebarItem.firstElementChild.src = "/images/static/hourglass-top-white.svg";
            break;
        case "story":
            currentSidebarItem.firstElementChild.src = "/images/static/bookmark-star-white.svg";
            break;
        case "banner":
            currentSidebarItem.firstElementChild.src = "/images/static/image-white.svg";
            break;
        case "contact":
            currentSidebarItem.firstElementChild.src = "/images/static/chat-dots-white.svg";
            break;
    }
    item.classList.add("active-sidebar-item");
    const data = item.dataset.item;
    switch (data) {
        case "user":
            item.firstElementChild.src = "/images/static/person-fill.svg";
            break;
        case "cms":
            item.firstElementChild.src = "/images/static/file-earmark-text-fill.svg";
            break;
        case "mission":
            item.firstElementChild.src = "/images/static/bullseye-fill.svg";
            break;
        case "theme":
            item.firstElementChild.src = "/images/static/puzzle-fill.svg";
            break;
        case "skill":
            item.firstElementChild.src = "/images/static/tools-fill.svg";
            break;
        case "application":
            item.firstElementChild.src = "/images/static/folder-fill.svg";
            break;
        case "timesheet":
            item.firstElementChild.src = "/images/static/hourglass-top.svg";
            break;
        case "story":
            item.firstElementChild.src = "/images/static/bookmark-star-fill.svg";
            break;
        case "banner":
            item.firstElementChild.src = "/images/static/image-fill.svg";
            break;
        case "contact":
            item.firstElementChild.src = "/images/static/chat-dots-fill.svg";
            break;
    }

    currentSidebarItem = item;
}
function addSidebarEvents() {
    sidebarItems.forEach((item) => {
        item.addEventListener("click", () => {
            if (currentSidebarItem.dataset.item == item.dataset.item) return;
            toggleSidebarCSS(item);
            switch (item.dataset.item) {
                case "user":
                    loadUserPage();
                    break;
                case "cms":
                    loadCMSPage();
                    break;
                case "mission":
                    loadMissionPage();
                    break;
                case "theme":
                    loadThemePage();
                    break;
                case "skill":
                    loadSkillPage();
                    break;
                case "application":
                    loadApplicationPage();
                    break;
                case "timesheet":
                    loadTimesheetPage();
                    break;
                case "story":
                    loadStoryPage();
                    break;
                case "banner":
                    loadBannerPage();
                    break;
                case "contact":
                    loadContactPage();
                    break;
            }
        })
    })
}

function loadCMSPage() {
    $.ajax({
        url: "/Admin/Cms/LoadCMSPage",
        method: "GET",
        success: (result, _, status) => {
            if (status == 204) {
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Something went wrong!',
                    showConfirmButton: false,
                    timer: 1500
                })
                return;
            }
            partialContainer.html(result);
            createPagination();
        },
        error: (error) => {
            console.log(error)
        }
    });
}
function loadMissionPage() {
    $.ajax({
        url: "/Admin/Mission/LoadMissionPage",
        method: "GET",
        success: (result, _, status) => {
            if (status == 204) {
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Something went wrong!',
                    showConfirmButton: false,
                    timer: 1500
                })
                return;
            }
            partialContainer.html(result);
            createPagination();
        },
        error: (error) => {
            console.log(error)
        }
    });
}
function loadStoryPage() {
    $.ajax({
        url: "/Admin/Story/LoadStoryPage",
        method: "GET",
        success: (result, _, status) => {
            if (status == 204) {
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Something went wrong!',
                    showConfirmButton: false,
                    timer: 1500
                })
                return;
            }
            partialContainer.html(result);
            createPagination();
        },
        error: (error) => {
            console.log(error)
        }
    });
}
function loadThemePage() {
    $.ajax({
        url: "/Admin/Mission/LoadThemePage",
        method: "GET",
        success: (result, _, status) => {
            if (status == 204) {
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Something went wrong!',
                    showConfirmButton: false,
                    timer: 1500
                })
                return;
            }
            partialContainer.html(result);
            createPagination();
        },
        error: (error) => {
            console.log(error)
        }
    });
}
function loadSkillPage() {
    $.ajax({
        url: "/Admin/Mission/LoadSkillPage",
        method: "GET",
        success: (result, _, status) => {
            if (status == 204) {
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Something went wrong!',
                    showConfirmButton: false,
                    timer: 1500
                })
                return;
            }
            partialContainer.html(result);
            createPagination();
        },
        error: (error) => {
            console.log(error)
        }
    });
}
function loadApplicationPage() {
    $.ajax({
        url: "/Admin/Mission/LoadApplicationPage",
        method: "GET",
        success: (result, _, status) => {
            if (status == 204) {
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Something went wrong!',
                    showConfirmButton: false,
                    timer: 1500
                })
                return;
            }
            partialContainer.html(result);
            createPagination();
        },
        error: (error) => {
            console.log(error)
        }
    });
}
function loadTimesheetPage() {
    $.ajax({
        url: "/Admin/Mission/LoadTimesheetPage",
        method: "GET",
        success: (result, _, status) => {
            if (status == 204) {
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Something went wrong!',
                    showConfirmButton: false,
                    timer: 1500
                })
                return;
            }
            partialContainer.html(result);
            createPagination();
        },
        error: (error) => {
            console.log(error)
        }
    });
}
function loadContactPage() {
    $.ajax({
        url: "/Admin/User/LoadContactPage",
        method: "GET",
        success: (result, _, status) => {
            if (status == 204) {
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Something went wrong!',
                    showConfirmButton: false,
                    timer: 1500
                })
                return;
            }
            partialContainer.html(result);
            createPagination();
        },
        error: (error) => {
            console.log(error)
        }
    });
}
function loadUserPage() {
    $.ajax({
        url: "/Admin/User/LoadUserPage",
        method: "GET",
        success: (result, _, status) => {
            if (status == 204) {
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Something went wrong!',
                    showConfirmButton: false,
                    timer: 1500
                })
                return;
            }
            partialContainer.html(result);
            createPagination();
        },
        error: (error) => {
            console.log(error)
        }
    });
}
function loadBannerPage() {
    $.ajax({
        url: "/Admin/Home/LoadBannerPage",
        method: "GET",
        success: (result, _, status) => {
            if (status == 204) {
                Swal.fire({
                    position: 'top-end',
                    icon: 'error',
                    title: 'Something went wrong!',
                    showConfirmButton: false,
                    timer: 1500
                })
                return;
            }
            partialContainer.html(result);
            createPagination();
        },
        error: (error) => {
            console.log(error)
        }
    });
}


//event listners
function cmsEvents() {
    $("#addCmsBtn").click(() => {
        $.ajax({
            url: "/Admin/Cms/AddCMSPage",
            success: (result, _, status) => {
                partialContainer.html(result);
            },
            error: (error) => {
                console.log(error)
            }
        });
    })
}

//sidebar toggle
$(".hamburger").click(() => {
    $(".sidebar").addClass("sidebar-open");
    $(".overlay").removeClass("d-none");
})
$(".btn-close").click(() => {
    $(".sidebar").removeClass("sidebar-open");
    $(".overlay").addClass("d-none");
})
$(".overlay").click(() => $(".btn-close").click())
//current time 
const months = [
    'January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'
];
const days = [
    'Sunday', 'Monday', 'Tueday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'
];
function updateCurrentDateTime() {
    let currentDate = new Date();
    document.getElementById("currentTime").textContent = `${days[currentDate.getDay()]}, ${months[currentDate.getMonth()]} ${currentDate.getDate()}, ${currentDate.getFullYear()}, ${currentDate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })}`;
}
updateCurrentDateTime();
setInterval(updateCurrentDateTime, 1000);
