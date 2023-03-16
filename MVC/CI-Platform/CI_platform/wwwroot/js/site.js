﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//$('#searchForm').submit(function (event) {
//    event.preventDefault();
//    var searchString = $('#searchString').val();
//    $.ajax({
//        url: '@Url.Action("Search", "Landing_page")',
//        data: { searchString: searchString },
//        dataType: 'html',
//        success: function (data) {
//            // TODO: Handle the search results returned from the server
//        },
//        error: function (xhr, status, error) {
//            // TODO: Handle any errors that occur during the AJAX request
//        }
//    });
//});
FilterSortPaginationSearch();

var SelectedsortCase = null;
var SelectedCountry = null;



function FilterSortPaginationSearch() {
    console.log("HEllo");
    var CountryId = SelectedCountry;
    var CityId = $('#CityList input[type="checkbox"]:checked').map(function () { return $(this).val(); }).get().join();
    var ThemeId = $('#ThemeList input[type="checkbox"]:checked').map(function () { return $(this).val(); }).get().join();
    console.log(ThemeId);
    var SkillId = $('#SkillList input[type="checkbox"]:checked').map(function () { return $(this).val(); }).get().join();
    var SearchText = $('#search').val();
    var sortCase = SelectedsortCase;
    console.log("SORTCASE" + sortCase);
    var UserId = null;
    //console.log(SortCase);
    /* var UserId = $()*/

    $.ajax({
        type: "POST",
        url: "/Landing_pageController/PlatformLandingPost",
        data: { CountryId: CountryId, CityId: CityId, ThemeId: ThemeId, SkillId: SkillId, SearchText: SearchText, sortCase: sortCase, UserId: UserId },
        success: function (data) {
            console.log("This works");
            var view = $(".partialViews");
            view.empty();
            view.append(data);

        },
        error: function (error) {
            // Show an error message or handle the error
            console.log("error");
        }
    });
}

$("#sortByDropdown li").click(function () {
    var sortCase = $(this).val();
    SelectedsortCase = sortCase;
    console.log(sortCase);

    FilterSortPaginationSearch();
});

$("#CountryList li").click(function () {
    $(this).addClass('selected');

    var CountryId = $(this).val();
    SelectedCountry = CountryId;
    /* console.log(CountryId);*/

    //$('.card-div').each(function () {

    // var cardCountry = $(this).find('.mission-country').text();


    // if (CountryId == cardCountry) {
    // $(this).show();
    // } else {
    // $(this).hide();
    // }
    //});

    GetCitiesByCountry(CountryId);
    FilterSortPaginationSearch();
});

function GetCitiesByCountry(CountryId) {
    $.ajax({
        type: "GET",
        url: "/site/GetCitiesByCountry",
        data: { CountryId: CountryId },
        success: function (data) {
            var dropdown = $("#CityList");
            dropdown.empty();
            var items = "";
            $(data).each(function (i, item) {
                //items += "<option value=" + this.value + ">" + this.text + "</option>"
                items += `<li> <div class="dropdown-item mb-3 ms-3 form-check"> <input type="checkbox" class="form-check-input" id="exampleCheck1" value=` + item.cityId + `><label class="form-check-label" for="exampleCheck1" value=` + item.cityId + `>` + item.cityName + `</label></div></li>`
            })
            dropdown.html(items);
        }
    });

    $.ajax({
        type: "GET",
        url: "/Home/GetCitiesByCountry",
        data: { CountryId: CountryId },
        success: function (data) {
            var dropdown = $("#CityListAccordian");
            dropdown.empty();
            var items = "";
            $(data).each(function (i, item) {
                //items += "<option value=" + this.value + ">" + this.text + "</option>"
                items += `<li> <div class="dropdown-item mb-3 ms-3 form-check"> <input type="checkbox" class="form-check-input" id="exampleCheck1" value=` + item.cityId + `><label class="form-check-label" for="exampleCheck1" value=` + item.cityId + `>` + item.cityName + `</label></div></li>`
            })
            dropdown.html(items);
        }
    });
}


function GetCitiesByCountry(CountryId) {
    $.ajax({
        type: "GET",
        url: "/site/GetCitiesByCountry",
        data: { CountryId: CountryId },
        success: function (data) {
            var dropdown = $("#CityList");
            dropdown.empty();
            var items = "";
            $(data).each(function (i, item) {
                //items += "<option value=" + this.value + ">" + this.text + "</option>"
                
                items += `<li> <div class="dropdown-item mb-3 ms-3 form-check"> <input type="checkbox" class="form-check-input" id="exampleCheck1" value=` + item.cityId + `><label class="form-check-label" for="exampleCheck1" value=` + item.cityId + `>` + item.cityName + `</label></div></li>`
            })
            dropdown.html(items);
        }
    });
}





//$('.favourite').click(function () {
// var misssionId = $(this).data('mission-id');
// console.log(misssionId);

