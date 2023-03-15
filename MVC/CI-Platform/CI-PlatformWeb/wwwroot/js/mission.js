const leftScroll = document.querySelector(".mp-left-scroll");
const rightScroll = document.querySelector(".mp-right-scroll");
const smallImages = document.querySelector(".mp-small-images");
rightScroll.addEventListener("click", () => { smallImages.scrollTo(smallImages.scrollLeft + smallImages.offsetWidth, 0); })
leftScroll.addEventListener("click", () => { smallImages.scrollTo(smallImages.scrollLeft - smallImages.offsetWidth, 0); })

const Tab1 = document.querySelector(".mp-tab1");
const Tab2 = document.querySelector(".mp-tab2");
const Tab3 = document.querySelector(".mp-tab3");
const mpMission = document.querySelector(".mp-mission");
const mpOrganization = document.querySelector(".mp-organization");
const mpComments = document.querySelector(".mp-comments");

Tab2.addEventListener('click', () => {
    Tab1.classList.remove("mp-tab-active");
    Tab3.classList.remove("mp-tab-active");
    Tab2.classList.add("mp-tab-active");

    mpMission.classList.add("d-none");
    mpOrganization.classList.remove("d-none");
    mpComments.classList.add("d-none");
})
Tab3.addEventListener('click', () => {
    Tab1.classList.remove("mp-tab-active");
    Tab2.classList.remove("mp-tab-active");
    Tab3.classList.add("mp-tab-active");

    mpMission.classList.add("d-none");
    mpOrganization.classList.add("d-none");
    mpComments.classList.remove("d-none");
})
Tab1.addEventListener('click', () => {
    Tab2.classList.remove("mp-tab-active");
    Tab3.classList.remove("mp-tab-active");
    Tab1.classList.add("mp-tab-active");

    mpMission.classList.remove("d-none");
    mpOrganization.classList.add("d-none");
    mpComments.classList.add("d-none");
})