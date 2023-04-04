$('#timesheetBtn').addClass("active-navitem");

const userId = $("#userId").val();
addEditEventListner();
addDeleteEventListner();
addAddEventListner();

//add event listners
function addAddEventListner() {
    $("#openHourModal").click(() => {
        $.ajax({
            url: "/Volunteer/Mission/AddTimesheetHour",
            method: "get",
            success: (result, _, status) => {
                if (status.status == 204) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Please participate in any time mission to add timesheet!',
                        footer: '<a href="/">Checkout all missions</a>'
                    })
                    return;
                }
                $('#partialTimesheetModalContainer').html(result);
                $("#hourModal").modal("show");
                $('#addHourForm').on("submit", (e) => {
                    e.preventDefault();
                    $("#addHourForm").valid();
                    if ($("#addHourForm").valid()) {
                        Swal.fire({
                            title: 'Are you sure?',
                            text: "Admin will review and approve it!",
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Yes, Submit!'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                $.ajax({
                                    url: "/Volunteer/Mission/AddTimesheetHour",
                                    method: "post",
                                    data: $("#addHourForm").serialize(),
                                    success: (result, _, status) => {
                                        if (status.status == 204) {
                                            Swal.fire({
                                                position: 'top-end',
                                                icon: 'error',
                                                title: 'Something went wrong!',
                                                showConfirmButton: false,
                                                timer: 1500
                                            })
                                            return;
                                        }
                                        $("#hourModal").modal("hide");
                                        $("#partialTimesheetContainer").html(result);
                                        addEditEventListner();
                                        addDeleteEventListner();
                                        addAddEventListner();
                                        Swal.fire({
                                            position: 'top-end',
                                            icon: 'success',
                                            title: 'Successfully added the timesheet data!',
                                            showConfirmButton: false,
                                            timer: 1500
                                        })
                                    },
                                    error: error => {
                                        console.log(error);
                                    }
                                });
                            }
                        })
                    }
                });
            },
            error: error => {
                console.log(error)
            }
        });
    });
    $("#openGoalModal").click(() => {
        $.ajax({
            url: "/Volunteer/Mission/AddTimesheetGoal",
            method: "get",
            success: (result, _, status) => {
                if (status.status == 204) {
                    Swal.fire({
                        icon: 'error',
                        title: 'Oops...',
                        text: 'Please participate in any goal mission to add timesheet!',
                        footer: '<a href="/">Checkout all missions</a>'
                    })
                    return;
                }
                $('#partialTimesheetModalContainer').html(result);
                $("#goalModal").modal("show");
                $('#addGoalForm').on("submit", (e) => {
                    e.preventDefault();
                    $("#addGoalForm").valid();
                    if ($("#addGoalForm").valid()) {
                        Swal.fire({
                            title: 'Are you sure?',
                            text: "Admin will review and approve it!",
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonColor: '#3085d6',
                            cancelButtonColor: '#d33',
                            confirmButtonText: 'Yes, Submit!'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                $.ajax({
                                    url: "/Volunteer/Mission/AddTimesheetGoal",
                                    method: "post",
                                    data: $("#addGoalForm").serialize(),
                                    success: (result, _, status) => {
                                        if (status.status == 204) {
                                            Swal.fire({
                                                position: 'top-end',
                                                icon: 'error',
                                                title: 'Something went wrong!',
                                                showConfirmButton: false,
                                                timer: 1500
                                            })
                                            return;
                                        }
                                        $("#goalModal").modal("hide");
                                        $("#partialTimesheetContainer").html(result);
                                        addEditEventListner();
                                        addDeleteEventListner();
                                        addAddEventListner();
                                        Swal.fire({
                                            position: 'top-end',
                                            icon: 'success',
                                            title: 'Successfully added the timesheet data!',
                                            showConfirmButton: false,
                                            timer: 1500
                                        })
                                    },
                                    error: error => {
                                        console.log(error);
                                    }
                                });
                            }
                        })
                    }
                });
            },
            error: error => {
                console.log(error)
            }
        });
    });
}

