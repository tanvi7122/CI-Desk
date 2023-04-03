﻿
var selectedCountry = null;
var selectedSortCase = null;
var currentUrl = window.location.href;
let allDropdowns = $('.dropdown ul');
var pagesize = 9;

spFilterSortSearchPagination(1);


$('#searchText').on('keyup', function () {

    spFilterSortSearchPagination();

});

allDropdowns.on('change', function () {

    spFilterSortSearchPagination();

});

function spFilterSortSearchPagination(pageNo) {
    var CountryId = selectedCountry;
    var CityId = $('#cityList input[type="checkbox"]:checked').map(function () { return $(this).val(); }).get().join();
    var ThemeId = $('#ThemeList input[type="checkbox"]:checked').map(function () { return $(this).val(); }).get().join();
    var SkillId = $('#SkillList input[type="checkbox"]:checked').map(function () { return $(this).val(); }).get().join();
    var searchText = $("#searchText").val();
    var sortCase = selectedSortCase;
    $.ajax({
        type: 'POST',
        url: '/Mission/HomePage',
        data: { CountryId: CountryId, CityId: CityId, ThemeId: ThemeId, SkillId: SkillId, searchText: searchText, sortCase: sortCase, UserId: UserId, pageNo: pageNo, pagesize: pagesize },
        success: function (data) {
            var view = $('#partialViews');
            view.empty();
            view.append(data);
            totalMission();
            console.log(data);
            if (document.getElementById('missionCount') != null) {
                var totalRecords = document.getElementById('missionCount').innerText;
            }
            let totalPages = Math.ceil(totalRecords / pagesize);

            if (totalPages <= 1) {
                $('#pagination-container').parent().parent().hide();
            }
            let paginationHTML = `
            <li class="page-item">
            <a class="pagination-link first-page" aria-label="Previous">
            <span aria-hidden="true"><img src="/images/previous.png" /></span>
            </a>
            </li>
            <li class="page-item">
            <a class="pagination-link previous-page" aria-label="Previous">
            <span aria-hidden="true"><img src="/images/left.png" /></span>
            </a>
            </li>`;

            for (let i = 1; i <= totalPages; i++) {
                let activeClass = '';
                if (i === (pageNo === undefined ? 1 : pageNo)) {
                    activeClass = ' active';
                }
                paginationHTML += `
                <li class="page-item ${activeClass}">
                <a class="pagination-link" data-page="${i}">${i}</a>
                </li>`;
            }

            paginationHTML += `
            <li class="page-item">
            <a class="pagination-link next-page" aria-label="Next">
            <span aria-hidden="true"><img src="/images/right-arrow1.png" /></span>
            </a>
            </li>
            <li class="page-item">
            <a class="pagination-link last-page" aria-label="Next">
            <span aria-hidden="true"><img src="/images/next.png" /></span>
            </a>
            </li>`;

            $('#pagination-container').empty()
            $('#pagination-container').append(paginationHTML)
            $('#pagination-container').parent().parent().show();



            // pagination
            let currentPage;

            $(document).on('click', '.pagination li', (function () {
                $('.pagination li').each(function () {
                    if ($(this).hasClass('active')) {

                        currentPage = $(this).find('a').data('page');
                        $(this).removeClass('active');
                    }
                })
                pageNo = currentPage;
                if ($(this).find('a').hasClass('first-page')) {
                    pageNo = 1;
                    currentPage = pageNo;
                    $('.pagination li').find('a').each(function () {
                        if ($(this).data('page') == 1) {
                            $(this).parent().addClass('active')
                        }
                    })
                }
                else if ($(this).find('a').hasClass('last-page')) {
                    pageNo = totalPages;
                    currentPage = pageNo;
                    $('.pagination li').find('a').each(function () {
                        if ($(this).data('page') == totalPages) {
                            $(this).parent().addClass('active')
                        }
                    })
                }
                else if ($(this).find('a').hasClass('previous-page')) {
                    if (currentPage > totalPages) {
                        pageNo = currentPage - 1;
                    }
                    $('.pagination li').find('a').each(function () {
                        if ($(this).data('page') == pageNo) {
                            $(this).parent().addClass('active')
                        }
                    })
                    currentPage = pageNo;

                } else if ($(this).find('a').hasClass('next-page')) {
                    if (currentPage < totalPages) {
                        pageNo = currentPage + 1;
                    }

                    $('.pagination li').find('a').each(function () {
                        if ($(this).data('page') == pageNo) {
                            $(this).parent().addClass('active')
                        }
                    })
                    currentPage = pageNo;

                } else {
                    $(this).addClass('active')

                    pageNo = $(this).find('a').data('page');
                    currentPage = pageNo;

                }
                spFilterSortSearchPagination(pageNo);
            }));
        },
        error: function (error) {
            console.log(error)
        }
    });
}


