const menubar = document.querySelector("#menu-bar");
const sidebar = document.querySelector(".sidebar");

menubar.addEventListener("click", () => {
    sidebar.classList.toggle("show-sidebar");
})

const tabContent = document.querySelector(".tab-content");
const tab1 = document.querySelector(".tab1");
const tab2 = document.querySelector(".tab2");
const tab3 = document.querySelector(".tab3");
tabContent.textContent = "tab 1 Ratione impedit unde quae quos iure, sunt voluptas voluptatum quam officia dicta pariatur fugit amet accusantium esse beatae officiis. Voluptate unde officiis, vel cumque debitis nostrum eveniet tempora architecto. Culpa necessitatibus laudantium sit nulla? Reiciendis distinctio possimus quidem.";

tab2.addEventListener("click", ()=>{
    tab2.classList.add("active-tab");
    tab1.classList.remove("active-tab");
    tab3.classList.remove("active-tab");

    tabContent.textContent = "tab 2 Lorem ipsum dolor sit amet consectetur adipisicing elit. Numquam odit quaerat laboriosam. Ratione impedit unde quae quos iure, sunt voluptas voluptatum quam officia dicta pariatur fugit amet accusantium esse beatae officiis. Voluptate unde officiis, vel cumque debitis nostrum eveniet tempora architecto. Culpa necessitatibus laudantium sit nulla? Reiciendis distinctio possimus quidem.";
})
tab1.addEventListener("click", ()=>{
    tab1.classList.add("active-tab");
    tab2.classList.remove("active-tab");
    tab3.classList.remove("active-tab");

    tabContent.textContent = "tab 1 Ratione impedit unde quae quos iure, sunt voluptas voluptatum quam officia dicta pariatur fugit amet accusantium esse beatae officiis. Voluptate unde officiis, vel cumque debitis nostrum eveniet tempora architecto. Culpa necessitatibus laudantium sit nulla? Reiciendis distinctio possimus quidem.";
})
tab3.addEventListener("click", ()=>{
    tab3.classList.add("active-tab");
    tab1.classList.remove("active-tab");
    tab2.classList.remove("active-tab");

    tabContent.textContent = "tab 3 adipisicing elit. Numquam odit quaerat laboriosam. Ratione impedit unde quae quos iure, sunt voluptas voluptatum quam officia dicta pariatur fugit amet accusantium esse beatae officiis. Voluptate unde officiis, vel cumque debitis nostrum eveniet tempora architecto. Culpa necessitatibus laudantium sit nulla? Reiciendis distinctio possimus quidem.";
})