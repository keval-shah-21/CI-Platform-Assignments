const partialContainer = $('#partialAdminPageContainer');
const partialModalContainer = $('#partialModalContainer');
let currentSidebarItem = document.querySelector("[data-item='cms']");
const sidebarItems = document.querySelectorAll("[data-item]");

loadIntialPartial("/Admin/Cms/LoadCmsPage", "cms");
addSidebarEvents();
toggleSidebarCSS(currentSidebarItem);

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
            currentSidebarItem.firstElementChild.src = "/images/static/ticket-white.svg";
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
            item.firstElementChild.src = "/images/static/ticket-fill.svg";
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
            $(".btn-close").click();
            if (currentSidebarItem.dataset.item == item.dataset.item) return;
            toggleSidebarCSS(item);
            switch (item.dataset.item) {
                case "user":
                    loadIntialPartial("/Admin/User/LoadUserPage", item.dataset.item);
                    break;
                case "cms":
                    loadIntialPartial("/Admin/Cms/LoadCmsPage", item.dataset.item);
                    break;
                case "mission":
                    loadIntialPartial("/Admin/Mission/LoadMissionPage", item.dataset.item);
                    break;
                case "theme":
                    loadIntialPartial("/Admin/Theme/LoadThemePage", item.dataset.item);
                    break;
                case "skill":
                    loadIntialPartial("/Admin/Skill/LoadSkillPage", item.dataset.item);
                    break;
                case "application":
                    loadIntialPartial("/Admin/Application/LoadApplicationPage", item.dataset.item);
                    break;
                case "timesheet":
                    loadIntialPartial("/Admin/Timesheet/LoadTimesheetPage", item.dataset.item);
                    break;
                case "story":
                    loadIntialPartial("/Admin/Story/LoadStoryPage", item.dataset.item);
                    break;
                case "banner":
                    loadIntialPartial("/Admin/Banner/LoadBannerPage", item.dataset.item);
                    break;
                case "contact":
                    loadIntialPartial("/Admin/Contact/LoadContactPage", item.dataset.item);
                    break;
            }
        })
    })
}
function setSidebarHeight() {
    const adminRight = $(".admin-right").outerHeight();
    const sidebar = $(".sidebar").outerHeight();
    if (adminRight > sidebar) {
        $(".sidebar").outerHeight(adminRight);
    } else {
        $(".sidebar").outerHeight('100vh');
    }
}
observeSizeChanges();
function observeSizeChanges() {
    const targetNode = document.querySelector('#partialAdminPageContainer');
    const observer = new ResizeObserver(entries => {
        setSidebarHeight();
    });
    observer.observe(targetNode);
}
$(window).on("resize", () => setSidebarOnResize());
const setSidebarOnResize = debounce(() => setSidebarHeight());

//timesheet events
function addTimesheetEvents() {
    $("#adminSearch").on("input", () => {
        $(".spinner-border").removeClass("opacity-0");
        $(".spinner-border").addClass("opacity-1");
        const query = $('#adminSearch').val();
        if ($("[data-search]").data("search") == "time")
            searchBar("/Admin/Timesheet/SearchTimesheet", query, "timesheet");
        else
            searchBar("/Admin/Timesheet/SearchGoalsheet", query, "timesheet");
    })
    $("time").click(() =>
        loadIntialPartial("/Admin/Timesheet/LoadTimesheetPage", "timesheet")
    );
    $("goal").click(() =>
        loadIntialPartial("/Admin/Timesheet/LoadGoalsheetPage", "timesheet"));
}

function timesheetTableEvents() {
    document.querySelectorAll("[data-decline]").forEach((decline) => {
        decline.addEventListener("click", () => {
            Swal.fire({
                title: 'Are you sure?',
                text: "User will be notified!",
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Decline!'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: "/Admin/Timesheet/UpdateStatus",
                        method: "PUT",
                        data: { id: $(decline).data("decline"), value: 2 },
                        success: (result) => {
                            partialContainer.html(result);
                            createPagination();
                            simpleAlert("Successfully declined the timesheet!", "success");
                            addAllEvents("timesheet");
                        },
                        error: (error) => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                }
            })
        })
    });
    document.querySelectorAll("[data-accept]").forEach((accept) => {
        accept.addEventListener("click", () => {
            $.ajax({
                url: "/Admin/Timesheet/UpdateStatus",
                method: "PUT",
                data: { id: $(accept).data("accept"), value: 1 },
                success: (result) => {
                    partialContainer.html(result);
                    createPagination();
                    simpleAlert("Successfully approved the timesheet!", "success");
                    addAllEvents("timesheet");
                },
                error: (error) => {
                    console.log(error);
                    simpleAlert("Something went wrong!", "error");
                }
            });
        })
    });
}

