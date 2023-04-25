const partialContainer = $('#partialAdminPageContainer');
const partialModalContainer = $('#partialModalContainer');
let currentSidebarItem = document.querySelector("[data-item='cms']");
const sidebarItems = document.querySelectorAll("[data-item]");

loadInitialPartial("/Admin/Cms/LoadCmsPage", "cms");
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
            window.scroll(0, 0);
            switch (item.dataset.item) {
                case "user":
                    loadInitialPartial("/Admin/User/LoadUserPage", item.dataset.item);
                    break;
                case "cms":
                    loadInitialPartial("/Admin/Cms/LoadCmsPage", item.dataset.item);
                    break;
                case "mission":
                    loadInitialPartial("/Admin/Mission/LoadMissionPage", item.dataset.item);
                    break;
                case "theme":
                    loadInitialPartial("/Admin/Theme/LoadThemePage", item.dataset.item);
                    break;
                case "skill":
                    loadInitialPartial("/Admin/Skill/LoadSkillPage", item.dataset.item);
                    break;
                case "application":
                    loadInitialPartial("/Admin/Application/LoadApplicationPage", item.dataset.item);
                    break;
                case "timesheet":
                    loadInitialPartial("/Admin/Timesheet/LoadTimesheetPage", item.dataset.item);
                    break;
                case "story":
                    loadInitialPartial("/Admin/Story/LoadStoryPage", item.dataset.item);
                    break;
                case "banner":
                    loadInitialPartial("/Admin/Banner/LoadBannerPage", item.dataset.item);
                    break;
                case "contact":
                    loadInitialPartial("/Admin/Contact/LoadContactPage", item.dataset.item);
                    break;
            }
        })
    })
}

