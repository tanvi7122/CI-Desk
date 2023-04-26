
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
                items += `<li> <div class="dropdown-item mb-1 ms-3 form-check"> <input type="checkbox" class="form-check-input" id="exampleCheck1" value=` + item.cityId + `><label class="form-check-label" for="exampleCheck1" value=` + item.cityId + `>` + item.name + `</label></div></li>`
            })
            dropdown.html(items);
        }
    });
}

$('.favorite-button').click(function () {

    var missionId = $(this).data('mission-id');
    $.ajax({
        url: '/Mission/AddToFavorites',
        type: 'POST',
        data: { missionId: missionId },
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


//$(".dropdown .CardsFilter").on('change', 'input[type="checkbox"]', function () {
//    console.log('hello');
function FilterMissions() {

    var selectedCities = $('#cityList input[type="checkbox"]:checked').map(function () {
        return $(this).next('label').text();
    }).get();
    console.log(selectedCities);

    var selectedThemes = $('#ThemeList input[type="checkbox"]:checked').map(function () {
        return $(this).next('label').text();
    }).get();
    console.log(selectedThemes);

    if (selectedCities.length === 0 && selectedThemes.length === 0) {
        $('.card-div').show();
    } else {
        //console.log(selectedCities);

        $('.card-div').each(function () {
            var cardCity = $(this).find('.mission-city').text();
            var cardTheme = $(this).find('.mission-theme').text();

            var cityFlag = selectedCities.some(function (selectedCity) {
                return selectedCity.trim().toUpperCase() == cardCity.trim().toUpperCase();
            });
            var themeFlag = selectedThemes.some(function (selectedTheme) {
                return selectedTheme.trim().toUpperCase() == cardTheme.trim().toUpperCase();
            });

            //if (cityFlag) {
            //    $(this).show();
            //} else {
            //    $(this).hide();
            //}
            if (selectedThemes.length === 0) {
                if (cityFlag) {
                    $(this).show();
                } else {
                    $(this).hide();
                }
            } else {
                if (cityFlag && themeFlag) {
                    $(this).show();
                } else {
                    $(this).hide();
                }
            }
        });
    }

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
            /*    filter()*/
            break;
    }
})





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

const resultDiv = document.getElementById('result');
const applyMissionBtn = document.getElementById('apply-mission-btn');

applyMissionBtn.addEventListener('click', function () {
    var missionId = applyMissionBtn.getAttribute('data-mission-id');
    var userId = applyMissionBtn.getAttribute('data-user-id');

    $.ajax({
        url: '/Mission/AddVolunteer',
        type: 'POST',
        data: { missionId: missionId, userId: userId },
        beforeSend: function () {
            console.log('Before sending AJAX request');
        },
        success: function (result) {
            resultDiv.innerHTML = 'Applied already';
            applyMissionBtn.style.backgroundColor = 'green';
            applyMissionBtn.style.color = 'white';
            applyMissionBtn.style.border = '2px solid green';
        },
        error: function (error) {
            resultDiv.innerHTML = 'Error: ' + error;
        }
    });
});





$('.recommend-btn').click(function () {
    var button = $(this)
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
        data: { missionId: missionId, fromuserId: fromuserId, touserId: touserId, theme: themeId, cityid: cityId, countryid:countryId },
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
$('.recommend-story-btn').click(function () {
    var button = $(this)
    var storyId = $(this).data('story-id');
    var missionId = $(this).data('mission-id');
    var fromuserId = $(this).data('fromuser-id');
    var touserId = $(this).data('touser-id');
    console.log(missionId);
    console.log(fromuserId);
    console.log(touserId);
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

    