//story events
function addStoryEvents() {
    $("#adminSearch").on("input", () => {
        $(".spinner-border").removeClass("opacity-0");
        $(".spinner-border").addClass("opacity-1");
        const query = $('#adminSearch').val();
        searchBar("/Admin/Story/SearchStory", query, "story");
    })
}
function storyTableEvents() {
    document.querySelectorAll("[data-decline]").forEach((decline) => {
        decline.addEventListener("click", () => {
            Swal.fire({
                title: 'Are you sure?',
                text: "User will be notified!",
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Decline!'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: "/Admin/Story/UpdateStatus",
                        method: "PUT",
                        data: { id: $(decline).data("decline"), value: 2 },
                        success: (result) => {
                            partialContainer.html(result);
                            createPagination();
                            simpleAlert("Successfully declined the story!", "success");
                            addAllEvents("story");
                        },
                        error: (error) => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                }
            })
        })
    });
    document.querySelectorAll("[data-restore]").forEach((restore) => {
        restore.addEventListener("click", () => {
            $.ajax({
                url: "/Admin/Story/UpdateStatus",
                method: "PUT",
                data: { id: $(restore).data("restore"), value: 1 },
                success: (result) => {
                    partialContainer.html(result);
                    createPagination();
                    simpleAlert("Successfully updated the story!", "success");
                    addAllEvents("story");
                },
                error: (error) => {
                    console.log(error);
                    simpleAlert("Something went wrong!", "error");
                }
            });
        })
    });
    document.querySelectorAll("[data-accept]").forEach((accept) => {
        accept.addEventListener("click", () => {
            $.ajax({
                url: "/Admin/Story/AcceptStory",
                method: "PUT",
                data: { id: $(accept).data("accept") },
                success: (result) => {
                    partialContainer.html(result);
                    createPagination();
                    simpleAlert("Successfully approved the story!", "success");
                    addAllEvents("story");
                },
                error: (error) => {
                    console.log(error);
                    simpleAlert("Something went wrong!", "error");
                }
            });
        })
    });
    document.querySelectorAll("[data-delete]").forEach((del) => {
        del.addEventListener("click", () => {
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to retrieve it back!",
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Remove!'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: "/Admin/Story/DeleteStory",
                        method: "DELETE",
                        data: { id: $(del).data("delete") },
                        success: (result) => {
                            simpleAlert("Successfully removed the story!", "success");
                            partialContainer.html(result);
                            createPagination();
                            addAllEvents("story");
                        },
                        error: (error) => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                }
            })
        })
    });
}

