$(".explore-dropdown").removeClass("d-none");
if ($("#profileSuccess").val() == "true") {
    Swal.fire({
        position: 'top-end',
        icon: 'success',
        title: 'Successfully updated the profile!',
        showConfirmButton: false,
        timer: 1500
    })
}

let currentView = "grid";
const filterToggleBtn = document.querySelector(".filter-toggle-btn");
const filterMenu = document.querySelector(".filter-menu");
filterToggleBtn.addEventListener('click', () => {
    filterMenu.classList.toggle("show-filter-menu");
})
$('.grid-btn').on('click', () => {
    currentView = "grid";
    $(".index-list-view").removeClass("d-grid");
    $(".index-list-view").addClass("d-none");
    $('.grid-btn').addClass("active-index-view");
    $('.list-btn').removeClass("active-index-view");
    $(".index-grid-view").removeClass("d-none");
    $(".index-grid-view").addClass("d-flex");
})
$('.list-btn').on('click', () => {
    currentView = "list";
    $(".index-grid-view").removeClass("d-flex");
    $(".index-grid-view").addClass("d-none");
    $('.list-btn').addClass("active-index-view");
    $('.grid-btn').removeClass("active-index-view");
    $(".index-list-view").removeClass("d-none");
    $(".index-list-view").addClass("d-grid");
})

const onFilterList = document.querySelector(".on-filter-list");
let city = [], country = [], skill = [], theme = [], search = "", sort = 0;
let isClearAllExists = false;
const pagination = document.querySelector("#pagination");
let page = 1, totalPages = 0, pageSet = 1;