let files = [];
let documents = [];
function validateMissionForm() {
    let error = false;

    if (tinymce.get("tiny").getContent() == null || tinymce.get("tiny").getContent() == "") {
        $('#descriptionError').text("The description field is required.");
        error = true;
    } else
        $('#descriptionError').text("");

    if (files.length == 0) {
        $('#mediaError').text("Please upload at least one image.");
        error = true;
    } else
        $('#mediaError').text("");

    const skills = $('#MissionSkills option:selected').length;
    if (skills == 0) {
        $('#skillError').text("Please select at least one skill.");
        error = true;
    } else {
        $('#skillError').text("");
    }

    const startDate = $("#StartDate").val();
    const endDate = $("#EndDate").val();
    if (startDate != null && endDate != null && startDate >= endDate) {
        document.querySelector("#StartDateError").textContent = "Start date can't be sooner than end date!";
        error = true;
    } else {
        document.querySelector("#StartDateError").textContent = "";
    }

    const regDead = new Date($("#RegistrationDeadline").val());
    if (regDead != null && regDead < new Date()) {
        $("#RegistrationDeadlineError").text("Invalid registration deadline!");
        error = true;
    } else {
        $("#RegistrationDeadlineError").text("");
    }
    return error;
}
function setImageInput() {
    let myFileList = new DataTransfer();
    files.forEach(function (file) {
        myFileList.items.add(file);
    });
    document.querySelector("#ImagesInput").files = myFileList.files;
}
function setDocsInput() {
    let myFileList = new DataTransfer();
    documents.forEach(function (file) {
        myFileList.items.add(file);
    });
    document.querySelector("#DocumentsInput").files = myFileList.files;
}
function addMissionEvents() {
    $("#addTimeBtn").click(() => {
        $.ajax({
            url: "/Admin/Mission/AddTimeMission",
            success: (result) => {
                partialContainer.html(result);
                $.getScript("/js/tinymceScript.js");
                addMissionFormEvents("/Admin/Mission/AddTimeMission", "post", (_) => {
                    simpleAlert("Successfully added the mission!", "success");
                    loadInitialPartial("/Admin/Mission/LoadMissionPage", "mission");
                });
            },
            error: (error) => {
                console.log(error);
                simpleAlert("Something went wrong!", "error");
            }
        });
    })
    $("#addGoalBtn").click(() => {
        $.ajax({
            url: "/Admin/Mission/AddGoalMission",
            success: (result) => {
                partialContainer.html(result);
                $.getScript("/js/tinymceScript.js");
                addMissionFormEvents("/Admin/Mission/AddGoalMission", "post", (_) => {
                    simpleAlert("Successfully added the mission!", "success");
                    loadInitialPartial("/Admin/Mission/LoadMissionPage", "mission");
                });
            },
            error: (error) => {
                console.log(error);
                simpleAlert("Something went wrong!", "error");
            }
        });
    })
}
function missionTableEvents() {
    document.querySelectorAll("[data-edit]").forEach((edit) => {
        edit.addEventListener("click", () => {
            const type = edit.getAttribute("data-type");
            let url = '';
            if (type == "TIME") {
                url = "/Admin/Mission/EditTimeMission";
            } else {
                url = "/Admin/Mission/EditGoalMission";
            }
            $.ajax({
                url: url,
                method: "GET",
                data: { id: $(edit).data("edit") },
                success: (result) => {
                    partialContainer.html(result);
                    $.getScript("/js/tinymceScript.js");
                    if (type == "TIME") {
                        addMissionFormEvents("/Admin/Mission/EditTimeMission", "put", (_) => {
                            simpleAlert("Successfully updated the mission!", "success");
                            loadInitialPartial("/Admin/Mission/LoadMissionPage", "mission");
                        });
                    } else {
                        addMissionFormEvents("/Admin/Mission/EditGoalMission", "put", (_) => {
                            simpleAlert("Successfully updated the mission!", "success");
                            loadInitialPartial("/Admin/Mission/LoadMissionPage", "mission");
                        });
                    }
                },
                error: (error) => {
                    console.log(error);
                    simpleAlert("Something went wrong!", "error");
                }
            });
        })
    });
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
                        url: "/Admin/Mission/UpdateStatus",
                        method: "PUT",
                        data: { id: $(act).data("activate"), value: 1 },
                        success: (_) => {
                            simpleAlert("Successfully activated the mission!", "success");
                            loadInitialPartial("/Admin/Mission/LoadMissionPage", "mission");
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
                        url: "/Admin/Mission/UpdateStatus",
                        method: "PUT",
                        data: { id: $(act).data("deactivate"), value: 0 },
                        success: (_) => {
                            simpleAlert("Successfully deactivated the mission!", "success");
                            loadInitialPartial("/Admin/Mission/LoadMissionPage", "mission");
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
function addMissionFormEvents(url, method, successCB) {
    files = [];
    documents = [];
    handleImagesEvents(true);
    handleDropdownEvents();
    $("#cancelBtn").click(() => loadInitialPartial("/Admin/Mission/LoadMissionPage", "mission"))
    $("#MissionForm").on("submit", (e) => {
        e.preventDefault();

        const isValid = $("#MissionForm").valid();
        if (validateMissionForm() || !isValid) return;

        setImageInput();
        const formData = new FormData($("#MissionForm")[0])
        formData.set("Description", tinymce.get("tiny").getContent());
        $.ajax({
            url: url,
            method: method,
            data: formData,
            processData: false,
            contentType: false,
            success: successCB,
            error: error => {
                console.log(error);
                simpleAlert("Something went wrong!", "error");
            }
        });
    })
}
function handleDropdownEvents() {
    if ($('countryDropdown').val() != 0) {
        const countryId = $("#countryDropdown").val();
        document.querySelectorAll('#cityDropdown option').forEach(city => {
            if ($(city).data('id') == countryId) {
                $(city).removeClass("d-none");
            } else {
                $(city).addClass("d-none");
            }
        })
    }
    $('#countryDropdown').on("change", e => {
        if ($("#cityDropdown").val() != e.currentTarget.value) {
            $('#cityDropdown').val(0);
        }
        document.querySelectorAll('#cityDropdown option').forEach(city => {
            if ($(city).data('id') == e.currentTarget.value) {
                $(city).removeClass("d-none");
            } else {
                $(city).addClass("d-none");
            }
        })
    })
}
function handleImagesEvents(isEdit) {
    const dragarea = document.querySelector(".dragarea");
    const imagesInput = document.querySelector("#ImagesInput");
    const selectedImages = document.querySelector(".selected-images");
    dragarea.addEventListener("click", () => imagesInput.click())

    imagesInput.addEventListener("change", () => {
        let file = imagesInput.files;
        // if user select no image
        if (file.length == 0) return;

        for (let i = 0; i < file.length; i++) {
            if (file[i].type.split("/")[0] != "image") continue;
            if (!files.some((e) => e.name == file[i].name)) {
                files.push(file[i]);
                showImage(URL.createObjectURL(file[i]), files.length - 1)
            }
        }
        addEvents();
    });
    dragarea.addEventListener("dragover", e => e.preventDefault());
    dragarea.addEventListener("dragenter", e => e.preventDefault());
    dragarea.addEventListener("drop", e => {
        e.preventDefault();
        let file = e.dataTransfer.files;
        for (let i = 0; i < file.length; i++) {
            /** Check selected file is image */
            if (file[i].type.split("/")[1] != "png" && file[i].type.split("/")[1] != "jpg" && file[i].type.split("/")[1] != "jpeg") continue;
            if (!files.some((e) => e.name == file[i].name)) {
                files.push(file[i]);
                showImage(URL.createObjectURL(file[i]), files.length - 1)
            }
        }
        addEvents();
    });
    function showImage(src, index) {
        selectedImages.innerHTML += `<div class="position-relative">
        <img src="${src}" alt="story image" class="object-fit-cover" height="90px" width="90px" />
        <div class="position-absolute top-0 end-0 p-1 bg-dark cursor-pointer" data-img="${index}">
          <img src="/images/static/cross.png" alt="cross" height="10px"/>
        </div>`;
    }
    function resetData() {
        Array.from(document.querySelectorAll(`[data-img]`)).forEach((element, i) => {
            $(element).data("img", i);
        })
    }
    function addEvents() {
        Array.from(document.querySelectorAll(`[data-img]`)).forEach(img => {
            $(img).click(() => {
                files.splice($(img).data('img'), 1)
                $(img).parent().remove();
                resetData();
            })
        });
    }

    documentsInput = document.querySelector("#DocumentsInput");
    selectedDocuments = document.querySelector(".selected-documents");
    documentsInput.addEventListener("change", () => {
        selectedDocuments.innerHTML = '';
        const documents = documentsInput.files;
        for (let i = 0; i < documentsInput.files.length; i++) {
            selectedDocuments.innerHTML += `<a target="_blank" href="${URL.createObjectURL(documents[i])}"
                       class="btn border border-dark rounded-pill p-2 d-flex align-items-center gap-2 text-15">${documents[i].name}</a>`
        }
    })

    if (isEdit) {
        async function fetchAndCreateImages() {
            const images = Array.from(document.querySelectorAll('[data-path]'));
            for (let i = 0; i < images.length; i++) {
                const image = images[i];
                const fileName = image.value;
                const url = $(image).data("path");
                const type = $(image).data("type");

                const response = await fetch(url);
                const buffer = await response.arrayBuffer();
                const myFile = new File([buffer], fileName, { type: `image/${type.slice(1)}` });
                files.push(myFile);

                showImage(url, i);
            }
            addEvents();
        }
        fetchAndCreateImages();

        let titles = [];
        async function fetchAndCreateFiles() {
            const docImages = Array.from(document.querySelectorAll('[data-doc]'));
            for (const image of docImages) {
                const fileName = image.value;
                const url = $(image).data("doc");
                const type = $(image).data("type");
                const title = $(image).data("title");

                const response = await fetch(url);
                const buffer = await response.arrayBuffer();
                const myFile = new File([buffer], fileName, { type: `image/${type.slice(1)}` });
                documents.push(myFile);
                titles.push(title);
            }
            setDocsInput();
            selectedDocuments.innerHTML = '';
            for (let i = 0; i < documents.length; i++) {
                selectedDocuments.innerHTML += `<a target="_blank" href="${URL.createObjectURL(documents[i])}"
                class="btn border border-dark rounded-pill p-2 d-flex align-items-center gap-2 text-15">${titles[i]}</a>`
            }
        }
        fetchAndCreateFiles();
    }
}

//application events
function applicationTableEvents() {
    document.querySelectorAll("[data-view]").forEach((view) => {
        view.addEventListener("click", () => {
            $.ajax({
                url: "/Admin/Application/ViewApplication",
                method: "GET",
                data: { id: $(view).data("view") },
                success: (result) => {
                    partialModalContainer.html(result);
                    $("#viewApplicationModal").modal("show");
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
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to revert it back!",
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#28a745',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Approve!'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: "/Admin/Application/UpdateStatus",
                        method: "PUT",
                        data: { id: $(accept).data("accept"), value: 1 },
                        success: (result) => {
                            partialContainer.html(result);
                            createPagination();
                            simpleAlert("Successfully approved the application!", "success");
                            addAllEvents("application");
                        },
                        error: (error) => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                }
            })
        });
    });
    document.querySelectorAll("[data-decline]").forEach((decline) => {
        decline.addEventListener("click", () => {
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to change it back!",
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Decline!'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: "/Admin/Application/UpdateStatus",
                        method: "PUT",
                        data: { id: $(decline).data("decline"), value: 2 },
                        success: (result) => {
                            partialContainer.html(result);
                            createPagination();
                            simpleAlert("Successfully declined the application!", "success");
                            addAllEvents("application");
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
    $("#hourBtn").click(() => {
        loadInitialPartial("/Admin/Timesheet/LoadTimesheetPage", "timesheet");
        addAllEvents("timesheet");
    });
    $("#goalBtn").click(() => {
        loadInitialPartial("/Admin/Timesheet/LoadGoalsheetPage", "timesheet")
        addAllEvents("timesheet");
    });
}
function timesheetTableEvents() {
    document.querySelectorAll("[data-decline]").forEach((decline) => {
        decline.addEventListener("click", () => {
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to revert it back!",
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
                        data: { id: $(decline).data("decline"), status: 2, isTime: $(decline).data("istime") },
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
            Swal.fire({
                title: 'Are you sure?',
                text: "You won't be able to revert it back!",
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#28a745',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Approve!'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: "/Admin/Timesheet/UpdateStatus",
                        method: "PUT",
                        data: { id: $(accept).data("accept"), status: 1, missionId: $(accept).data("missionid"), action: $(accept).data("action"), isTime: $(accept).data("istime") },
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
                }
            })
        })
    });
    document.querySelectorAll("[data-viewgoal]").forEach((view) => {
        view.addEventListener("click", () => {
            $.ajax({
                url: '/Admin/Timesheet/ViewGoal',
                method: "GET",
                data: { id: $(view).data("viewgoal") },
                success: (result) => {
                    partialModalContainer.html(result);
                    $("#viewGoalModal").modal("show");
                },
                error: (error) => {
                    console.log(error);
                    simpleAlert("Something went wrong!", "error");
                }
            });
        })
    });
    document.querySelectorAll("[data-viewtime]").forEach((view) => {
        view.addEventListener("click", () => {
            $.ajax({
                url: '/Admin/Timesheet/ViewTime',
                method: "GET",
                data: { id: $(view).data("viewtime") },
                success: (result) => {
                    partialModalContainer.html(result);
                    $("#viewTimeModal").modal("show");
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
function storyTableEvents() {
    document.querySelectorAll("[data-decline]").forEach((decline) => {
        decline.addEventListener("click", () => {
            Swal.fire({
                title: 'Are you sure?',
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
                url: "/Admin/Story/AcceptStory",
                method: "PUT",
                data: { id: $(restore).data("restore") },
                success: (result) => {
                    partialContainer.html(result);
                    createPagination();
                    simpleAlert("Successfully restored the story!", "success");
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
                text: "It will be deleted permenantly, You won't be able to retrieve it back!",
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
    $("#cancelBtn").click(() => loadInitialPartial("/Admin/Banner/LoadBannerPage", "banner"))
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
        if (!isValid) return;
        let formData = new FormData($("#bannerForm")[0]);
        formData.append("bannerImage", document.querySelector("#bannerImage").files[0]);
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
    $("#cancelBtn").click(() => loadInitialPartial("/Admin/Banner/LoadBannerPage", "banner"))

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
                            loadInitialPartial("/Admin/Contact/LoadContactPage", "contact")
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
        $("#contactReplyModal").modal("hide");
        $.ajax({
            url: "/Admin/Contact/ReplyMessage",
            method: "PUT",
            data: $("#contactForm").serialize(),
            success: (result) => {
                loadInitialPartial("/Admin/Contact/LoadContactPage", "contact")
                simpleAlert("Successfully replied to the user!", "success");
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
    $("#addBtn").click(() => {
        $.ajax({
            url: "/Admin/User/AddUser",
            success: (result) => {
                partialContainer.html(result);
                addUserFormEvents();
            },
            error: (error) => {
                console.log(error);
                simpleAlert("Something went wrong!", "error");
            }
        });
    })
}
function addUserFormEvents() {
    $("#cancelBtn").click(() => loadInitialPartial("/Admin/User/LoadUserPage", "user"))
    handleDropdownEvents();
    $("#profilePic").click(() => { $('#profileInput').click(); });
    $("#profileInput").on("change", () => {
        $("#profilePic").attr("src", URL.createObjectURL($("#profileInput").prop('files')[0]))
    })

    $("#UserForm").on("submit", (e) => {
        e.preventDefault();
        let isValid = $("#UserForm").valid();
        if (!isValid) return;
        Swal.fire({
            title: 'Are you sure?',
            icon: 'question',
            text: "You won't be able to change email and password later!",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, Submit!'
        }).then((result) => {
            if (result.isConfirmed) {
                $.ajax({
                    url: "/Admin/User/AddUser",
                    method: "POST",
                    data: new FormData($("#UserForm")[0]),
                    processData: false,
                    contentType: false,
                    success: (_, __, status) => {
                        if (status.status == 204) {
                            $("#emailError").text("User already exist with this email address!");
                            return;
                        }
                        UserEmailAjax($("#Email").val(), $("#Password").val());
                        simpleAlert("User added and mail send to email address!", "success");
                        loadInitialPartial("/Admin/User/LoadUserPage", "user");
                    },
                    error: (error) => {
                        console.log(error);
                        simpleAlert("Something went wrong!", "error");
                    }
                });
            }
        })
    })
}
function UserEmailAjax(email, password) {
    console.log(email, password)
    $.ajax({
        url: "/Admin/User/SendAccountMail",
        data: { email, password }
    });
}
function editUserFormEvents() {
    $("#cancelBtn").click(() => loadInitialPartial("/Admin/User/LoadUserPage", "user"))
    handleDropdownEvents();
    $("#profilePic").click(() => { $('#profileInput').click(); });
    $("#profileInput").on("change", () => {
        $("#profilePic").attr("src", URL.createObjectURL($("#profileInput").prop('files')[0]))
    })

    $("#UserForm").on("submit", (e) => {
        e.preventDefault();
        let isValid = $("#UserForm").valid();
        if (!isValid) return;
        $.ajax({
            url: "/Admin/User/EditUser",
            method: "PUT",
            data: new FormData($("#UserForm")[0]),
            processData: false,
            contentType: false,
            success: (_) => {
                simpleAlert("User updated successfully!", "success");
                loadInitialPartial("/Admin/User/LoadUserPage", "user");
            },
            error: (error) => {
                console.log(error);
                simpleAlert("Something went wrong!", "error");
            }
        });
    })
}
function userTableEvents() {
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
    document.querySelectorAll("[data-edit]").forEach((edit) => {
        edit.addEventListener("click", () => {
            $.ajax({
                url: "/Admin/User/EditUser",
                method: "GET",
                data: { id: $(edit).data("edit") },
                success: (result) => {
                    partialContainer.html(result);
                    editUserFormEvents();
                },
                error: (error) => {
                    console.log(error);
                    simpleAlert("Something went wrong!", "error");
                }
            });
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
}
function cmsFormEvents(url, method) {
    $("#cancelBtn").click(() => loadInitialPartial("/Admin/Cms/LoadCmsPage", "cms"))
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
function loadInitialPartial(url, action) {
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

//search event
function searchEvent(url, action) {
    $("#adminSearch").on("input", () => {
        $(".spinner-border").removeClass("opacity-0");
        $(".spinner-border").addClass("opacity-1");
        const query = $('#adminSearch').val();
        searchBar(url, query, action);
    })
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
            $(".spinner-border").removeClass("opacity-1");
            $(".spinner-border").addClass("opacity-0");
            simpleAlert("Something went wrong!", "error");
        }
    });
});

//all page events
function addAllEvents(action) {
    switch (action) {
        case "cms":
            searchEvent("/Admin/Cms/SearchCmsPage", action);
            addCmsEvents();
            cmsTableEvents();
            break;
        case "user":
            searchEvent("/Admin/User/SearchUser", action);
            addUserEvents();
            userTableEvents();
            break;
        case "theme":
            searchEvent("/Admin/Theme/SearchTheme", action);
            addThemeEvents();
            themeTableEvents();
            break;
        case "skill":
            searchEvent("/Admin/Skill/SearchSkill", action);
            addSkillEvents();
            skillTableEvents();
            break;
        case "contact":
            searchEvent("/Admin/Contact/SearchContact", action);
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
            searchEvent("/Admin/Mission/SearchMission", action);
            addMissionEvents();
            missionTableEvents();
            break;
        case "story":
            searchEvent("/Admin/Story/SearchStory", action);
            storyTableEvents();
            break;
        case "application":
            searchEvent("/Admin/Application/SearchApplication", action);
            applicationTableEvents();
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