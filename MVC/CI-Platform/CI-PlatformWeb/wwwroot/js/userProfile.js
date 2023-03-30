const allSkillsContainer = document.querySelector("#allSkillsContainer");
const selectedSkillsContainer = document.querySelector("#selectedSkillsContainer");
const rightSkillBtn = document.querySelector("#rightSkill")
const leftSkillBtn = document.querySelector("#leftSkill")
let skills = [];
let selectedLeftSkill = '';
let selectedRightSkill = '';
allSkillsArray = Array.from(allSkillsContainer.children);
addEventsLeft();

function addEventsLeft() {
    allSkillsArray.forEach((s) => {
        s.addEventListener("click", () => {
            s.classList.add("active-skill");
            if (!skills.includes(s.textContent)) {
                selectedLeftSkill = s.textContent;
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
    skills.push(s.textContent)
    selectedSkillsContainer.innerHTML += `<div class="px-2 py-1 cursor-pointer" data-skill='${s.textContent}'>${s.textContent}</div>`;
})
addEventsRight();
resetLeftCSS();

function resetLeftCSS() {
    allSkillsArray.forEach(re => {
        if (!skills.includes(re.textContent) && re.textContent != selectedLeftSkill) {
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
        skills.push(selectedLeftSkill);
        selectedSkillsContainer.innerHTML += `<div class="px-2 py-1 cursor-pointer" data-skill='${selectedLeftSkill}'>${selectedLeftSkill}</div>`;
        addEventsRight();
        selectedLeftSkill = "";
        selectedRightSkill = "";
        resetLeftCSS();
        resetRightCSS();
    }
})
leftSkillBtn.addEventListener("click", () => {
    if (selectedRightSkill != "" && selectedRightSkill != null) {
        skills.splice(skills.indexOf(selectedRightSkill), 1);
        selectedSkillsContainer.removeChild(document.querySelector(`[data-skill='${selectedRightSkill}']`));
        selectedRightSkill = "";
        selectedLeftSkill = "";
        resetLeftCSS();
        resetRightCSS();
    }
})
const noSkill = document.querySelector("#noSkill");
document.querySelector("#saveSkillBtn").addEventListener("click", () => {
    if (skills.length == 0) {
        noSkill.classList.remove("d-none");
        document.querySelector("#outerSkillsContainer").innerHTML = "";
    } else {
        noSkill.classList.add("d-none");
        const outerSkillsContainer = document.querySelector("#outerSkillsContainer");
        outerSkillsContainer.innerHTML = "";
        skills.forEach(s => {
            outerSkillsContainer.innerHTML += `<div data-pre="${s}">${s}</div>`
        })
    }
    $("#skillModal").modal("hide")
})

$('#savePasswordBtn').click(() => {
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
    } else if (oldPassword != $('#Password').val()) {
        $("#oldPasswordError").text("Incorrect old Password.");
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
    $("#changePasswordModal").modal("hide");
});

$("#saveContactBtn").click(() => {
    const message = document.querySelector('#message').value.trim();
    const subject = document.querySelector('#subject').value.trim();
    let error = false;

    if (message == null || message == "") {
        error = true;
        $("#messageError").text("The Message field is required.");
    } else {
        $("#messageError").text("");
    }
    if (subject == null || subject == "") {
        error = true;
        $("#subjectError").text("The Subject field is required.");
    } else {
        $("#subjectError").text("");
    }

    if (error) return;
    $('#contactModal').modal("hide");
});

$("#profilePic").click(() => { $('#profileInput').click(); });
$("#profileInput").on("change", () => {
    $("#profilePic").attr("src", URL.createObjectURL($("#profileInput").prop('files')[0]))
})