function totalMission() {
    if (document.getElementById('missionCount') != null) {

        var count = document.getElementById('missionCount').innerText;
        $('#exploreText').text("Explore " + count + " missions");

        if (count == 0) {
            $('.NoMissionFound').show();

        }
        else {
            $('.NoMissionFound').hide();
        }
    }

}


$("#sortList li").click(function () {
    selectedSortCase = $(this).val();
    spFilterSortSearchPagination();
});

$("#CountryList li").click(function () {

    var countryId = $(this).val();
    selectedCountry = countryId;
    console.log(selectedCountry);

    GetCitiesByCountry(countryId);
    spFilterSortSearchPagination();


});

function GetCitiesByCountry(countryId) {
    $.ajax({
        type: "GET",
        url: "/Mission/GetCitiesByCountry",
        data: { countryId: countryId },
        success: function (data) {
            var dropdown = $("#cityList");
            dropdown.empty();
            var items = "";
            $(data).each(function (i, item) {
                items += `<li> <div class="dropdown-item mb-1 ms-3 form-check"> <input type="checkbox" class="form-check-input" id="exampleCheck1" value =` + item.cityId + `><label class="form-check-label" for="exampleCheck1" value=` + item.cityId + `>` + item.name + `</label></div></li>`
            })
            dropdown.html(items);

            var dropdown = $("#cityListAccordian");
            dropdown.empty();
            var items = "";
            //console.log(data);
            $(data).each(function (i, item) {
                //console.log(item);
                items += `<li> <div class="dropdown-item mb-1 form-check"> <input type="checkbox"  class="form-check-input" id="exampleCheck1" value =` + item.cityId + `><label class="form-check-label" for="exampleCheck1" value=` + item.cityId + `>` + item.name + `</label></div></li>`

            })
            dropdown.html(items);
        }
    });
}