//banner evets
function addBannerEvents() {
    $("#addBtn").click(() => {
        $.ajax({
            url: "/Admin/Banner/AddBanner",
            success: (result) => {
                partialContainer.html(result);
                addBannerFormEvent();
            },
            error: (error) => {
                console.log(error);
                simpleAlert("Something went wrong!", "error");
            }
        });
    })
}
function addBannerFormEvent() {
    $("#cancelBtn").click(() => loadIntialPartial("/Admin/Banner/LoadBannerPage", "banner"))
    const preview = document.getElementById("preview");
    $("#bannerImage").on("change", () => {
        preview.src = URL.createObjectURL(document.querySelector("#bannerImage").files[0]);
        preview.style.height = "250px";
    })

    $("#bannerForm").on("submit", (e) => {
        e.preventDefault();
        let isValid = $("#bannerForm").valid();
        if (document.querySelector("#bannerImage").files.length == 0) {
            $("#imageError").text("The Banner Image field is required.")
            return
        } else {
            $("#imageError").text("");
        }
        let formData = new FormData($("#bannerForm")[0]);
        formData.append("bannerImage", document.querySelector("#bannerImage").files[0]);
        if (!isValid) return;
        $.ajax({
            url: "/Admin/Banner/AddBanner",
            method: "POST",
            data: formData,
            processData: false,
            contentType: false,
            success: (result) => {
                simpleAlert("Successfully added the banner!", "success");
                partialContainer.html(result);
                createPagination();
                addAllEvents("banner")
            },
            error: (error) => {
                console.log(error);
                simpleAlert("Something went wrong!", "error");
            }
        });
    })
}
function editBannerFormEvents() {
    $("#cancelBtn").click(() => loadIntialPartial("/Admin/Banner/LoadBannerPage", "banner"))

    const url = $("#MediaPath").val() + $("#MediaName").val() + $("#MediaType").val();
    const fileName = $("#MediaName").val() + $("#MediaType").val();
    const type = $("#MediaType").val();
    const preview = document.getElementById("preview");

    fetch(url)
        .then(response => response.arrayBuffer())
        .then(buffer => {
            const myFile = new File([buffer], fileName, { type: `image/${type.slice(1)}` });
            preview.src = URL.createObjectURL(myFile);
            preview.style.height = "250px";

            let myFileList = new DataTransfer();
            myFileList.items.add(myFile);
            document.querySelector("#bannerImage").files = myFileList.files;
        });

    $("#bannerImage").on("change", () => {
        preview.src = URL.createObjectURL(document.querySelector("#bannerImage").files[0]);
        preview.style.height = "250px";
    })

    $("#bannerForm").on("submit", (e) => {
        e.preventDefault();
        let isValid = $("#bannerForm").valid();
        if (document.querySelector("#bannerImage").files.length == 0) {
            $("#imageError").text("The Banner Image field is required.")
            return
        } else {
            $("#imageError").text("");
        }
        let formData = new FormData($("#bannerForm")[0]);
        formData.append("bannerImage", document.querySelector("#bannerImage").files[0]);
        if (!isValid) return;
        $.ajax({
            url: "/Admin/Banner/EditBanner",
            method: "PUT",
            data: formData,
            processData: false,
            contentType: false,
            success: (result) => {
                simpleAlert("Successfully updated the banner!", "success");
                partialContainer.html(result);
                createPagination(4);
                addAllEvents("banner")
            },
            error: (error) => {
                console.log(error);
                simpleAlert("Something went wrong!", "error");
            }
        });
    })
}
function bannerTableEvents() {
    document.querySelectorAll("[data-edit]").forEach((edit) => {
        edit.addEventListener("click", () => {
            $.ajax({
                url: "/Admin/Banner/EditBanner",
                method: "GET",
                data: { id: $(edit).data("edit") },
                success: (result) => {
                    partialContainer.html(result);
                    editBannerFormEvents();
                },
                error: (error) => {
                    console.log(error);
                    simpleAlert("Something went wrong!", "error");
                }
            });
        })
    });
    document.querySelectorAll("[data-delete]").forEach((del) => {
        del.addEventListener("click", () => {
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to retrieve it back!",
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Remove!'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: "/Admin/Banner/DeleteBanner",
                        method: "DELETE",
                        data: { id: $(del).data("delete") },
                        success: (result) => {
                            simpleAlert("Successfully deleted the banner!", "success")
                            partialContainer.html(result);
                            createPagination(4);
                            addAllEvents("banner");
                        },
                        error: (error) => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                }
            })
        })
    })
}