$(document).ready(() => {
    $.ajax({
        url: `/Volunteer/Home/FilterMissions`,
        type: "POST",
        success: (result) => {
            $('#partialViewContainer').html(result);
            const totalMissions = $('#totalMissions').val();
            $('.total-missions').text(totalMissions);
            createPaginationHTML(totalMissions);
            handleIndexFavouriteMissions();
            handleIndexRecommendMission();
            handleCancelMission();
            $('.index-list-view').addClass("d-none");
        }
    })
});
function handleCancelMission() {
    document.querySelectorAll("[data-cancelmission]").forEach(cancel => {
        $(cancel).click(() => {
            console.log("hell")
            Swal.fire({
                title: 'Are you sure?',
                icon: 'question',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, Cancel!'
            }).then((result) => {
                if (result.isConfirmed) {
                    $.ajax({
                        url: "/Volunteer/Mission/CancelMission",
                        method: "POST",
                        data: {missionId: $(cancel).data("cancelmission"), userId: $("#userId").val()},
                        success: _ => {
                            MakeAjaxCall();
                        },
                        error: error => {
                            console.log(error);
                            simpleAlert("Something went wrong!", "error");
                        }
                    });
                }
            })
        });
    });
}
function debounce(cb, delay = 800) {
    let timeout;
    return (...arg) => {
        clearTimeout(timeout);
        timeout = setTimeout(() => {
            cb(...arg);
        }, delay);
    }
}
const searchMission = debounce((query) => {
    search = query;
    page = 1;
    MakeAjaxCall();
    window.scrollTo(0, 0);
});
const query = document.querySelector("#Search");
$(query).on('input', () => {
    $(".spinner-border").removeClass("opacity-0");
    $(".spinner-border").addClass("opacity-1");
    searchMission($(query).val());
})
$("#sortDropdown li").on("click", (e) => {
    const id = e.currentTarget.getAttribute('data-id');
    if (sort == id) return;
    sort = id;
    page = 1;
    MakeAjaxCall();
    window.scrollTo(0, 0);
})
$("#exploreDropdown li").on("click", (e) => {
    const id = e.currentTarget.getAttribute('data-id');
    if (sort == id && +id != 10) return;
    sort = id;
    page = 1;
    $(".btn-close").click();
    MakeAjaxCall();
    window.scrollTo(0, 0);
})
$("#themeDropdown li").on("click", (e) => {
    const id = e.currentTarget.getAttribute('data-id');
    if (theme.includes(+id)) return;
    theme.push(+id);
    createFilterHTML("Theme", e.currentTarget.textContent, id);
    page = 1;
    MakeAjaxCall();
    window.scrollTo(0, 0);
})
$("#skillDropdown li").on("click", (e) => {
    const id = e.currentTarget.getAttribute('data-id');
    if (skill.includes(+id)) return;
    skill.push(+id);
    createFilterHTML("Skill", e.currentTarget.textContent, id);
    page = 1;
    MakeAjaxCall();
    window.scrollTo(0, 0);
})
$("#countryDropdown li").on("click", (e) => {
    const id = e.currentTarget.getAttribute('data-id');
    if (country.includes(id)) return;
    country.push(id);
    document.querySelectorAll('#cityDropdown li').forEach((li) => {
        if (!country.includes(li.getAttribute('data-country'))) {
            $(li).addClass("d-none");
        }
        else {
            $(li).removeClass("d-none");
        }
    })
    createFilterHTML("Country", e.currentTarget.textContent, id);
    page = 1;
    MakeAjaxCall();
    window.scrollTo(0, 0);
})
$("#cityDropdown li").on("click", (e) => {
    const id = e.currentTarget.getAttribute('data-id');
    if (city.includes(+id)) return;
    const countryId = e.currentTarget.getAttribute('data-country');
    if (country == 0) {
        document.querySelectorAll('#countryDropdown li').forEach((li) => {
            if ($(li).data('id') != countryId)
                $(li).addClass("d-none");
        })
        document.querySelectorAll('#cityDropdown li').forEach((li) => {
            if ($(li).data('country') != countryId)
                $(li).addClass("d-none")
        })
    }
    city.push(+id);
    createFilterHTML("City", e.currentTarget.textContent, id);
    page = 1;
    MakeAjaxCall();
    window.scrollTo(0, 0);
})
function createFilterHTML(type, value, id) {
    if (!isClearAllExists) {
        onFilterList.classList.add("mb-4");
        onFilterList.innerHTML = `
            <div class="clear-all my-auto color-darkgray fw-light text-14 cursor-pointer">
                Clear all
            </div>
        `;
        isClearAllExists = true;
        $(".clear-all").click(() => {
            onFilterList.innerHTML = "";
            onFilterList.classList.remove("mb-4");
            document.querySelectorAll('#countryDropdown li').forEach((li) => {
                $(li).removeClass("d-none");
            })
            document.querySelectorAll('#cityDropdown li').forEach((li) => {
                $(li).removeClass("d-none");
            })
            page = 1;
            city = [];
            skill = [];
            theme = [];
            country = [];
            isClearAllExists = false;
            MakeAjaxCall();
            window.scrollTo(0, 0);
        })
    }
    $(`<div
        class="on-filter-item border rounded-pill d-flex align-items-center gap-2 py-1 px-2 color-darkgray fw-light text-14">
        <span>${value}</span>
        <img src="/images/static/cancel.png" data-value="${value}" data-type=${type} alt="cancel" class="cursor-pointer">
        </div>
    `).insertBefore('.clear-all');

    $(`[data-value = "${value}"]`).click((event) => {
        HandleFilterRemove(event, event.currentTarget.getAttribute("data-type"), id);
    });
}
function HandleFilterRemove(event, type, id) {
    onFilterList.removeChild(event.currentTarget.parentElement)

    if (type == "City") {
        city.splice(city.indexOf(+id), 1);
    }
    if (type == "Country") {

        country.splice(country.indexOf(id), 1);

        document.querySelectorAll('#cityDropdown li').forEach((li) => {
            if (country.includes(li.getAttribute('data-country')))
                $(li).removeClass("d-none");
            else {
                $(li).addClass("d-none");
                document.querySelectorAll(`[data-type='${"City"}']`).forEach((onFilter) => {
                    const c = $(onFilter).data("value");
                    if (c == $(li).text()) {
                        $(onFilter).parent().remove();
                        city.splice(city.indexOf($(li).data("id")), 1);
                    }
                })
            }
        })
    }
    if (type == "Theme")
        theme.splice(theme.indexOf(+id), 1);
    if (type == "Skill")
        skill.splice(skill.indexOf(+id), 1);
    if (city.length == 0 && skill.length == 0 && theme.length == 0 && country.length == 0) {
        $(".clear-all").click();
        return;
    }
    MakeAjaxCall();
    window.scrollTo(0, 0);
}
function MakeAjaxCall() {
    let obj = {
        country: country,
        city: city,
        theme: theme,
        skill: skill,
        search: search,
        sort: sort,
        page: page
    };
    $.ajax({
        url: `/Volunteer/Home/FilterMissions`,
        type: "POST",
        data: obj,
        success: (result) => {
            $('#partialViewContainer').html('');
            $(".spinner-border").removeClass("opacity-1");
            $(".spinner-border").addClass("opacity-0");
            $('#partialViewContainer').html(result);
            if (currentView == "grid") {
                $(".index-list-view").addClass("d-none");
                $(".index-grid-view").removeClass("d-none");
            } else {
                $(".index-list-view").removeClass("d-none");
                $(".index-grid-view").addClass("d-none");
            }
            const totalMissions = $('#totalMissions').val();
            $('.total-missions').text(totalMissions);
            createPaginationHTML(totalMissions);
            if (totalMissions == 0 && sort != 5) {
                $(".index-top").addClass("d-none");
                $(".no-mission").removeClass("d-none");
            } else {
                $(".index-top").removeClass("d-none");
                $(".no-mission").addClass("d-none");
            }
            handleIndexFavouriteMissions();
            handleIndexRecommendMission();
            handleCancelMission();
        }
    })
}

