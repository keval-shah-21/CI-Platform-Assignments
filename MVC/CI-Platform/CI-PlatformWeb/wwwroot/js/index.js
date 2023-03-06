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

const query = document.querySelector("#Search");
$(query).on('input', () => {
    $.ajax({
        url: "/volunteer/home/search",
        type: "GET",
        data: { query: $(query).val() },
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
});

$("#sortDropdown li").on("click", (e)=>{
    console.log(e.currentTarget.textContent);
})

$("#cityDropdown li").on("click", (e)=>{
    addFilter("city", e.currentTarget.textContent)
})
$("#countryDropdown li").on("click", (e)=>{
    addFilter("country", e.currentTarget.textContent)
})
$("#themeDropdown li").on("click", (e)=>{
    addFilter("theme", e.currentTarget.textContent)
})
$("#skillDropdown li").on("click", (e)=>{
    addFilter("skill", e.currentTarget.textContent)
})

let filters = new Set();
const onFilterList =  document.querySelector(".on-filter-list");
function addFilter(type, value){
    filters.add(value);
    createFilterHTML();
}
function createFilterHTML(){
    onFilterList.innerHTML = "";
    Array.from(filters).forEach((f, index) => {
        onFilterList.innerHTML += 
        `<div
            class="on-filter-item border rounded-pill d-flex align-items-center gap-2 py-1 px-2 color-darkgray fw-light text-14">
            <span>${f}</span>
            <img src="/images/static/cancel.png" alt="cancel" class="cancel-btn cursor-pointer">
        </div>
        `;
    });
    onFilterList.innerHTML += `
    <div class="clear-all my-auto color-darkgray fw-light text-14 cursor-pointer">
        Clear all
    </div>
    `;
    $(".clear-all").on('click', ()=>{
        onFilterList.innerHTML = "";
        filters.clear();
        isClearAllExist = false;
    })
}