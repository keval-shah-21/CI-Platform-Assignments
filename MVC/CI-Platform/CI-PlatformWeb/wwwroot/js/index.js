const filterToggleBtn = document.querySelector(".filter-toggle-btn");
const filterMenu = document.querySelector(".filter-menu");
filterToggleBtn.addEventListener('click', () => {
    filterMenu.classList.toggle("show-filter-menu");
})
const gridBtn = document.querySelector(".grid-btn");
const listBtn = document.querySelector(".list-btn");
const gridView = document.querySelector(".index-grid-view");
const listView = document.querySelector(".index-list-view");
gridBtn.addEventListener('click', () => {
    listView.classList.remove("d-grid");
    listView.classList.add("d-none");
    gridBtn.classList.add("active-index-view");
    listBtn.classList.remove("active-index-view");
    gridView.classList.remove("d-none");
    gridView.classList.add("d-flex");
})
listBtn.addEventListener('click', () => {
    gridView.classList.remove("d-flex");
    gridView.classList.add("d-none");
    listBtn.classList.add("active-index-view");
    gridBtn.classList.remove("active-index-view");
    listView.classList.remove("d-none");
    listView.classList.add("d-grid");
})

const onFilterList = document.querySelector(".on-filter-list");
let city = [], country = 0, skill = [], theme = [], search = "", sort = "", isClearAllExists = false;

const query = document.querySelector("#Search");
$(query).on('input', () => {
    search = $(query).val();
    MakeAjaxCall();
})
$("#themeDropdown li").on("click", (e) => {
    const id = e.currentTarget.getAttribute('data-id');
    if (theme.includes(id)) return;
    theme.push(+id);
    createFilterHTML("Theme", e.currentTarget.textContent, id);
    MakeAjaxCall();
})
$("#skillDropdown li").on("click", (e) => {
    const id = e.currentTarget.getAttribute('data-id');
    if (skill.includes(id)) return;
    skill.push(+id);
    createFilterHTML("Skill", e.currentTarget.textContent, id);
    MakeAjaxCall();
})
$("#countryDropdown li").on("click", (e) => {
    const id = e.currentTarget.getAttribute('data-id');
    if (country == id) return;
    country = id;
    document.querySelectorAll('#cityDropdown li').forEach((li) => {
        if ($(li).data('country') != country)
            $(li).addClass("d-none");
    })
    document.querySelectorAll('#countryDropdown li').forEach((li) => {
        if ($(li).data('id') != id)
            $(li).addClass("d-none")
    })
    createFilterHTML("Country", e.currentTarget.textContent, id);
    MakeAjaxCall();
})
$("#cityDropdown li").on("click", (e) => {
    const id = e.currentTarget.getAttribute('data-id');
    if (city.includes(id)) return;
    const countryId = e.currentTarget.getAttribute('data-country');
    if (country == 0) {
        document.querySelectorAll('#countryDropdown li').forEach((li) => {
            if ($(li).data('id') != countryId)
                $(li).addClass("d-none");
        })
    }
    document.querySelectorAll('#cityDropdown li').forEach((li) => {
        if ($(li).data('country') != countryId)
            $(li).addClass("d-none")
    })
    city.push(+id);
    createFilterHTML("City", e.currentTarget.textContent, id);
    MakeAjaxCall();
})
function createFilterHTML(type, value, id) {
    if (!isClearAllExists) {
        onFilterList.innerHTML = `
            <div class="clear-all my-auto color-darkgray fw-light text-14 cursor-pointer">
                Clear all
            </div>
        `;
        isClearAllExists = true;
        $(".clear-all").click(() => {
            onFilterList.innerHTML = "";
            document.querySelectorAll('#countryDropdown li').forEach((li) => {
                $(li).removeClass("d-none");
            })
            document.querySelectorAll('#cityDropdown li').forEach((li) => {
                $(li).removeClass("d-none");
            })
            city = [];
            skill = [];
            theme = [];
            country = 0;
            isClearAllExists = false;
            MakeAjaxCall();
        })
    }
    $(`<div
         class="on-filter-item border rounded-pill d-flex align-items-center gap-2 py-1 px-2 color-darkgray fw-light text-14">
         <span>${value}</span>
         <img src="/images/static/cancel.png" data-id="${value}" data-type=${type} alt="cancel" class="cursor-pointer">
         </div>
     `).insertBefore('.clear-all');

    $(`[data-id = "${value}"]`).click((e) => {

        onFilterList.removeChild(e.currentTarget.parentElement)

        if (e.currentTarget.getAttribute("data-type") == "City") {
            city.splice(city.indexOf(+id), 1);
        }
        if (e.currentTarget.getAttribute("data-type") == "Country") {
            country = 0;
            city = []
            document.querySelectorAll('#countryDropdown li').forEach((li) => {
                $(li).removeClass("d-none");
            })
            document.querySelectorAll('#cityDropdown li').forEach((li) => {
                $(li).removeClass("d-none");
            })
            document.querySelectorAll(`[data-type='${"City"}']`).forEach((li) => {
                $(li).parent().remove();
            })
        }
        if (e.currentTarget.getAttribute("data-type") == "Theme")
            theme.splice(theme.indexOf(+id), 1);
        if (e.currentTarget.getAttribute("data-type") == "Skill")
            skill.splice(skill.indexOf(+id), 1);
        if (city.length == 0 && skill.length == 0 && theme.length == 0 && country == 0) {
            $(".clear-all").click();
        }
        MakeAjaxCall();
    });
}
function MakeAjaxCall() {
    $.ajax({
        url: "volunteer/home/filter-data",
        type: "GET",
        data: {
            country: country,
            city: JSON.stringify(city),
            theme: JSON.stringify(theme),
            skill: JSON.stringify(skill),
            search: search,
            sort: sort
        },
        success: (result) => {
            $('#partialViewContainer').html(result);

            if (result == "") {
                $(".index-top").addClass("d-none");
                $(".no-mission").removeClass("d-none");
            } else {
                $(".index-top").removeClass("d-none");
                $(".no-mission").addClass("d-none");
            }
        }
    })
}