function createPaginationHTML(totalMissions) {
    if (totalPages == Math.ceil(totalMissions / 9)) return;
    pagination.innerHTML = "";
    totalPages = Math.ceil(totalMissions / 9);
    if (totalPages > 5) {
        pagination.innerHTML = `<button data-page="previous" class="btn rounded border border-2">
        <img src="/images/static/previous.png" alt="previous">
        </button>`;
    }
    if (totalPages > 1) {
        pagination.innerHTML += `<button data-page="left" class="btn rounded border border-2">
        <img src="/images/static/left.png" alt="left"></button>`;
        for (let i = 1; i <= totalPages; i++) {
            pagination.innerHTML += `<button data-page="${i}"
             class="btn rounded border border-2 shadow-sm color-darkgray fw-light">${i}</button>`;
        }
        pagination.innerHTML += `<button data-page="right" class="btn rounded border border-2">
        <img src="/images/static/right-arrow1.png" alt="right"></button>`;
    }
    if (totalPages > 5) {
        pagination.innerHTML += `<button data-page="next" class="btn rounded border border-2">
        <img src="/images/static/next.png" alt="next">
        </button>`;
    }
    //event listner
    document.querySelectorAll('[data-page]').forEach((btn) => {
        $(btn).click(() => handlePagination($(btn).data('page')));

        if ($(btn).data('page') <= 5 * (pageSet - 1) || $(btn).data('page') > pageSet * 5)
            $(btn).addClass("d-none")
    });

    // css to current page
    $(`[data-page=${page}]`).addClass('active-page');
}
function scrollPageSet() {
    document.querySelectorAll('[data-page]').forEach((btn) => {
        if ($(btn).data('page') <= 5 * (pageSet - 1) || $(btn).data('page') > pageSet * 5)
            $(btn).addClass("d-none")
        else
            $(btn).removeClass("d-none");
    });
}
function handlePagination(value) {
    if (value == 'next') {
        if (pageSet * 5 > totalPages) return;
        pageSet += 1;
        scrollPageSet();
        return;
    }
    else if (value == 'previous') {
        if (pageSet == 1) return;
        pageSet -= 1;
        scrollPageSet();
        return;
    }
    else if (value == 'left') {
        if (page == 1) return;
        $(`[data-page=${page}]`).removeClass('active-page');
        page = page - 1;
        $(`[data-page=${page}]`).addClass('active-page');
        if (page <= (pageSet - 1) * 5) {
            pageSet -= 1;
            scrollPageSet();
        }
    }
    else if (value == 'right') {
        if (page == totalPages) return;
        $(`[data-page=${page}]`).removeClass('active-page');
        page = page + 1;
        $(`[data-page=${page}]`).addClass('active-page');
        if (page > pageSet * 5) {
            pageSet += 1;
            scrollPageSet();
        }
    }
    else {
        if (page == value) {
            window.scrollTo(0, 0);
            return;
        }
        $(`[data-page=${page}]`).removeClass('active-page');
        page = value;
        $(`[data-page=${page}]`).addClass('active-page');
    }
    MakeAjaxCall();
    window.scrollTo(0, 0);
}
function handleIndexFavouriteMissions() {

    const userId = document.querySelector("#userId").value;
    Array.from(document.querySelectorAll(".add-favourite")).forEach(fav => {
        $(fav).click(() => {
            if (userId == null || userId == "") {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'You need to Login to add this mission as your favourite!',
                    footer: '<a href="/volunteer/user/login">Login here</a>'
                })
                return;
            }
            $.ajax({
                url: "/Volunteer/Mission/ToggleFavouriteMission",
                method: "POST",
                data: { missionId: $(fav).data("missionid"), userId: userId, isFavourite: false },
                success: (_) => {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Succesfully added to Favourite missions!',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    MakeAjaxCall();
                },
                error: (error) => {
                    console.log(error);
                }
            });
        })
    })

    Array.from(document.querySelectorAll(".remove-favourite")).forEach(fav => {
        $(fav).click(() => {
            $.ajax({
                url: "/Volunteer/Mission/ToggleFavouriteMission",
                method: "POST",
                data: { missionId: $(fav).data("missionid"), userId: userId, isFavourite: true },
                success: (_) => {
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Succesfully removed from Favourite missions!',
                        showConfirmButton: false,
                        timer: 1500
                    })
                    MakeAjaxCall();
                },
                error: (error) => {
                    console.log(error);
                }
            });
        })
    })
}