let filterPills = $('.filter-pills');
allDropdowns.each(function () {
    let dropdown = $(this);
    $(this).on('change', 'input[type="checkbox"]', function () {

        // if the check box is checked then add it to pill
        if ($(this).is(':checked')) {
            let selectedOptionText = $(this).next('label').text();
            let selectedOptionValue = $(this).val();
            const closeAllButton = filterPills.children('.closeAll');

            // creating a new pill
            let pill = $('<div></div>').addClass('pill ');

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
                spFilterSortSearchPagination();

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

                    spFilterSortSearchPagination();


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


        spFilterSortSearchPagination();


    });
});





//$('.favorite-button').click(function () {

//    var missionId = $(this).data('mission-id');
//    $.ajax({
//        url: '/Mission/AddToFavorites',
//        type: 'POST',
//        data: { missionId: missionId },
//        success: function (result) {
//            // Show a success message or update the UI
//            console.log(missionId)
//            var allMissionId = $('.favorite-button')
//            allMissionId.each(function () {
//                if ($(this).data('mission-id') === missionId) {
//                    if ($(this).hasClass('bi-heart')) {
//                        $(this).addClass('bi-heart-fill text-danger')
//                        $(this).removeClass('bi-heart text-light')
//                        console.log("added")
//                    }
//                    else {
//                        $(this).addClass('bi-heart text-light')
//                        $(this).removeClass('bi-heart-fill text-danger')
//                        console.log("remove")
//                    }
//                }
//            })
//        },
//        error: function (error) {
//            // Show an error message or handle the error
//            console.log("error")

//        }
//    });
//});



$('.favorite-button').click(function () {
    var button = $(this)
    var missionId = $(this).data('mission-id');
    var userId = $(this).data('user-id');
    console.log(missionId);
    console.log(userId);
    $.ajax({
        url: '/Mission/AddToFavorites',
        type: 'POST',
        data: { missionId: missionId, userId: userId },
        success: function (result) {
            // Show a success message or update the UI
            console.log(missionId)
            console.log(userId)
            var allMissionId = $('.favorite-button')
            allMissionId.each(function () {
                if ($(this).data('mission-id') === missionId) {
                    if ($(this).hasClass('bi-heart')) {
                        $(this).addClass('bi-heart-fill text-danger')
                        $(this).removeClass('bi-heart')
                        /*     $("#mission-fav-id").html("")*/
                        console.log("added")
                    }
                    else {
                        $(this).addClass('bi-heart ')
                        $(this).removeClass('bi-heart-fill text-danger')
                        console.log("remove")
                    }
                }
            })
        },
        error: function (error) {
            // Show an error message or handle the error
            console.log("error")

        }
    });
});


$('.favorite-button').click(function () {
    var button = $(this)
    var missionId = $(this).data('mission-id');
    var userId = $(this).data('user-id');
    console.log(missionId);
    console.log(userId);
    $.ajax({
        url: '/Mission/AddToFavorites',
        type: 'POST',
        data: { missionId: missionId, userId: userId },
        success: function (result) {
            // Show a success message or update the UI
            console.log(missionId)
            console.log(userId)
            var allMissionId = $('.favorite-button')
            allMissionId.each(function () {
                if ($(this).data('mission-id') === missionId) {
                    if ($(this).hasClass('bi-heart')) {
                        $(this).addClass('bi-heart-fill text-danger')
                        $(this).removeClass('bi-heart')
                        /*     $("#mission-fav-id").html("")*/
                        console.log("added")
                    }
                    else {
                        $(this).addClass('bi-heart ')
                        $(this).removeClass('bi-heart-fill text-danger')
                        console.log("remove")
                    }
                }
            })
        },
        error: function (error) {
            // Show an error message or handle the error
            console.log("error")

        }
    });
});



$('.recommend-btn').click(function () {
    var button = $(this);
    var missionId = $(this).data('mission-id');
    var fromuserId = $(this).data('fromuser-id');
    var touserId = $(this).data('touser-id');
    var themeId = $(this).data('theme-id');
    var cityId = $(this).data('city-id');
    var countryId = $(this).data('country-id');
    console.log(missionId);
    console.log(fromuserId);
    console.log(touserId);
    $.ajax({
        url: '/Mission/RecommendToCoWorker',
        type: 'POST',
        data: { missionId: missionId, fromuserId: fromuserId, touserId: touserId, theme: themeId, cityid: cityId, countryid: countryId },
        success: function (result) {
            // Show a success message or update the UI
            console.log(missionId)
            console.log(fromuserId)
            console.log(touserId)
            var allrecommendedId = $('.recommend-btn')
            allrecommendedId.each(function () {
                if ($(this).data('mission-id') === missionId && $(this).data('fromuser-id') == fromuserId && $(this).data('touser-id') == touserId) {
                    if ($(this).hasClass('btn-primary')) {
                        $(this).addClass('btn-success')
                        $(this).removeClass('btn-primary')
                        $(this).html('Recommended')
                        console.log("added")
                    }
                    else {
                        $(this).addClass('btn-primary')
                        $(this).removeClass('btn-success')
                        $(this).html('Recommend')
                        console.log("remove")
                    }
                }
            })
        },
        error: function (error) {
            // Show an error message or handle the error
            console.log("error")

        }
    });
});



$('.post-comment').click(function () {
    var button = $(this);
    var missionId = $(this).data('mission-id');
    var userId = $(this).data('user-id');
    var comment_text = $('#comment_text').val();
    console.log(missionId);
    console.log(userId);
    console.log(comment_text);
    $.ajax({
        url: '/Mission/AddComment',
        type: 'POST',
        data: { missionId: missionId, userId: userId, comment_text: comment_text },
        success: function (result) {
            console.log("comment added");
            $('#comment_text').reset();

        },
        error: function (error) {
            // Show an error message or handle the error
            console.log("error")

        }
    });
});



$('.apply-mission-btn').click(function () {
    var button = $(this);
    var missionId = $(this).data('mission-id');
    var userId = $(this).data('user-id');

    console.log(missionId);
    console.log(userId);

    $.ajax({
        url: '/Mission/AddVolunteer',
        type: 'POST',
        data: { missionId: missionId, userId: userId },
        success: function (result) {
            console.log("Application added");
        },
        error: function (error) {
            // Show an error message or handle the error
            console.log("error")
        }
    });
});


$(".Rating p").click(function () {

    var rate = $(this).index() + 1;
    var selectedIcon = $(this).prevAll().addBack();
    var unselectedIcon = $(this).nextAll();
    var missionId = $(this).data('mission-id');
    var userId = $(this).data('user-id');
    console.log(rate);


    $.ajax({
        type: "GET",
        url: "/Mission/AddRating",
        data: { missionid: missionId, rating: rate, userId: userId },
        success: function (response) {
            console.log("rated succesfully");

            selectedIcon.removeClass('unselectedstar').addClass('selectedstar');
            unselectedIcon.removeClass('selectedstar').addClass('unselectedstar');

        },
        error: function (error) {
            console.log(error)
        }
    })
})