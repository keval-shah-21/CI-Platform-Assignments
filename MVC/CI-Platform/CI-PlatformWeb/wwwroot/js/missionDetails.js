if ($("#missionMedia").val() > 0) {

const leftScroll = document.querySelector(".mp-left-scroll");
const rightScroll = document.querySelector(".mp-right-scroll");
const smallImages = document.querySelector(".mp-small-images");
rightScroll.addEventListener("click", () => { smallImages.scrollTo(smallImages.scrollLeft + smallImages.offsetWidth, 0); })
leftScroll.addEventListener("click", () => { smallImages.scrollTo(smallImages.scrollLeft - smallImages.offsetWidth, 0); })
}

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

const addFavouriteBtn = document.querySelector("#addFavourite");
const removeFavouriteBtn = document.querySelector("#removeFavourite");
const missionId = document.querySelector("#missionId").value;
$(document).ready(() => {
    const isFavourite = $("#isFavourite").val();
    toggleFavouriteButton(isFavourite)
});
function toggleFavouriteButton(isFavourite) {
    if (isFavourite == "true")
        $(addFavouriteBtn).addClass("d-none");
    else {
        $(removeFavouriteBtn).addClass("d-none");
    }
}
$(addFavouriteBtn).click(() => {
    const userId = document.querySelector("#userId").value;
    console.log("userid ", userId)
    if (userId == null || userId == "") {
        console.log("login")
        return;
    }
    const isFavourite = document.querySelector("#isFavourite").value;
    $.ajax({
        url: "/volunteer/user/toggle-favourite-mission",
        method: "post",
        data: { missionId: missionId, userId : userId, isFavourite: isFavourite === "true" },
        result: (result) => {
            console.log("success");
            if (isFavourite == "true") {;
                $(isFavourite).val("false");
            } else {
                $(isFavourite).val("true");
            }
            toggleFavouriteButton(isFavourite);
        }
    });
});
