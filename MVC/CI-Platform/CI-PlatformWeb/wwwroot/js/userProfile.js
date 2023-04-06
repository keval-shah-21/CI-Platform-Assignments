$('#myprofileBtn').addClass("active-navitem");

const allSkillsContainer = document.querySelector("#allSkillsContainer");
const selectedSkillsContainer = document.querySelector("#selectedSkillsContainer");
const rightSkillBtn = document.querySelector("#rightSkill")
const leftSkillBtn = document.querySelector("#leftSkill")
let skills = new Map();
let selectedLeftSkill = '';
let selectedRightSkill = '';
let leftSkillId = 0;
let rightSkillId = 0;
allSkillsArray = Array.from(allSkillsContainer.children);
addEventsLeft();

function addEventsLeft() {
    allSkillsArray.forEach((s) => {
        s.addEventListener("click", () => {
            s.classList.add("active-skill");
            if (!skills.has($(s).data("id"))) {
                selectedLeftSkill = s.textContent;
                leftSkillId = $(s).data("id")
                resetLeftCSS();
            }
        })
    });
}
function addEventsRight() {
    const selectedSkillsArray = Array.from(selectedSkillsContainer.children);
    selectedSkillsArray.forEach(s => {
        s.addEventListener("click", () => {
            selectedRightSkill = s.textContent;
            rightSkillId = $(s).data("skill");
            selectedSkillsArray.forEach(re => {
                if (selectedRightSkill != re.textContent) {
                    re.classList.remove("active-skill")
                } else {
                    re.classList.add("active-skill")
                }
            })
        })
    })
}
const pre = document.querySelectorAll(`[data-pre]`);
pre.forEach(s => {
    skills.set($(s).data('pre'), s.textContent)
    selectedSkillsContainer.innerHTML += `<div class="px-2 py-1 cursor-pointer" data-skill='${$(s).data('pre')}'>${s.textContent}</div>`;
})
const noSkill = document.querySelector("#noSkill");
if (skills.size != 0) noSkill.classList.add("d-none");
addEventsRight();
resetLeftCSS();

function resetLeftCSS() {
    allSkillsArray.forEach(re => {
        if (!skills.has($(re).data("id")) && re.textContent != selectedLeftSkill) {
            re.classList.remove("active-skill")
        } else {
            re.classList.add("active-skill");
        }
    })
}
function resetRightCSS() {
    Array.from(selectedSkillsContainer.children).forEach(re => {
        if (selectedRightSkill != re.textContent) {
            re.classList.remove("active-skill")
        } else {
            re.classList.add("active-skill")
        }
    })
}
rightSkillBtn.addEventListener("click", () => {
    if (selectedLeftSkill != "" && selectedLeftSkill != null) {
        skills.set(leftSkillId, selectedLeftSkill);
        selectedSkillsContainer.innerHTML += `<div class="px-2 py-1 cursor-pointer" data-skill='${leftSkillId}'>${selectedLeftSkill}</div>`;
        addEventsRight();

        selectedLeftSkill = "";
        selectedRightSkill = "";
        resetLeftCSS();
        resetRightCSS();
    }
})
leftSkillBtn.addEventListener("click", () => {
    if (selectedRightSkill != "" && selectedRightSkill != null) {
        skills.delete(rightSkillId);
        selectedSkillsContainer.removeChild(document.querySelector(`[data-skill='${rightSkillId}']`));
        selectedRightSkill = "";
        selectedLeftSkill = "";
        resetLeftCSS();
        resetRightCSS();
    }
})
document.querySelector("#saveSkillBtn").addEventListener("click", () => {
    if (skills.size == 0) {
        noSkill.classList.remove("d-none");
        document.querySelector("#outerSkillsContainer").innerHTML = "";
    } else {
        noSkill.classList.add("d-none");
        const outerSkillsContainer = document.querySelector("#outerSkillsContainer");
        outerSkillsContainer.innerHTML = "";
        skills.forEach((value, key) => {
            outerSkillsContainer.innerHTML += `<div data-pre="${key}">${value}</div><input hidden name="skillIds" value=${key}>`
        })
    }
    $("#skillModal").modal("hide")
})
$("#changePasswordBtn").click(() => {
    $.ajax({
        url: "/Volunteer/User/get-change-password-partial",
        method: "GET",
        success: (result) => {
            $("#changePasswordPartialContainer").html(result);
            $("#changePasswordModal").modal("show");
            $("#savePasswordBtn").click(() => handleChangePassword());
        },
        error: (error) => console.log(error)
    });
})

function handleChangePassword() {
    const oldPassword = document.querySelector("#oldPassword").value.trim();
    const newPassword = document.querySelector("#newPassword").value.trim();
    const confirmPassword = document.querySelector("#confirmPassword").value
    let error = false;
    if (oldPassword == null || oldPassword == "") {
        $("#oldPasswordError").text("The Old Password field is required.");
        error = true;
    } else if (oldPassword.length < 8) {
        $("#oldPasswordError").text("Minimum 8 characters required.");
        error = true;
    }
    else {
        $("#oldPasswordError").text("");
    }

    if (newPassword == null || newPassword == "") {
        $("#newPasswordError").text("The New Password field is required.");
        error = true;
    } else if (newPassword.length < 8) {
        $("#newPasswordError").text("Minimum 8 characters required.");
        error = true;
    } else {
        $("#newPasswordError").text("");
    }

    if (confirmPassword == null || confirmPassword == "") {
        $("#confirmPasswordError").text("The Confirm Password field is required.");
        error = true;
    } else if (newPassword != confirmPassword) {
        $("#confirmPasswordError").text("Both New Password and Confirm Password must be same");
        error = true;
    } else {
        $("#confirmPasswordError").text("");
    }

    if (error) return;
    $.ajax({
        url: "/volunteer/user/update-password",
        method: "put",
        data: { email: $('#email').val(), oldPassword: oldPassword, newPassword: newPassword },
        success: (_, __, status) => {
            if (status.status == 204) {
                $("#oldPasswordError").text("The Old Password is incorrect!");
            } else {
                $("#oldPasswordError").text("");
                $("#changePasswordModal").modal("hide");
                Swal.fire({
                    position: 'top-end',
                    icon: 'success',
                    title: 'Successfully updated the Password!',
                    showConfirmButton: false,
                    timer: 1500
                })
            }
        },
        error: error => {
            console.log(error)
        }
    });
}

$("#profilePic").click(() => { $('#profileInput').click(); });
$("#profileInput").on("change", () => {
    $("#profilePic").attr("src", URL.createObjectURL($("#profileInput").prop('files')[0]))
})
if ($('countryDropdown').val() != 0) {
    const countryId = $("#countryDropdown").val();
    document.querySelectorAll('#cityDropdown option').forEach(city => {
        if ($(city).data('id') == countryId) {
            $(city).removeClass("d-none");
        } else {
            $(city).addClass("d-none");
        }
    })
}
$('#countryDropdown option').on("click", e => {
    if ($("#cityDropdown").val() != e.currentTarget.value) {
        $('#cityDropdown').val(0);
    }
    document.querySelectorAll('#cityDropdown option').forEach(city => {
        if ($(city).data('id') == e.currentTarget.value) {
            $(city).removeClass("d-none");
        } else {
            $(city).addClass("d-none");
        }
    })
})