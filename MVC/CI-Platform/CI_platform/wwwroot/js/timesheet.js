
$(document).ready(function () {

    $("input[type='date']").keydown(function (event) { event.preventDefault(); });

    loadDataTabel();
});
function loadDataTabel() {
    $('#volunteer-time-table').DataTable().destroy()
    var timetstable = $('#volunteer-time-table').DataTable({

        lengthChange: false,
        paging: false,
        searching: false,
        columnDefs: [
            { "orderable": false, "targets": 0 },
            { "orderData": [1] },
            { "orderData": [2] },
            { "orderable": false, "targets": 3 },
            { "orderable": false, "targets": 4 }
        ],
        order: [[1, "asc"], [2, "asc"]] // sort by date (column 1) ascending, then by hours (column 2) ascending

    });
    $('#volunteer-goal-table').DataTable().destroy()
    var goaltstable = $('#volunteer-goal-table').DataTable({

        lengthChange: false,
        paging: false,
        searching: false,
        columnDefs: [
            { "orderable": false, "targets": 0 },
            { "orderData": [1] },
            { "orderData": [2] },
            { "orderable": false, "targets": 3 },
        ],
        order: [[1, "asc"], [2, "asc"]] // sort by date (column 1) ascending, then by hours (column 2) ascending

    });

}
function getDatesTs() {

        var startDate = $('#missionTimelist').find('option:selected').data('date');
    console.log(startDate);
    startDate = startDate.split(' ')[0]
    var dateParts = startDate.split("-");
    var year = dateParts[2];
    var month = dateParts[1];
    var day = dateParts[0];
    var minDateVal = `${year}-${month}-${day}`


    $('#TimeVolunteerDate').attr('min', minDateVal);
    const today = new Date();
    const maxDateVal = today.toISOString().split('T')[0];
    $('#TimeVolunteerDate').attr('max', maxDateVal);
}

function getDatesGoalTs() {

    var startDate = $('#missionGoallist').find('option:selected').data('date');
    console.log(startDate);
    startDate = startDate.split(' ')[0]
    var dateParts = startDate.split("-");
    var year = dateParts[2];
    var month = dateParts[1];
    var day = dateParts[0];
    var minDateVal = `${year}-${month}-${day}`
    $('#GoalVolunteerDate').attr('min', minDateVal);
    const today = new Date();
    const maxDateVal = today.toISOString().split('T')[0];
    $('#GoalVolunteerDate').attr('max', maxDateVal);

}
function TimeBaseTs() {
    clearTextBox();

    /*$('#volunteerHours').modal('show')*/

    $('#TsId').hide();
    $('.submit-time-timesheet').css('display', 'block');
    $('.update-time-timesheet').css('display', 'none');
}

function GoalBaseTs() {
    clearTextBox();

    $('#volunteerGoals').modal('show')
    $('#GsId').hide();
    $('.submit-time-timesheet').css('display', 'block');
    $('.update-time-timesheet').css('display', 'none');
}
function AddTimeTS() {

    var hour = ($('#TimeVolunteerHour').val());
    var min = ($('#TimeVolunteerMinute').val());
  
    var timeString = hour + ':' + min ;
    console.log(timeString);
    var timeParts = timeString.split(':');
    var timeOnly = new Date(0, 0, 0, timeParts[0], timeParts[1]);

    var Timesheet = {
        MissionId: $('#missionTimelist').val(),
        DateVolunteered: $('#TimeVolunteerDate').val(),
        Time: timeOnly.toTimeString().slice(0, 8),
        Action: $('#VolunteerAction').val(),
        Notes: $('#TimeVolunteerMessage').val(),
        UserId: $('.submit-time-timesheet').data('user-id'),
    }
    temp(Timesheet);
    //if (validateFormTimeTs()) {
        
    //    loadDataTabel();
    //    $('#volunteerHours').modal('toggle');
    //}

}

function AddGoalTS() {
    var Timesheet = {
        MissionId: $('#missionGoallist').val(),
        DateVolunteered: $('#GoalVolunteerDate').val(),

        Action: $('#VolunteerAction').val(),
        Notes: $('#GoalVolunteerMessage').val(),
        UserId: $('.submit-time-timesheet').data('user-id'),
    }
    if (validateFormGoalTs()) {
        temp(Timesheet);
        loadDataTabel();
        $('#volunteerGoals').modal('toggle');

    }

}


function temp(Timesheet) {
    $.ajax({
        url: '/TimeSheet/AddTimeTs',
        type: 'Post',
        data: Timesheet,
        contentType: 'application/x-www-form-urlencoded;charset=utf-8;',
        success: function (response) {

            $('#partial-timesheet').html(response);
            swal.fire({
                position: 'center',
                icon: "success",
                title: "Record Added To TimeSheet",
                showConfirmButton: false,
                timer: 3000
            });
            loadDataTabel();

        },
        error: function () {

        }
    })


}

