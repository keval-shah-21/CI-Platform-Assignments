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
const isFavourite = document.querySelector("#isFavourite").value;

$(document).ready(() => {
    const isFavourite = $("#isFavourite").val();
    toggleFavouriteButton(isFavourite)

    $.ajax({
        url: "/volunteer/mission/RelatedMissions",
        method: "GET",
        data: {missionId : missionId},
        result: (result) => {
            $('#partialViewContainer').html(result);
            if($('#totalMissions').val() == 0){
                $('#noRelatedMission').removeClass("d-none");
            }
        },
        error: (error) => {
            console.log(error);
        }
    });
});
function toggleFavouriteButton(isFavourite) {
    if (isFavourite == "true"){
        $(removeFavouriteBtn).removeClass("d-none");
        $(addFavouriteBtn).addClass("d-none");
    }
    else {
        $(removeFavouriteBtn).addClass("d-none");
        $(addFavouriteBtn).removeClass("d-none");
    }
}
$(addFavouriteBtn, removeFavouriteBtn).click(() => {
    const userId = document.querySelector("#userId").value;
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
        url: "/volunteer/user/toggle-favourite-mission",
        method: "GET",
        data: { missionId: missionId, userId : userId, isFavourite: isFavourite === "true" },
        result: () => {
            if(isFavourite === "true"){
                isFavourite = "false";
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Succesfully removed from Favourite missions!',
                    showConfirmButton: false,
                    timer: 1500
                  })
            }else{
                isFavourite = "true";
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Succesfully added to Favourite missions!',
                    showConfirmButton: false,
                    timer: 1500
                  })
            }
            isFavourite = isFavourite === "true" ? "false" : "true";
            toggleFavouriteButton(isFavourite);
        },
        error: (error) => {
            console.log(error);
        }
    });
});

let currentPage = 1;
const totalVolunteers = document.querySelector("#totalVolunteers");
if(totalVolunteers > 0){
    const totalPages = Math.ceil(totalVolunteers / 9);
    const recentVolunteers = document.querySelectorAll("[data-recent]");
    const rightPage = document.querySelector("#rightPage");
    const leftPage = document.querySelector("#leftPage");
    setPage(recentVolunteers);
    $(rightPage).click(()=>{
        if(currentPage == totalPages) return;
        currentPage++;
        setPage(recentVolunteers);
    })
    $(leftPage).click(()=>{
        if(currentPage == 1) return;
        currentPage--;
        setPage(recentVolunteers)
    })
}
function setPage(recentVolunteers){
    let count = 0;
    $(recentVolunteers).foreach((vol) => {
        const i = $(vol).data('recent');
        if(i >= (currentPage-1)*9 && i < currentPage*9 ){
            $(vol).removeClass("d-none");
        }else{
            count++;
            $(vol).addClass("d-none");
        }
    })
    const pageRange = document.querySelector("#pageRange");
    const start = (currentPage-1)*9 + 1;
    pageRange.textContent = `${start} - ${start+count-1} recent volunteers`
}

const hasApplied = document.querySelector("#hasApplied");
if(hasApplied == 'true'){
    const postComment = document.querySelector("#postComment");
    const comment = document.querySelector("#comment");
    $(postComment).click(() => {
        if(comment.value == null || comment.value == ""){
            $('.commentError').text("This field is required.");
            return;
        }else{
            $('.commentError').text("");
        }
        $.ajax({
            url: "/Volunteer/Mission/PostComment",
            method: "GET",
            data: {missionId: missionId, userId: userId, comment: comment.value},
            result: (_)=>{
                comment.value = "";
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Successfully posted a comment!',
                    showConfirmButton: false,
                    timer: 1500
                  })
            },
            error: (error) => {
                console.log(error);
            }
        });
    });
}