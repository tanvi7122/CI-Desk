
$(document).ready(function () {
    GetCities();
});
$('#Country').on('change', GetCities);
function GetCities() {
    var country = $('#Country').val();
    if (country) {
        $.ajax({
            url: '/User/GetCities',
            data: { country: country },
            success: function (data) {

                $('#city').empty();
                var items = "";
                $(data).each(function (i, item) {
                    items += `<option value=` + item.cityId + `>` + item.name + `</option>`
                })
                console.log(items);
                $('#city').html(items);
            }
        });
    } else {
        $('#city').empty();
    }
}
let userskill = new Set();
window.addEventListener("load", function () {
    $("#selectedSkill option").each(function () {
        var skillId = $(this).val();
        userskill.add(skillId);
    });
})
let array = new Array();
function rightClick() {
    $("#selectSkill option:selected").each(function () {
        var temp = $(this).text();
        var skillId = $(this).val();
        console.log(temp);
        console.log(skillId);

        var obj = 0;
        $("#selectedSkill option").each(function () {

            if ($(this).val() == skillId) {
                obj = 1;
            }
        });
        if (obj == 0) {
            $("#selectedSkill").append($(`<option class="p-1" value=` + skillId + `>` + temp + `</option>`));
            userskill.add(skillId);
        }
    });
}

function leftClick() {
    $("#selectedSkill option:selected").each(function () {
        var skillId = $(this).remove().val();

        userskill.delete(skillId);
    });
}

function addingSkill() {
    var data = $("#selectedSkill option");

    console.log(data);
    console.log(userskill);
    array = [...userskill];
    console.log(array);
    $("#skills").empty();
    $.each($(data), function (index, val) {

        $("#skills").append($('<h6></h6>').text(val.text));
    });
    $("#skills").css("border", "1px solid #dee2e6");
    $("#skills").css("margin-top", "20px");
    $("#skills").css("padding", "10px 0px 10px 15px");
    $("#skills").css("height", "120px");
    var userAddedSkill = {
        UserId: $('#skillSave').data('user-id'),
        SkillIds: array,
    }
    console.log(userAddedSkill);
    $.ajax({
        url: '/User/UserSkill',
        type: 'Post',
        data: userAddedSkill,
        contentType: 'application/x-www-form-urlencoded;charset=utf-8;',
        success: function (response) {
            /* $('#partial-UserProfile').html(response);*/
            console.log(userAddedSkill);
        },
        error: function () {
            alert("hello wolrd");
        }
    })
};

function updateUserProfile() {
    var LoggerUserId = document.getElementById('loggerUser').value;
    var UserProfile = {
        WhyIVolunteer: $('#volunteerReason').val(),
        Department: $('#department').val(),
        ProfileText: $('#profile').val(),
        LinkedInUrl: $('#linkedIn').val(),
        Title: $('#title').val(),
        CityId: $('#city option').val(),
        CountryId: $('#Country').val(),
        FirstName: $('#name').val(),
        LastName: $('#surname').val(),
        EmployeeId: $('#employee').val(),
     
        UserId: $('UserId').val(),
    }

    $.ajax({
        url: '/User/UpdateUser',
        type: 'Post',
        data: UserProfile,
        contentType: 'application/x-www-form-urlencoded;charset=utf-8;',

        success: function (response) {
            ///*         $('#partial-UserProfile').html(response);*/
            // $('body').html(response);
            //console.log(response);
            //GetCities();

            window.location.reload();

        },
        error: function (error) {
            alert("hello world");
        }

    })
}


function passwordChange() {

}



function verifyFileUpload(e) {
    var file = document.getElementById("avatar-upload");
    var LoggerUserId = document.getElementById('loggerUser').value;
    if (file && file.files.length > 0) {
        var img = new Image();
        var flag = 0;
        img.src = window.URL.createObjectURL(file.files[0]);
        img.onload = function () {
            var width = this.naturalWidth,
                height = this.naturalHeight;

            console.log("Image Width: " + width);
            console.log("Image Height: " + height);
            if (this.naturalHeight > 100 || this.naturalWidth > 100) {
                alert('The selected image exceeds the maximum allowed dimensions of ' + 100 + ' x ' + 100 + ' pixels.');
                flag = 1;
            }
            if (flag == 0) {
                var formData = new FormData();

                // Add the selected file to the FormData object
                formData.append('avatarFile', file.files[0]);
                formData.append('UserId', LoggerUserId);


                console.log(formData);

                // Send an AJAX request to upload the file
                $.ajax({
                    url: '/User/UploadAvatar',
                    type: 'POST',
                    data: formData,
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        console.log(data);
                        // Update the user's avatar image in the view
                        $('#avatar-img').attr('src', data.avatar);
                        $('#header-avatar').attr('src', data.avatar);
                    }
                });
            }
        };

    }
}

        
    


$('#avatar-img').click(function () {
    $('#avatar-upload').click();
});