function handleIndexRecommendMission() {
    Array.from(document.querySelectorAll("#recommendBtn")).forEach(recBtn => {
        $(recBtn).click(() => {
            const userId = document.querySelector("#userId").value;
            if (userId == null || userId == "") {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'You need to Login to recommend your co-workers!',
                    footer: '<a href="/volunteer/user/login">Login here</a>'
                })
                return;
            }
            $.ajax({
                url: "/Volunteer/User/get-users-to-recommend",
                method: "GET",
                data: { missionId: $(recBtn).data("missionid"), userId },
                success: (result) => {
                    $("#partialRecommendContainer").html(result);
                    $("#exampleModal").modal('show');
                    $('#modalRecommendBtn').click(() => {
                        const checkedInputs = Array.from(document.querySelectorAll(".form-check-input:checked"));
                        if (checkedInputs.length == 0) {
                            Swal.fire({
                                icon: 'error',
                                title: 'Oops...',
                                text: 'Please select at least one user to recommend!',
                            })
                            return;
                        }
                        $("#exampleModal").modal('hide');
                        const userList = [];
                        checkedInputs.forEach((checked) => {
                            userList.push(+$(checked).val());
                        })
                        $.ajax({
                            url: "/Volunteer/Mission/RecommendMission",
                            data: { missionId: $(recBtn).data("missionid"), userId, toUsers: userList },
                            method: "POST",
                            success: (result) => {
                                $("#partialRecommendContainer").html(result);
                                Swal.fire({
                                    position: 'top-end',
                                    icon: 'success',
                                    title: 'Successfully recommended the mission!',
                                    showConfirmButton: false,
                                    timer: 1500
                                })
                            },
                            error: (error) => {
                                console.log(error)
                            }
                        });
                    })
                },
                error: (error) => {
                    console.log(error);
                }
            });
        })
    })
}