function clearTextBox() {
    $('#missionTimelist').val(' ');
    $('#TimeVolunteerDate').val(' ');
    $('#TimeVolunteerHour').val(' ');
    $('#TimeVolunteerMinute').val(' ');
    $('#TimeSheetId').val(' ');
    $('#TimeVolunteerMessage').val(' ');
}

function Delete(id) {
    Swal.fire({
        title: 'Are You Sure?',
        text: "You Won't to Delete this?",
        position: 'center',
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: '#d33',
        confirmButtonText: "Surely Delete?"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/TimeSheet/Delete?id=' + id,

                success: function (response) {

                    $('#partial-timesheet').html(response);
                    swal.fire({
                        position: 'center',
                        icon: "success",
                        title: "Record Deleted Succesfully",
                        showConfirmButton: false,
                        timer: 2000
                    });
                    loadDataTabel();

                },
                error: function () {
                    alert("data can not deleted");
                }
            })
        }
    })

}

function EditGoal(id) {

    $.ajax({
        url: '/TimeSheet/Edit?id=' + id,
        type: 'Get',
        contentType: 'application/json;charset=utf-8;',
        dataType: 'json',

        success: function (response) {
            /* $('#volunteerGoals').modal('show');*/
            $('#VolunteerAction').val(response.action);
            $('#missionGoallist').val(response.missionId);
            getDatesGoalTs();
            $('#GoalVolunteerMessage').val(response.notes);
            $('#GoalSheetId').val(response.timesheetId);
            const date = new Date(response.dateVolunteered);
            const yyyy = date.getFullYear();
            const mm = String(date.getMonth() + 1).padStart(2, '0');
            const dd = String(date.getDate()).padStart(2, '0');
            const formattedDate = `${yyyy}-${mm}-${dd}`;

            $('#GoalVolunteerDate').val(formattedDate);
            $('.submit-time-timesheet').css('display', 'none');
            $('.update-time-timesheet').css('display', 'block');


        },
        error: function () {
            alert("Data Not Found");
        }
    })


}

function EditTime(id) {

    $.ajax({
        url: '/TimeSheet/Edit?id=' + id,
        type: 'Get',
        contentType: 'application/json;charset=utf-8;',
        dataType: 'json',

        success: function (response) {
            /*            $('#volunteerHours').modal('show');*/
            $('#TimeSheetId').val(response.timesheetId);
            $('#missionTimelist').val(response.missionId);
            getDatesTs();
            $('#TimeVolunteerMessage').val(response.notes);
            $('#TimeVolunteerHour').val(response.time.substring(0, 2));
            $('#TimeVolunteerMinute').val(response.time.substring(3, 5));
            const date = new Date(response.dateVolunteered);
            const yyyy = date.getFullYear();
            const mm = String(date.getMonth() + 1).padStart(2, '0');
            const dd = String(date.getDate()).padStart(2, '0');
            const formattedDate = `${yyyy}-${mm}-${dd}`;

            $('#TimeVolunteerDate').val(formattedDate);
            $('.submit-time-timesheet').css('display', 'none');
            $('.update-time-timesheet').css('display', 'block');


        },
        error: function () {
            alert("Data Not Found");
        }
    })


}

function UpdateTimeTs() {
    var hour = $('#TimeVolunteerHour').val();
    var min = $('#TimeVolunteerMinute').val();
    var time = hour + ':' + min;
    var Timesheet = {
        TimeSheetId: $('#TimeSheetId').val(),
        MissionId: $('#missionTimelist').val(),
        DateVolunteered: $('#TimeVolunteerDate').val(),
        Time: time,
        CreatedAt: $('#CreatedDate').val(),
        Notes: $('#TimeVolunteerMessage').val(),
        UserId: $('.submit-time-timesheet').data('user-id'),
    }
    if (validateFormTimeTs()) {
        Update(Timesheet);

        $('#volunteerHours').modal('toggle');

    }

}
function UpdateGoalTs() {

    var Timesheet = {
        TimeSheetId: $('#GoalSheetId').val(),
        MissionId: $('#missionGoallist').val(),
        DateVolunteered: $('#GoalVolunteerDate').val(),
        Action: $('#VolunteerAction').val(),
        Notes: $('#GoalVolunteerMessage').val(),
        UserId: $('.submit-time-timesheet').data('user-id'),
    }
    if (validateFormGoalTs()) {
        Update(Timesheet);

        $('#volunteerGoals').modal('toggle');

    }

}
function Update(Timesheet) {
    Swal.fire({
        title: 'Are You Sure?',
        text: "You Won't to Update this?",
        position: 'center',
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: '#d33',
        confirmButtonText: "Yes,Update it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/TimeSheet/Update',
                type: 'Post',
                data: Timesheet,
                contentType: 'application/x-www-form-urlencoded;charset=utf-8;',

                success: function (response) {

                    $('#partial-timesheet').html(response);
                    swal.fire({
                        position: 'center',
                        icon: "success",
                        title: "Record Updated Succesfully",
                        showConfirmButton: false,
                        timer: 2000
                    });
                    loadDataTabel();
                    console.log(Timesheet);
                },
                error: function () {
                    alert("hello world");
                }

            })
        }
    })
}

