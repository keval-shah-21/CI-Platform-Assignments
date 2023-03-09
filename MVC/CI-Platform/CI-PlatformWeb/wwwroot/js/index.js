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

// const query = document.querySelector("#Search");
// $(query).on('input', () => {
//     $.ajax({
//         url: "/volunteer/home/search",
//         type: "GET",
//         data: { query: $(query).val() },
//         success: (result) => {
//             $('#partialViewContainer').html(result);
//             if (result == "") {
//                 $(".index-top").addClass("d-none");
//                 $(".no-mission").removeClass("d-none");
//             } else {
//                 $(".index-top").removeClass("d-none");
//                 $(".no-mission").addClass("d-none");
//             }
//         }
//     })
// });

// $("#sortDropdown li").on("click", (e) => {
//     console.log(e.currentTarget.textContent);
// })

// $("#cityDropdown li").on("click", (e) => {
//     const id = e.currentTarget.getAttribute('data-id');
//     addFilterHTML("city", e.currentTarget.textContent, id)
// })
// $("#countryDropdown li").on("click", (e) => {
//     const id = e.currentTarget.getAttribute('data-id');
//     addFilterHTML("country", e.currentTarget.textContent, id)
//     $.ajax({
//         url: "/volunteer/home/filter-country",
//         type: "GET",
//         data: { query: $(query).val(), id:id },
//         success: (result) => {
//             $('#partialViewContainer').html(result);
//             if (result == "") {
//                 $(".index-top").addClass("d-none");
//                 $(".no-mission").removeClass("d-none");
//             } else {
//                 $(".index-top").removeClass("d-none");
//                 $(".no-mission").addClass("d-none");
//             }
//         }
//     })
// })
// $("#themeDropdown li").on("click", (e) => {
//     const id = e.currentTarget.getAttribute('data-id');
//     addFilterHTML("theme", e.currentTarget.textContent, id)
// })
// $("#skillDropdown li").on("click", (e) => {
//     const id = e.currentTarget.getAttribute('data-id');
//     addFilterHTML("skill", e.currentTarget.textContent, id)
// })

// let filters = new Set();
// let isClearAllExist = false;
// const onFilterList = document.querySelector(".on-filter-list");
// function addFilterHTML(type, value, id) {

//     if(filters.has(value)) return;
//     filters.add(value);

//     if (!isClearAllExist) {
//         onFilterList.innerHTML = `
//             <div class="clear-all my-auto color-darkgray fw-light text-14 cursor-pointer">
//                 Clear all
//             </div>
//         `;
//         isClearAllExist = true;
//         $(".clear-all").click(() => {
//             onFilterList.innerHTML = "";
//             filters.clear();
//             isClearAllExist = false;
//         })
//     }
//     $(`<div
//         class="on-filter-item border rounded-pill d-flex align-items-center gap-2 py-1 px-2 color-darkgray fw-light text-14">
//         <span>${value}</span>
//         <img src="/images/static/cancel.png" data-id="${value}" alt="cancel" class="cursor-pointer">
//         </div>
//     `).insertBefore('.clear-all');
//     $(`[data-id = "${value}"]`).click((e) => {
//         filters.delete(e.currentTarget.previousElementSibling.textContent);
//         onFilterList.removeChild(e.currentTarget.parentElement)
//         if(filters.size == 0){
//             $(".clear-all").click();
//         }
//     });
// }

// function updateHeader(){
//     $.ajax({
//         url: "/volunteer/home/update-header",
//         type: "GET",
//         success: (result) => {
//             $('#headerFilterSectionContainer').html(result);
//         }
//     })
// }


let city = [], country = 0, skill = [], theme = [], search = "", sort = "";

const query = document.querySelector("#Search");
$(query).on('input', () => {
    search = $(query).val();
    MakeAjaxCall();
})
$("#themeDropdown li").on("click", (e) => {
    const id = e.currentTarget.getAttribute('data-id');
    if(theme.includes(id)){
        console.log("already there");
        return;
    }
    theme.push(id);
    MakeAjaxCall();
})
$("#skillDropdown li").on("click", (e) => {
    const id = e.currentTarget.getAttribute('data-id');
    if(skill.includes(id)){
        console.log("already there");
        return;
    }
    skill.push(id);
    MakeAjaxCall();
})
$("#countryDropdown li").on("click", (e) => {
    const id = e.currentTarget.getAttribute('data-id');
    country = id;
    MakeAjaxCall();
})
$("#cityDropdown li").on("click", (e) => {
    const id = e.currentTarget.getAttribute('data-id');
    if(city.includes(id)){
        console.log("already there");
        return;
    }
    city.push(id);
    MakeAjaxCall();
})

function MakeAjaxCall(){
    $.ajax({
        url: "volunteer/home/filter-data",
        type: "GET",
        data: {country: country,
            city: JSON.stringify(city),    
            theme: JSON.stringify(theme),
            skill: JSON.stringify(skill),
            search: search,
            sort: sort
            },
        success: (result) => {
            console.log(result);
        }
    })
}