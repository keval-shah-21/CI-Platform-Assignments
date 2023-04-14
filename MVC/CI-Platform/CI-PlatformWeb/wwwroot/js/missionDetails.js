const leftScroll = document.querySelector(".mp-left-scroll");
const missionImage = document.querySelector("#missionImage")
const rightScroll = document.querySelector(".mp-right-scroll");
const smallImages = document.querySelector(".mp-small-images");
rightScroll.addEventListener("click", () => { smallImages.scrollTo(smallImages.scrollLeft + smallImages.offsetWidth, 0); })
leftScroll.addEventListener("click", () => { smallImages.scrollTo(smallImages.scrollLeft - smallImages.offsetWidth, 0); })
if (smallImages.scrollWidth > smallImages.clientWidth) {
    leftScroll.classList.remove("d-none");
    rightScroll.classList.remove("d-none");
} else {
    leftScroll.classList.add("d-none");
    rightScroll.classList.add("d-none");
}
addEventListener("resize", () => {
    if (smallImages.scrollWidth > smallImages.clientWidth) {
        leftScroll.classList.remove("d-none");
        rightScroll.classList.remove("d-none");
    } else {
        leftScroll.classList.add("d-none");
        rightScroll.classList.add("d-none");
    }
});
document.querySelectorAll(".mp-small-images > img").forEach(img => {
    img.addEventListener("click", () => {
        missionImage.src = img.src;
    })
});


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
let isFavourite = document.querySelector("#isFavourite").value;
const userId = document.querySelector("#userId").value;
const hasApplied = document.querySelector("#hasApplied").value;

$(document).ready(() => {
    toggleFavouriteButton(isFavourite)
    getRelatedMissions();
    handleMissionRating();
    handleRecommendMission();
});
function getRelatedMissions() {
    $.ajax({
        url: "/Volunteer/Mission/RelatedMissions",
        method: "GET",
        data: { id: missionId },
        success: (result) => {
            $('#partialViewContainer').html(result);
            $('.index-list-view').addClass("d-none");
            if ($('#totalMissions').val() == 0) {
                $('#noRelatedMission').removeClass("d-none");
            }
        },
        error: (error) => {
            console.log(error);
        }
    });
}
function handleMissionRating() {
    const stars = Array.from(document.querySelectorAll("[data-star]"));
    stars.forEach((star, index) => {
        $(star).click(() => {
            if (userId == null || userId == "" || hasApplied == 'false') {
                Swal.fire({
                    icon: 'error',
                    title: 'Oops...',
                    text: 'You need to register in this mission to give rating!',
                })
                return;
            }
            stars.forEach((star, i) => {
                if (i <= index)
                    star.src = "/images/static/selected-star.png";
                else
                    star.src = "/images/static/star.png";
            })
            $.ajax({
                url: "/Volunteer/Mission/RateMission",
                method: "POST",
                data: { missionId: missionId, userId: userId, rate: index + 1 },
                success: (result) => {
                    $("#MissionRatingContainer").html(result);
                    Swal.fire({
                        position: 'top-end',
                        icon: 'success',
                        title: 'Successfully rated the mission!',
                        showConfirmButton: false,
                        timer: 1500
                    })
                },
                error: (error) => {
                    console.log(error);
                }
            });
        })
    })
}
function handleRecommendMission() {
    $("#recommendBtn").click(() => {
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
            data: { missionId, userId },
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
                        data: { missionId, userId, toUsers: userList },
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
}

$(".fav-btn").click(() => {
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
        data: { missionId: missionId, userId: userId, isFavourite: isFavourite === "true" },
        success: (_) => {
            if (isFavourite === "true") {
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Succesfully removed from Favourite missions!',
                    showConfirmButton: false,
                    timer: 1500
                })
            } else {
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
function toggleFavouriteButton(isFavourite) {
    if (isFavourite == "true") {
        $(removeFavouriteBtn).removeClass("d-none");
        $(addFavouriteBtn).addClass("d-none");
    }
    else {
        $(removeFavouriteBtn).addClass("d-none");
        $(addFavouriteBtn).removeClass("d-none");
    }
}

let currentPage = 1;
const totalVolunteers = document.querySelector("#totalVolunteers").value;
if (totalVolunteers > 0) {
    const totalPages = Math.ceil(totalVolunteers / 2);
    const recentVolunteers = document.querySelectorAll("[data-recent]");
    const rightPage = document.querySelector("#rightPage");
    const leftPage = document.querySelector("#leftPage");
    setPage(recentVolunteers);
    $(rightPage).click(() => {
        if (currentPage == totalPages) return;
        currentPage++;
        setPage(recentVolunteers);
    })
    $(leftPage).click(() => {
        if (currentPage == 1) return;
        currentPage--;
        setPage(recentVolunteers)
    })
}
function setPage(recentVolunteers) {
    let count = 0;
    Array.from($(recentVolunteers)).forEach((vol) => {
        const i = $(vol).data('recent');
        if (i >= (currentPage - 1) * 2 && i < currentPage * 2) {
            $(vol).removeClass("d-none");
            count++;
        } else {
            $(vol).addClass("d-none");
        }
    })
    const pageRange = document.querySelector("#pageRange");
    const start = (currentPage - 1) * 2 + 1;
    pageRange.textContent = `${start} - ${start + count - 1} of ${totalVolunteers} recent volunteers`
}

const postComment = document.querySelector("#postComment");
const comment = document.querySelector("#comment");
$(postComment).click(() => {
    if (userId == null || userId == "" || hasApplied == 'false') {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'You need to register in this mission to post comment!',
        })
        return;
    }
    if (comment.value == null || comment.value == "") {
        $('.commentError').text("This field is required.");
        return;
    } else {
        $('.commentError').text("");
    }
    $.ajax({
        url: "/Volunteer/Mission/PostComment",
        method: "POST",
        data: { missionId: missionId, userId: userId, comment: comment.value },
        success: (result) => {
            comment.value = "";
            $("#partialCommentsContainer").html(result);
            Swal.fire({
                position: 'top-end',
                icon: 'success',
                title: 'Successfully posted a comment!',
                text: 'Admin may remove it if necessary.',
                showConfirmButton: false,
                timer: 2500
            })
        },
        error: (error) => {
            console.log(error);
        }
    });
});
