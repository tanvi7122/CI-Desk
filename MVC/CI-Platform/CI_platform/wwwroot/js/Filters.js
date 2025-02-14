﻿
function search() {
    // Declare variables
    var input, filter;
    input = document.getElementById("searchField");
    console.log(input)
    filter = input.value.toUpperCase();
    cards = document.getElementsByClassName("card-div");
    titles = document.getElementsByClassName("card-title");

    // Loop through all list items, and hide those who don't match the search query
    for (i = 0; i < cards.length; i++) {
        a = titles[i];
        if (a.innerHTML.toUpperCase().indexOf(filter) > -1) {
            cards[i].classList.remove("d-none");
        } else {
            cards[i].classList.add("d-none");
        }
    }
}

$('.favorite-button').click(function () {

    var missionId = $(this).data('mission-id');
    var userId = $(this).data('user-id');
    $.ajax({
        url: '/Mission/AddToFavorites',
        type: 'POST',
        data: { missionId: missionId, userId: userId },
        success: function (result) {
            // Show a success message or update the UI
            console.log(missionId)
            var allMissionId = $('.favorite-button')
            allMissionId.each(function () {
                if ($(this).data('mission-id') === missionId) {
                    if ($(this).hasClass('bi-heart')) {
                        $(this).addClass('bi-heart-fill text-danger')
                        $(this).removeClass('bi-heart text-light')
                        console.log("added")
                    }
                    else {
                        $(this).addClass('bi-heart text-light')
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



//let filterpills = $('#filter-items');
//let alldropdowns = $('.dropdown ul');
//alldropdowns.each(function () {

//    let dropdown = $(this);
//    $(this).on('change', 'input[type="checkbox"]', function () {

//        /*// if the check box is checked then add it to pill*/
//        if ($(this).is(':checked')) {
//            let selectedoptiontext = $(this).next('label').text();
//            console.log(selectedoptiontext);
//            let selectedoptionvalue = $(this).val();
//            console.log(selectedoptionvalue);
//            const closeallbutton = filterpills.children('.closeall');

//            // creating a new pill
//            let pill = $('<div></div>').addclass('pill');

//            // adding the text to pill
//            let pilltext = $('<span></span>').text(selectedoptiontext);
//            pill.append(pilltext);

//            // add the close icon (bootstrap)
//            let closeicon = $('<span></span>').addclass('close').html(' x');
//            pill.append(closeicon);


//            // for closing the pill when clicking on close icon
//            closeicon.click(function () {
//                const pilltoremove = $(this).closest('.pill');
//                pilltoremove.remove();
//                // uncheck the corresponding checkbox
//                const checkboxelement = dropdown.find(`input[type="checkbox"][value="${selectedoptionvalue}"]`);
//                checkboxelement.prop('checked', false);
//                if (filterpills.children('.pill').length === 1) {
//                    filterpills.children('.closeall').remove();
//                }

//                filtermissions();
//            });

//            // add "close all" button
//            if (closeallbutton.length === 0) {
//                filterpills.append('<div class=" closeall"><span>close all</span></div>');
//                filterpills.children('.closeall').click(function () {
//                    alldropdowns.find('input[type="checkbox"]').prop('checked', false);
//                    filterpills.empty();

//                    filtermissions();

//                });

//                //add the pill before the close icon
//                filterpills.prepend(pill);

//            }
//            else {
//                filterpills.children('.closeall').before(pill);
//            }

//        }
//        // if the checkbox is not checked then we have to check for its value if it is exists in the pills section then we have to remove it
//        else {
//            let selectedoptiontext = $(this).next('label').text() + ' x';
//            let selectedoptionvalue = $(this).val();
//            $('.pill').each(function () {
//                const pilltext = $(this).text();
//                if (pilltext === selectedoptiontext) {
//                    $(this).remove();
//                }
//            });
//            if ($('.pill').length === 1) {
//                $('.closeall').remove();
//            }
//        }

//        filtermissions();
//    });

//})


$("#CountryList li").click(function () {

    var countryId = $(this).val();
    console.log(countryId);

    $('.card-div').each(function () {
        var cardCountry = $(this).find('.mission-country').text();
        //console.log(cardCountry);

        if (countryId == cardCountry) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
    GetCitiesByCountry(countryId);
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
                items += `<li> <div class="dropdown-item mb-1 ms-3 form-check"> <input type="checkbox" class="form-check-input" id="cityList" value=` + item.cityId + `><label class="form-check-label" for="cityList" value=` + item.cityId + `>` + item.name + `</label></div></li>`
            })
            dropdown.html(items);
            $("#cityList input[type='checkbox']").click(function () {
                FilterMissions();
            });
        }
    });
}
function FilterMissions() {
    var selectedCities = $('#cityList input[type="checkbox"]:checked').map(function () {
        return $(this).next('label').text();
    }).get();
    console.log(selectedCities);

    var selectedThemes = $('#ThemeList input[type="checkbox"]:checked').map(function () {
        return $(this).next('label').text();
    }).get();
    console.log(selectedThemes);

    var selectedSkills = $('#SkillList input[type="checkbox"]:checked').map(function () {
        return $(this).parent().find('label').text().trim();
    }).get().filter(function (value) {
        return value !== '';
    });

    console.log(selectedSkills);

    var $cards = $('.card-div');

    $cards.each(function () {
        var cardCity = $(this).find('.mission-city').text();
        var cardTheme = $(this).find('.mission-theme').text();
        var cardSkills = $(this).find('.mission-skills').text().trim().replace(/\s+/g, ' ');

        var cityFlag = selectedCities.length === 0 || selectedCities.some(function (selectedCity) {
            return selectedCity.trim().toUpperCase() == cardCity.trim().toUpperCase();
        });

        var themeFlag = selectedThemes.length === 0 || selectedThemes.some(function (selectedTheme) {
            return selectedTheme.trim().toUpperCase() == cardTheme.trim().toUpperCase();
        });
        console.log(`Selected Skills: ${selectedSkills}, Card Skills: ${cardSkills}`);

        var skillFlag = selectedSkills.length === 0 || selectedSkills.some(function (selectedSkill) {
            var skills = cardSkills.split(' ');
            return skills.some(function (skill) {
                return selectedSkill.trim().toUpperCase() == skill.trim().toUpperCase();
            });
        });



        if (cityFlag && themeFlag && skillFlag) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}




$('#sortByDropdown li').on('click', function () {
    selectedSortOption = $(this).find('a').text();

    let gridCardsContainer = $('.grid-card').parent().parent();
    let listCardsContainer = $('.list-card').parent();
    var dateArray = [];
    var SeatLeftArray = [];
    switch (selectedSortOption) {
        case 'Newest':
            let cardsDateForNewest = $('.card').find('.created-date');
            cardsDateForNewest.each(function (j) {
                dateArray.push($(this).text());
            });
            dateArray = $.unique(dateArray)
            // Arrange array Elemeny in Ascending order
            dateArray.sort();

            // Arrange Array Element In Descending order
            dateArray.reverse();
            dateArray = $.unique(dateArray)
            for (var i = 0; i < dateArray.length; i++) {
                $('.grid-card').each(function () {
                    if ($(this).find('.created-date').text() == dateArray[i]) {
                        $(this).parent().appendTo($(gridCardsContainer));
                    }
                });
            }
            for (var i = 0; i < dateArray.length; i++) {
                $('.list-card').each(function () {
                    if ($(this).find('.created-date').text() == dateArray[i]) {
                        $(this).parent().appendTo($(listCardsContainer));
                    }
                });
            }

            break;
        case 'Oldest':
            let cardsDateForOldest = $('.card').find('.created-date')
            var dateArray = [];
            cardsDateForOldest.each(function (j) {
                dateArray.push($(this).text());
            });
            // Arrange array Elemeny in Ascending order
            dateArray = $.unique(dateArray)
            dateArray.sort(function (a, b) {
                return new Date(a) - new Date(b);
            });
            console.log(dateArray)

            for (var i = 0; i < dateArray.length; i++) {
                $('.grid-card').each(function () {
                    if ($(this).find('.created-date').text() == dateArray[i]) {
                        console.log(true)

                        $(this).parent().appendTo($(gridCardsContainer));
                    }
                });
            }
            for (var i = 0; i < dateArray.length; i++) {
                $('.list-card').each(function () {
                    if ($(this).find('.created-date').text() == dateArray[i]) {
                        console.log(true)

                        $(this).parent().appendTo($(listCardsContainer));
                    }
                });
            }

            break;
        case 'Lowest available seats':
            let cardsSeatLeftForLowest = $('.card').find('.seat-left');
            cardsSeatLeftForLowest.each(function (j) {
                SeatLeftArray.push(parseInt($(this).text()));
            });

            SeatLeftArray = $.unique(SeatLeftArray)
            // Arrange array Elemeny in Ascending order

            SeatLeftArray.sort(function (a, b) {
                return a - b
            });

            for (var i = 0; i < SeatLeftArray.length; i++) {
                $('.grid-card').each(function () {
                    if ($(this).find('.seat-left').text() == SeatLeftArray[i]) {
                        $(this).parent().appendTo($(gridCardsContainer));
                    }
                });
            }
            for (var i = 0; i < SeatLeftArray.length; i++) {
                $('.list-card').each(function () {
                    if ($(this).find('.seat-left').text() == SeatLeftArray[i]) {
                        $(this).parent().appendTo($(listCardsContainer));
                    }
                });
            }

            break;
        case 'Highest available seats':

            let cardsSeatLeftForHighest = $('.card').find('.seat-left');
            cardsSeatLeftForHighest.each(function (j) {
                SeatLeftArray.push(parseInt($(this).text()));
            });

            SeatLeftArray = $.unique(SeatLeftArray)
            // Arrange array Elemeny in Ascending order

            SeatLeftArray.sort(function (a, b) {
                return a - b
            });

            // Arrange array Element in Descending order
            SeatLeftArray.reverse();

            for (var i = 0; i < SeatLeftArray.length; i++) {
                $('.grid-card').each(function () {
                    if ($(this).find('.seat-left').text() == SeatLeftArray[i]) {
                        $(this).parent().appendTo($(gridCardsContainer));
                    }
                });
            }
            for (var i = 0; i < SeatLeftArray.length; i++) {
                $('.list-card').each(function () {
                    if ($(this).find('.seat-left').text() == SeatLeftArray[i]) {
                        $(this).parent().appendTo($(listCardsContainer));
                    }
                });
            }
            break;

        case 'My favourites':
            var $cards = $('.card-div');
            var cardsArray = $cards.toArray();
            console.log(cardsArray)
            cardsArray.sort(function (a, b) {
                var favA = $(a).find('.mission-favorite').data('favorite');
                var favB = $(b).find('.mission-favorite').data('favorite');
                return favB - favA;
            });
          
            $('.mission-container').empty();
            for (var i = 0; i < cardsArray.length; i++) {
                $('.mission-container').append(cardsArray[i]);
            }
            break;

        case 'Registration deadline':
            let deadlines = $('.card').find('.deadline')
            var dateArray = [];
            deadlines.each(function (j) {
                dateArray.push($(this).text());
            });
            dateArray.sort(function (a, b) {
                var dateA = new Date(
                    parseInt(a.substring(6)),
                    parseInt(a.substring(3, 5)) - 1,
                    parseInt(a.substring(0, 2))
                );
                var dateB = new Date(
                    parseInt(b.substring(6)),
                    parseInt(b.substring(3, 5)) - 1,
                    parseInt(b.substring(0, 2))
                );
                return dateA - dateB;
            });
            //dateArray = $.unique(dateArray)
            // Arrange array Elemeny in Ascending order
            //dateArray.sort();
            console.log(dateArray)
            for (var i = 0; i < dateArray.length; i++) {
                $('.grid-card').each(function () {
                    if ($(this).find('.deadline').text() == dateArray[i]) {

                        $(this).parent().appendTo($(gridCardsContainer));
                    }
                });
            }
            for (var i = 0; i < dateArray.length; i++) {
                $('.list-card').each(function () {
                    if ($(this).find('.deadline').text() == dateArray[i]) {
                        console.log(true)

                        $(this).parent().appendTo($(listCardsContainer));
                    }
                });
            }
        
            break;
    }
})




$('.like').click(function () {
    var button = $(this);
    var missionId = $(this).data('mission-id');
    var userId = $(this).data('user-id');
    console.log(missionId);
    console.log(userId);
    $.ajax({
        url: '/Mission/AddToFavorite',
        type: 'POST',
        data: { missionId: missionId, userId: userId },
        success: function (result) {
            // Show a success message or update the UI
            console.log(missionId)
            console.log(userId)
            button.find('i').toggleClass('bi-heart bi-heart-fill text-danger');
            if (button.find('i').hasClass('bi-heart-fill text-danger')) {
                button.find('i').text('Added To Favourites');
            } else {
                button.find('i').text('Add To Favourites');
            }
        },
        error: function (error) {
            // Show an error message or handle the error
            console.log("error")
        }
    });
});






function RecommendMission(missionId, fromuserId, touserId, themeId, countryId, cityId) {

    $.ajax({
        url: '/Mission/RecommendToCoWorker',
        type: 'POST',
        data: {
            missionId: missionId, fromuserId: fromuserId, touserId: touserId, themeId: themeId, countryId: countryId, cityId: cityId
        },
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
};
function RecommendStory(storyId, missionId, fromuserId, touserId) {
    $.ajax({
        url: '/Story/RecommendToCoWorker',
        type: 'POST',
        data: { storyId: storyId, missionId: missionId, fromuserId: fromuserId, touserId: touserId },
        success: function (result) {
            // Show a success message or update the UI
            console.log(missionId)
            console.log(fromuserId)
            console.log(touserId)
            var allrecommendedId = $('.recommend-story-btn')
            allrecommendedId.each(function () {
                if ($(this).data('story-id') === storyId && $(this).data('mission-id') === missionId && $(this).data('fromuser-id') == fromuserId && $(this).data('touser-id') == touserId) {
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
};



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



//function submitForm() {
//    var data = $('#contact-form').serialize();

//    $.ajax({
//        type: 'POST',
//        url: '/Mission/ContactUs',
//        data: data,
//        success: function (response) {
//            if (response.success) {
//                // show success message
//                Swal.fire({
//                    icon: 'success',
//                    title: 'Success!',
//                    text: response.message
//                });
//                // clear form inputs
//                $('#contact-form')[0].reset();
//            } else {
//                // show error message
//                Swal.fire({
//                    icon: 'error',
//                    title:
//}

    