//contact events
function addContactEvents() {
    $("#adminSearch").on("input", () => {
        $(".spinner-border").removeClass("opacity-0");
        $(".spinner-border").addClass("opacity-1");
        const query = $('#adminSearch').val();
        searchBar("/Admin/Contact/SearchContact", query, "contact");
    })
}
function contactTableEvents() {
    document.querySelectorAll("[data-view]").forEach((view) => {
        view.addEventListener("click", () => {
            $.ajax({
                url: "/Admin/Contact/ViewMessage",
                method: "GET",
                data: { id: $(view).data("view") },
                success: (result) => {
                    partialModalContainer.html(result);
                    $("#contactViewModal").modal("show");
                },
                error: (error) => {
                    console.log(error);
                    simpleAlert("Something went wrong!", "error");
                }
            });
        })
    });
    document.querySelectorAll("[data-reply]").forEach((reply) => {
        reply.addEventListener("click", () => {
            $.ajax({
                url: "/Admin/Contact/ReplyMessage",
                method: "GET",
                data: { id: $(reply).data("reply") },
                success: (result) => {
                    partialModalContainer.html(result);
                    $("#contactReplyModal").modal("show");
                    contactFormEvents();
                },
                error: (error) => {
                    console.log(error);
                    simpleAlert("Something went wrong!", "error");
                }
            });
        })
    });
    document.querySelectorAll("[data-delete]").forEach((del) => {
        del.addEventListener("click", () => {
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to retrieve it back!",
                icon: 'question',
                showDenyButton: true,
                showCancelButton: true,
                confirmButtonText: 'Yes, Delete',
                denyButtonText: `Reply`,
                confirmButtonColor: '#3085d6',
                denyButtonColor: '#5cb85c'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: "/Admin/Contact/DeleteMessage",
                        method: "DELETE",
                        data: { id: $(del).data("delete") },
                        success: (result) => {
                            simpleAlert("Successfully removed the message!", "success")
                            partialContainer.html(result);
                            createPagination();
                            addAllEvents("contact")
                        },
                        error: (error) => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                } else if (result.isDenied) {
                    $.ajax({
                        url: "/Admin/Contact/ReplyMessage",
                        method: "GET",
                        data: { id: $(del).data("delete") },
                        success: (result) => {
                            partialModalContainer.html(result);
                            $("#contactReplyModal").modal("show");
                            contactFormEvents();
                        },
                        error: (error) => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                }
            })
        })
    });
}
function contactFormEvents() {
    $("#contactForm").on("submit", (e) => {
        e.preventDefault();

        let isValid = $("#contactForm").valid();
        if (!isValid) return;
        if ($("#Reply").val().trim() == null || $("#Reply").val().trim() == '') {
            $("#replyError").text("The Reply field is required.");
            return
        } else {
            $("#replyError").text("");
        }
        $.ajax({
            url: "/Admin/Contact/ReplyMessage",
            method: "PUT",
            data: $("#contactForm").serialize(),
            success: (result) => {
                $("#contactReplyModal").modal("hide");
                simpleAlert("Successfully replied to the user!", "success");
                partialContainer.html(result);
                createPagination();
                addAllEvents("contact")
            },
            error: (error) => {
                console.log(error);
                simpleAlert("Something went wrong!", "error");
            }
        });
    })
}
//skill events
function addSkillEvents() {
    $("#addBtn").click(() => {
        $.ajax({
            url: "/Admin/Skill/AddSkill",
            success: (result) => {
                partialModalContainer.html(result);
                $("#skillModal").modal("show");
                skillFormEvents("/Admin/Skill/AddSkill", "POST");
            },
            error: (error) => {
                console.log(error);
                simpleAlert("Something went wrong!", "error");
            }
        });
    })
    $("#adminSearch").on("input", () => {
        $(".spinner-border").removeClass("opacity-0");
        $(".spinner-border").addClass("opacity-1");
        const query = $('#adminSearch').val();
        searchBar("/Admin/Skill/SearchSkill", query, "skill");
    })
}
function skillFormEvents(url, method) {
    $("#skillForm").on("submit", (e) => {
        e.preventDefault();
        let isValid = $("#skillForm").valid();
        if (!isValid) return;
        obj = {
            skillName: $("#SkillName").val()
        }
        if (method == "PUT")
            obj["id"] = $("#SkillId").val()

        $.ajax({
            url: "/Admin/Skill/IsSkillUnique",
            method: "GET",
            data: obj,
            success: (result) => {
                if (result == true) {
                    $("#skillModal").modal("hide");
                    $.ajax({
                        url: url,
                        method: method,
                        data: $("#skillForm").serialize(),
                        success: (result) => {
                            if (method == "POST") {
                                simpleAlert("Successfully added the skill!", "success");
                            } else {
                                simpleAlert("Successfully updated the skill!", "success");
                            }
                            partialContainer.html(result);
                            createPagination();
                            addAllEvents("skill");
                        },
                        error: error => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                } else {
                    $("#skillError").text("Skill already exists!");
                }
            },
            error: error => {
                console.log(error);
            }
        });
    })
}
function skillTableEvents() {
    document.querySelectorAll("[data-edit]").forEach((edit) => {
        edit.addEventListener("click", () => {
            $.ajax({
                url: "/Admin/Skill/EditSkill",
                method: "GET",
                data: { id: $(edit).data("edit") },
                success: (result) => {
                    partialModalContainer.html(result);
                    $("#skillModal").modal("show");
                    skillFormEvents("/Admin/Skill/EditSkill", "PUT");
                },
                error: (error) => {
                    console.log(error);
                    simpleAlert("Something went wrong!", "error");
                }
            });
        })
    });
    document.querySelectorAll("[data-delete]").forEach((del) => {
        del.addEventListener("click", () => {
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to retrieve it back!",
                icon: 'question',
                showDenyButton: true,
                showCancelButton: true,
                confirmButtonText: 'Yes, Delete',
                denyButtonText: `Deactivate Instead`,
                confirmButtonColor: '#3085d6',
                denyButtonColor: '#5cb85c'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: "/Admin/Skill/DeleteSkill",
                        method: "DELETE",
                        data: { id: $(del).data("delete") },
                        success: (result, _, status) => {
                            if (status.status == 204) {
                                alertWithOk("Can't delete because it's already used by mission or user!", "warning");
                                return;
                            }
                            simpleAlert("Successfully deleted the skill!", "success")
                            partialContainer.html(result);
                            createPagination();
                            addAllEvents("skill");
                        },
                        error: (error) => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                } else if (result.isDenied) {
                    $.ajax({
                        url: "/Admin/Skill/DeactivateSkill",
                        method: "PUT",
                        data: { id: $(del).data("delete"), value: 0 },
                        success: (result) => {
                            simpleAlert("Successfully deactivated the skill!", "success")
                            partialContainer.html(result);
                            createPagination();
                            addAllEvents("skill");
                        },
                        error: (error) => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                }
            })
        })
    });
}

//theme events
function addThemeEvents() {
    $("#addBtn").click(() => {
        $.ajax({
            url: "/Admin/Theme/AddTheme",
            success: (result) => {
                partialModalContainer.html(result);
                $("#themeModal").modal("show");
                themeFormEvents("/Admin/Theme/AddTheme", "POST");
            },
            error: (error) => {
                console.log(error);
                simpleAlert("Something went wrong!", "error");
            }
        });
    })
    $("#adminSearch").on("input", () => {
        $(".spinner-border").removeClass("opacity-0");
        $(".spinner-border").addClass("opacity-1");
        const query = $('#adminSearch').val();
        searchBar("/Admin/Theme/SearchTheme", query, "theme");
    })
}
function themeFormEvents(url, method) {
    $("#themeForm").on("submit", (e) => {
        e.preventDefault();
        let isValid = $("#themeForm").valid();
        if (!isValid) return;
        obj = {
            themeName: $("#MissionThemeName").val()
        }
        if (method == "PUT")
            obj["id"] = $("#MissionThemeId").val()

        $.ajax({
            url: "/Admin/Theme/IsThemeUnique",
            method: "GET",
            data: obj,
            success: (result) => {
                if (result == true) {
                    $("#themeModal").modal("hide");
                    $.ajax({
                        url: url,
                        method: method,
                        data: $("#themeForm").serialize(),
                        success: (result) => {
                            if (method == "POST") {
                                simpleAlert("Successfully added the theme!", "success");
                            } else {
                                simpleAlert("Successfully updated the theme!", "success");
                            }
                            partialContainer.html(result);
                            createPagination();
                            addAllEvents("theme");
                        },
                        error: error => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                } else {
                    $("#themeError").text("Theme already exists!");
                }
            },
            error: error => {
                console.log(error);
            }
        });
    })
}
function themeTableEvents() {
    document.querySelectorAll("[data-edit]").forEach((edit) => {
        edit.addEventListener("click", () => {
            $.ajax({
                url: "/Admin/Theme/EditTheme",
                method: "GET",
                data: { id: $(edit).data("edit") },
                success: (result) => {
                    partialModalContainer.html(result);
                    $("#themeModal").modal("show");
                    themeFormEvents("/Admin/Theme/EditTheme", "PUT");
                },
                error: (error) => {
                    console.log(error);
                    simpleAlert("Something went wrong!", "error");
                }
            });
        })
    });
    document.querySelectorAll("[data-delete]").forEach((del) => {
        del.addEventListener("click", () => {
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to retrieve it back!",
                icon: 'question',
                showDenyButton: true,
                showCancelButton: true,
                confirmButtonText: 'Yes, Delete',
                denyButtonText: `Deactivate Instead`,
                confirmButtonColor: '#3085d6',
                denyButtonColor: '#5cb85c'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: "/Admin/Theme/DeleteTheme",
                        method: "DELETE",
                        data: { id: $(del).data("delete") },
                        success: (result, _, status) => {
                            if (status.status == 204) {
                                alertWithOk("Can't delete because it's already used in mission!", "warning");
                                return;
                            }
                            simpleAlert("Successfully deleted the theme!", "success")
                            partialContainer.html(result);
                            createPagination();
                            addAllEvents("theme");
                        },
                        error: (error) => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                } else if (result.isDenied) {
                    $.ajax({
                        url: "/Admin/Theme/DeactivateTheme",
                        method: "PUT",
                        data: { id: $(del).data("delete"), value: 0 },
                        success: (result) => {
                            simpleAlert("Successfully deactivated the theme!", "success")
                            partialContainer.html(result);
                            createPagination();
                            addAllEvents("theme");
                        },
                        error: (error) => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                }
            })
        })
    });
}

//user page events
function addUserEvents() {
    $("#adminSearch").on("input", () => {
        $(".spinner-border").addClass("opacity-1");
        $(".spinner-border").removeClass("opacity-0");
        const query = $('#adminSearch').val();
        searchBar("/Admin/User/SearchUser", query, "user");
    })
    document.querySelectorAll("[data-activate]").forEach((act) => {
        act.addEventListener("click", () => {
            Swal.fire({
                title: 'Are you sure?',
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#5cb85c',
                confirmButtonText: 'Yes, Activate!'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: "/Admin/User/ActivateUser",
                        method: "GET",
                        data: { email: $(act).data("activate") },
                        success: (result) => {
                            partialContainer.html(result);
                            simpleAlert("Successfully activated the user!", "success");
                            addAllEvents("user");
                        },
                        error: error => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                }
            })
        })
    });
    document.querySelectorAll("[data-deactivate]").forEach((act) => {
        act.addEventListener("click", () => {
            Swal.fire({
                title: 'Are you sure?',
                icon: 'question',
                text: "User won't be able to login after this!",
                showCancelButton: true,
                confirmButtonColor: '#df4759',
                confirmButtonText: 'Yes, Deactivate!'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: "/Admin/User/DeactivateUser",
                        method: "GET",
                        data: { email: $(act).data("deactivate") },
                        success: (result) => {
                            partialContainer.html(result);
                            simpleAlert("Successfully deactivated the user!", "success");
                            createPagination();
                            addUserEvents();
                        },
                        error: error => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                }
            })
        })
    });
}

