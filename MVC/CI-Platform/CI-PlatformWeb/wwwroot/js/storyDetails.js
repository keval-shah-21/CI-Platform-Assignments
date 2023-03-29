if ($('#storyMedia').val() > 0) {
    const leftScroll = document.querySelector(".mp-left-scroll");
    const rightScroll = document.querySelector(".mp-right-scroll");
    const smallImages = document.querySelector(".mp-small-images");
    rightScroll.addEventListener("click", () => {
        smallImages.scrollTo(
            smallImages.scrollLeft + smallImages.offsetWidth,
            0
        );
    });
    leftScroll.addEventListener("click", () => {
        smallImages.scrollTo(
            smallImages.scrollLeft - smallImages.offsetWidth,
            0
        );
    });
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
}
$(document).ready(() => {
    handleRecommendMission();
})
const userId = document.querySelector("#userId").value;
const storyId = document.querySelector("#storyId").value;

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
            data: { storyId, userId },
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
                        url: "/Volunteer/Story/RecommendStory",
                        data: { storyId, userId, toUsers: userList },
                        method: "POST",
                        success: (result) => {
                            $("#partialRecommendContainer").html(result);
                            Swal.fire({
                                position: 'top-end',
                                icon: 'success',
                                title: 'Successfully recommended the story!',
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