// $.ajax({
// url: '/Home/AddToFavourites',
// type: 'POST',
// data: { missionId: misssionId },
// success: function (result) {
// console.log(misssionId);
// var CardMissionId = $('.favourite')
// CardMissionId.each(function () {
// if ($(this).data('mission-id') === missionId) {
// if ($(this).hasClass('bi-heart')) {
// $(this).addClass('bi-heart-fill text-danger')
// $(this).removeClass('bi-heart text-light')
// console.log("added")
// }
// }
// });

// },
// error: function (error) {
// alert("An error occured. Please try later!!");
// }
// });
//});




//$('.favourite').click(function () {
// var button = $(this)
// var missionId = $(this).data('mission-id');
// console.log(missionId);
// $.ajax({
// url: '/Home/AddToFavourites',
// type: 'POST',
// data: { missionId: missionId },
// success: function (result) {
// // Show a success message or update the UI
// console.log(missionId)
// var allMissionId = $('.favourite')
// allMissionId.each(function () {
// if ($(this).data('mission-id') === missionId) {
// if ($(this).hasClass('bi-heart')) {
// $(this).addClass('bi-heart-fill text-danger')
// $(this).removeClass('bi-heart text-light')
// console.log("added")
// }
// else {
// $(this).addClass('bi-heart text-light')
// $(this).removeClass('bi-heart-fill text-danger')
// console.log("remove")
// }
// }
// })
// },
// error: function (error) {
// // Show an error message or handle the error
// console.log("error")

// }
// });
//});

//function addToFavourites(missionId) {
// $.ajax({
// type: "POST",
// url: "/Home/AddToFavourites",
// data: { missionId: missionId },
// success: function () {
// alert("Added to favourites!");

// },
// error: function () {
// alert("An error occurred.");
// }
// });
//}

//add to favourites

// get the button element
/*var addButton = document.getElementByClassName("add-to-favourites-button");*/

// add a click event listener to the button
//addButton.addEventListener("click", function () {
//});
// // get the data you want to insert into the database
// //var favouriteData = {
// // userId: "1234", // replace with the actual user ID
// // missionName: "My favourite mission",
// // missionDescription: "This is my favourite mission entry"
// //};



// // make an AJAX request to the server-side API endpoint
// var xhr = new XMLHttpRequest();
// xhr.open("POST", "/api/favourites", true);
// xhr.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
// xhr.onreadystatechange = function () {
// if (xhr.readyState === 4 && xhr.status === 200) {
// // do something if the insertion was successful
// console.log("Favourite mission inserted into database for user " + favouriteData.userId);
// }
// };
// xhr.send(JSON.stringify(favouriteData));

let filterPills = $('.filter-pills');
let allDropdowns = $('.dropdown ul');
allDropdowns.each(function () {
    let dropdown = $(this);
    $(this).on('change', 'input[type="checkbox"]', function () {

        // if the check box is checked then add it to pill
        if ($(this).is(':checked')) {
            let selectedOptionText = $(this).next('label').text();
            let selectedOptionValue = $(this).val();
            const closeAllButton = filterPills.children('.closeAll');

            // creating a new pill
            let pill = $('<span></span>').addClass('pill ');

            // adding the text to pill
            let pillText = $('<span></span>').text(selectedOptionText);
            pill.append(pillText);

            // add the close icon (bootstrap)
            let closeIcon = $('<span></span>').addClass('close').html(' x');
            pill.append(closeIcon);


            // for closing the pill when clicking on close icon
            closeIcon.click(function () {
                const pillToRemove = $(this).closest('.pill');
                pillToRemove.remove();
                // Uncheck the corresponding checkbox
                const checkboxElement = dropdown.find(`input[type="checkbox"][value="${selectedOptionValue}"]`);
                checkboxElement.prop('checked', false);
                FilterSortPaginationSearch();
                if (filterPills.children('.pill').length === 1) {
                    filterPills.children('.closeAll').remove();
                }

            });

            // Add "Close All" button
            if (closeAllButton.length === 0) {
                filterPills.append('<div class=" closeAll"><span>Close All</span></div>');
                filterPills.children('.closeAll').click(function () {
                    allDropdowns.find('input[type="checkbox"]').prop('checked', false);
                    filterPills.empty();
                    FilterSortPaginationSearch();

                });

                //add the pill before the close icon
                filterPills.prepend(pill);

            }
            else {
                filterPills.children('.closeAll').before(pill);
            }

        }
        // if the checkbox is not checked then we have to check for its value if it is exists in the pills section then we have to remove it
        else {
            let selectedOptionText = $(this).next('label').text() + ' x';
            let selectedOptionValue = $(this).val();
            $('.pill').each(function () {
                const pillText = $(this).text();
                if (pillText === selectedOptionText) {
                    $(this).remove();
                }
            });
            if ($('.pill').length === 1) {
                $('.closeAll').remove();
            }
        }

        //FilterMissions();
        FilterSortPaginationSearch();
    });

})