//cms page event listners
function addCmsEvents() {
    $("#addBtn").click(() => {
        $.ajax({
            url: "/Admin/Cms/AddCMSPage",
            success: (result) => {
                partialContainer.html(result);
                $.getScript("/js/tinymceCmsScript.js");
                cmsFormEvents("/Admin/Cms/AddCMSPage", "POST");
            },
            error: (error) => {
                console.log(error);
                simpleAlert("Something went wrong!", "error");
            }
        });
    })
    $("#adminSearch").on("input", () => {
        $(".spinner-border").removeClass("opacity-0");
        $(".spinner-border").addClass("opacity-1");
        const query = $('#adminSearch').val();
        searchBar("/Admin/Cms/SearchCmsPage", query, "cms");
    })
}
function cmsFormEvents(url, method) {
    $("#cancelBtn").click(() => loadIntialPartial("/Admin/Cms/LoadCmsPage", "cms"))
    $("#CmsForm").on("submit", (e) => {
        e.preventDefault();
        let isValid = $("#CmsForm").valid();

        if (tinymce.get("tiny").getContent() == null || tinymce.get("tiny").getContent() == "") {
            $('#descriptionError').text("The description field is required.");
            return;
        } else
            $('#descriptionError').text("");

        if (!isValid) return;
        obj = {
            slug: $("#Slug").val()
        }
        if (method == "PUT")
            obj["id"] = $("#CmsPageId").val()

        $.ajax({
            url: "/Admin/Cms/IsSlugUnique",
            method: "GET",
            data: obj,
            success: (result) => {
                if (result == true) {
                    $.ajax({
                        url: url,
                        method: method,
                        data: $("#CmsForm").serialize(),
                        success: (result) => {
                            if (method == "POST") {
                                simpleAlert("Successfully added the page!", "success");
                            } else {
                                simpleAlert("Successfully updated the page!", "success");
                            }
                            tinymce.remove("textarea#tiny");
                            partialContainer.html(result);
                            createPagination();
                            addAllEvents("cms");
                        },
                        error: error => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                } else {
                    $("#slugError").text("Slug must be unique.");
                }
            }
        });
    })
}
function cmsTableEvents() {
    document.querySelectorAll("[data-edit]").forEach((edit) => {
        edit.addEventListener("click", () => {
            $.ajax({
                url: "/Admin/Cms/EditCMSPage",
                method: "GET",
                data: { cmsPageId: $(edit).data("edit") },
                success: (result) => {
                    partialContainer.html(result);
                    $.getScript("/js/tinymceCmsScript.js");
                    cmsFormEvents("/Admin/Cms/EditCMSPage", "PUT");
                },
                error: (error) => {
                    console.log(error);
                    simpleAlert("Something went wrong!", "error");
                }
            });
        })
    });
    document.querySelectorAll("[data-delete]").forEach((del) => {
        del.addEventListener("click", () => {
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to retrieve it back!",
                icon: 'question',
                showDenyButton: true,
                showCancelButton: true,
                confirmButtonText: 'Yes, Delete',
                denyButtonText: `Deactivate Instead`,
                confirmButtonColor: '#3085d6',
                denyButtonColor: '#5cb85c'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: "/Admin/Cms/DeleteCMSPage",
                        method: "DELETE",
                        data: { cmsPageId: $(del).data("delete") },
                        success: (result) => {
                            simpleAlert("Successfully deleted the page!", "success")
                            partialContainer.html(result);
                            createPagination();
                            addAllEvents("cms");
                        },
                        error: (error) => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                } else if (result.isDenied) {
                    $.ajax({
                        url: "/Admin/Cms/DeactivateCMSPage",
                        method: "PUT",
                        data: { cmsPageId: $(del).data("delete") },
                        success: (result) => {
                            simpleAlert("Successfully deactivated the page!", "success")
                            partialContainer.html(result);
                            createPagination();
                            addAllEvents("cms");
                        },
                        error: (error) => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                }
            })
        })
    });
}

//load function
function loadIntialPartial(url, action) {
    tinymce.remove("textarea#tiny");
    $.ajax({
        url: url,
        method: "GET",
        success: (result) => {
            partialContainer.html(result);
            if (action == "banner")
                createPagination(4);
            createPagination();
            addAllEvents(action)
        },
        error: (error) => {
            console.log(error);
            simpleAlert("Something went wrong!", "error");
        }
    });
}

//search function
const searchBar = debounce((url, query, action) => {
    $.ajax({
        url: url,
        method: "GET",
        data: { query },
        success: (result) => {
            $(".spinner-border").removeClass("opacity-1");
            $(".spinner-border").addClass("opacity-0");
            partialContainer.html(result);
            createPagination();
            $('#adminSearch').val(query);
            $("#adminSearch").focus();
            addAllEvents(action)
        },
        error: error => {
            console.log(error);
            simpleAlert("Something went wrong!", "error");
        }
    });
});

//all page events
function addAllEvents(action) {
    switch (action) {
        case "cms":
            addCmsEvents();
            cmsTableEvents();
            break;
        case "user":
            addUserEvents();
            break;
        case "theme":
            addThemeEvents();
            themeTableEvents();
            break;
        case "skill":
            addSkillEvents();
            skillTableEvents();
            break;
        case "contact":
            addContactEvents();
            contactTableEvents();
            break;
        case "banner":
            addBannerEvents();
            bannerTableEvents();
            break;
        case "timesheet":
            addTimesheetEvents();
            timesheetTableEvents();
            break;
        case "mission":
            break;
        case "story":
            addStoryEvents();
            storyTableEvents();
            break;
        case "application":
            break;
    }
}

//debounce function
function debounce(cb, delay = 800) {
    let timeout;
    return (...arg) => {
        clearTimeout(timeout);
        timeout = setTimeout(() => {
            cb(...arg);
        }, delay);
    }
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