//edit event listners
function addEditEventListner() {
    $(".editHour").each(function () {
        $(this).click(() => {
            $.ajax({
                url: "/Volunteer/Mission/EditTimesheetHour",
                method: "get",
                data: { timesheetId: $(this).data("id"), userId },
                success: (result, _, status) => {
                    if (status == 204) {
                        Swal.fire({
                            position: 'top-end',
                            icon: 'error',
                            title: 'Something went wrong!',
                            showConfirmButton: false,
                            timer: 1500
                        });
                        return;
                    }
                    $('#partialTimesheetModalContainer').html(result);
                    $("#editHourModal").modal("show");
                    $('#editHourForm').on("submit", (e) => {
                        e.preventDefault();
                        $("#editHourForm").valid();
                        if ($("#editHourForm").valid()) {
                            Swal.fire({
                                title: 'Are you sure?',
                                text: "Admin will review and approve it!",
                                icon: 'warning',
                                showCancelButton: true,
                                confirmButtonColor: '#3085d6',
                                cancelButtonColor: '#d33',
                                confirmButtonText: 'Yes, Submit!'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    $.ajax({
                                        url: "/Volunteer/Mission/EditTimesheetHour",
                                        method: "put",
                                        data: $("#editHourForm").serialize(),
                                        success: (result, _, status) => {
                                            $("#partialTimesheetContainer").html(result);
                                            addEditEventListner();
                                            addDeleteEventListner();
                                            addAddEventListner();
                                            if (status.status == 200) {
                                                $("#editHourModal").modal("hide");
                                                Swal.fire({
                                                    position: 'top-end',
                                                    icon: 'success',
                                                    title: 'Successfully updated the timesheet data!',
                                                    showConfirmButton: false,
                                                    timer: 1500
                                                })
                                            } else {
                                                Swal.fire({
                                                    position: 'top-end',
                                                    icon: 'error',
                                                    title: 'Something went wrong!',
                                                    showConfirmButton: false,
                                                    timer: 1500
                                                })
                                            }
                                        },
                                        error: error => {
                                            console.log(error);
                                        }
                                    });
                                }
                            })
                        }
                    });
                },
                error: error => console.log(error)
            });
        });
    })
    $(".editGoal").each(function () {
        $(this).click(() => {
            $.ajax({
                url: "/Volunteer/Mission/EditTimesheetGoal",
                method: "get",
                data: { timesheetId: $(this).data("id"), userId },
                success: (result, _, status) => {
                    if (status == 204) {
                        Swal.fire({
                            position: 'top-end',
                            icon: 'error',
                            title: 'Something went wrong!',
                            showConfirmButton: false,
                            timer: 1500
                        });
                        return;
                    }
                    $('#partialTimesheetModalContainer').html(result);
                    $("#editGoalModal").modal("show");
                    $('#editGoalForm').on("submit", (e) => {
                        e.preventDefault();
                        $("#editGoalForm").valid();
                        if ($("#editGoalForm").valid()) {
                            Swal.fire({
                                title: 'Are you sure?',
                                text: "Admin will review and approve it!",
                                icon: 'warning',
                                showCancelButton: true,
                                confirmButtonColor: '#3085d6',
                                cancelButtonColor: '#d33',
                                confirmButtonText: 'Yes, Submit!'
                            }).then((result) => {
                                if (result.isConfirmed) {
                                    $.ajax({
                                        url: "/Volunteer/Mission/EditTimesheetGoal",
                                        method: "put",
                                        data: $("#editGoalForm").serialize(),
                                        success: (result, _, status) => {
                                            $("#partialTimesheetContainer").html(result);
                                            addEditEventListner();
                                            addDeleteEventListner();
                                            addAddEventListner();
                                            if (status.status == 200) {
                                                $("#editGoalModal").modal("hide");
                                                Swal.fire({
                                                    position: 'top-end',
                                                    icon: 'success',
                                                    title: 'Successfully updated the timesheet data!',
                                                    showConfirmButton: false,
                                                    timer: 1500
                                                })
                                            } else {
                                                Swal.fire({
                                                    position: 'top-end',
                                                    icon: 'error',
                                                    title: 'Something went wrong!',
                                                    showConfirmButton: false,
                                                    timer: 1500
                                                })
                                            }
                                        },
                                        error: error => {
                                            console.log(error);
                                        }
                                    });
                                }
                            })
                        }
                    });
                },
                error: error => console.log(error)
            });
        });
    })
}

function addDeleteEventListner() {
    $(".deleteHour").each(function () {
        $(this).click(() => {
            deleteTimesheet($(this).data("id"));
        });
    })
    $(".deleteGoal").each(function () {
        $(this).click(() => {
            deleteTimesheet($(this).data("id"));
        });
    })
}
function deleteTimesheet(timesheetId) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to retrieve data back!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, Remove!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: "/Volunteer/Mission/DeleteTimesheet",
                method: "delete",
                data: { timesheetId, userId },
                success: (result, _, status) => {
                    $("#partialTimesheetContainer").html(result);
                    addEditEventListner();
                    addDeleteEventListner();
                    addAddEventListner();
                    if (status.status == 200) {
                        Swal.fire({
                            position: 'top-end',
                            icon: 'success',
                            title: 'Successfully deleted the timesheet data!',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    } else {
                        Swal.fire({
                            position: 'top-end',
                            icon: 'error',
                            title: 'Something went wrong!',
                            showConfirmButton: false,
                            timer: 1500
                        })
                    }
                },
                error: error => {
                    console.log(error);
                }
            });
        }
    })
}