$('#missionTimelist').on("input", missionSelectionValidation)
function missionSelectionValidation() {
    var TimeSheetMission = document.getElementById('missionTimelist').value;
    if (TimeSheetMission === "") {
        document.getElementById("TimeSheetMissionError").innerHTML = "Please select Mission";
        error = true;
    } else {
        document.getElementById("TimeSheetMissionError").innerHTML = "";
    }
}

$('#TimeVolunteerMessage').on("input", TimeMessageValidation)
function TimeMessageValidation() {
    var TimeVolunteerMessage = document.getElementById('TimeVolunteerMessage').value;
    if (TimeVolunteerMessage.trim() === "") {
        document.getElementById("TimeVolunteerMessageError").innerHTML = "Please enter a Your Work Description";
        error = true;
    } else {
        document.getElementById("TimeVolunteerMessageError").innerHTML = "";
    }
}


$('#TimeVolunteerMinute').on("input", TimeMinuteValidation)
function TimeMinuteValidation() {
    var TimeVolunteerMinute = document.getElementById("TimeVolunteerMinute").value;
    if (TimeVolunteerMinute === "") {
        document.getElementById("TimeVolunteerMinuteError").innerHTML = "Please enter a Volunteering Minutes";
        error = true;
    } else {
        document.getElementById("TimeVolunteerMinuteError").innerHTML = "";
    }
}

$('#TimeVolunteerHour').on("input", TimeHourValidation)
function TimeHourValidation() {
    var TimeVolunteerHour = document.getElementById("TimeVolunteerHour").value;

    if (TimeVolunteerHour === "") {
        document.getElementById("TimeVolunteerHourError").innerHTML = "Please enter a Voluntering Hours";
        error = true;
    } else {
        document.getElementById("TimeVolunteerHourError").innerHTML = "";
    }
}

$('#TimeVolunteerDate').on("input", TimeVolunteerDateValidation)
function TimeVolunteerDateValidation() {
    var TimeVolunteerDate = document.getElementById("TimeVolunteerDate").value;

    if (TimeVolunteerDate === "") {
        document.getElementById("TimeVolunteerDateError").innerHTML = "Please Select Date";
        error = true;
    } else {
        document.getElementById("TimeVolunteerDateError").innerHTML = "";
    }
}






function validateFormTimeTs() {
 
    var error = false;
    missionSelectionValidation();
    TimeMessageValidation();
    TimeVolunteerDateValidation();
    TimeHourValidation();
    TimeMinuteValidation();


    //if (TimeVolunteerHour === "") {
    //    document.getElementById("TimeVolunteerHourError").innerHTML = "Please enter a Voluntering Hours";
    //    error = true;
    //} else {
    //    document.getElementById("TimeVolunteerHourError").innerHTML = "";
    //}
    //if (TimeSheetMission === "") {
    //    document.getElementById("TimeSheetMissionError").innerHTML = "Please select Mission";
    //    error = true;
    //} else {
    //    document.getElementById("TimeSheetMissionError").innerHTML = "";
    //}
    //if (TimeVolunteerDate === "") {
    //    document.getElementById("TimeVolunteerDateError").innerHTML = "Please Select Date";
    //    error = true;
    //} else {
    //    document.getElementById("TimeVolunteerDateError").innerHTML = "";
    //}






    return !error;
}

function validateFormGoalTs() {
    var GoalVolunteerAction = document.getElementById("VolunteerAction").value;
    var GoalVolunteerDate = document.getElementById("GoalVolunteerDate").value;
    var GoalTimeSheetMission = document.getElementById('missionGoallist').value;
    var GoalVolunteerMessage = document.getElementById('GoalVolunteerMessage').value;
    var error = false;

    if (GoalVolunteerAction === "") {
        document.getElementById("GoalVolunteerActionError").innerHTML = "Please enter a Voluntering Goal Actions";
        error = true;
    } else {
        document.getElementById("GoalVolunteerActionError").innerHTML = "";
    }
    if (GoalTimeSheetMission === "Select your mission") {
        document.getElementById("GoalTimeSheetMissionError").innerHTML = "Please select Mission";
        error = true;
    } else {
        document.getElementById("GoalTimeSheetMissionError").innerHTML = "";
    }
    if (GoalVolunteerDate === "") {
        document.getElementById("GoalVolunteerDateError").innerHTML = "Please Select Date";
        error = true;
    } else {
        document.getElementById("GoalVolunteerDateError").innerHTML = "";
    }

    if (GoalVolunteerMessage.trim() === "") {
        document.getElementById("GoalVolunteerMessageError").innerHTML = "Please enter Your Work Description";
        error = true;
    } else {
        document.getElementById("GoalVolunteerMessageError").innerHTML = "";
    }

    return !error;
}