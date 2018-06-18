$(function () {
    /********** General **********/

    //Initialize Bootstrap tooltips
    $('[data-toggle="tooltip"]').tooltip();

    //Update image modals
    $('.image-modal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);

        var title = button.data('title');
        var imageSource = button.data('img-src');

        var modal = $(this);
        modal.find('.modal-title').text(title);
        modal.find('img').attr('src', imageSource);
    });

    //Toggled open/closed icon on collapsible cards
    $('.collapse-card').on('click', function () {
        $(this)
            .find('[data-fa-i2svg]')
            .toggleClass('fa-chevron-up')
            .toggleClass('fa-chevron-down');
    });

    /********** Movie Cards **********/

    //Sort movie cards by type
    $(document).on('click', '.sort-movies', function () {
        var activeSortItem = $(this);
        var movieSorter = activeSortItem.closest('.movie-sorter');

        movieSorter.find('.dropdown-item.sort-movies.active').removeClass('active text-white');
        activeSortItem.addClass('active text-white');
        movieSorter.find('#sort-movies-dropdown').text(this.text);

        sortMovies(movieSorter);
    });

    //Toggle ascending/descending sort for movie cards
    $(document).on('click', '#sort-direction', function () {
        var activeSortItem = $(this);
        activeSortItem.attr('data-sort', activeSortItem.attr('data-sort') === 'desc' ? 'asc' : 'desc');
        activeSortItem.find('[data-fa-i2svg]').toggleClass('fa-arrow-up').toggleClass('fa-arrow-down');

        var movieSorter = activeSortItem.closest('.movie-sorter');
        sortMovies(movieSorter);
    });

    /********** Edit Movie **********/

    //jQuery breaks validation for the Runtime TimeSpan, but server-side works: https://stackoverflow.com/a/18625285
    $('#Runtime').rules('remove', 'range');

    //Initialize AJAX Bootstrap Select lists on Cast/Crew modal open for faster load times
    $('#cast-modal, #crew-modal').on('show.bs.modal', function () {
        initializePersonPicker('#' + $(this).attr('id'));

        if ($(this).is('#crew-modal')) {
            initializeDepartmentPicker('');
        }
    });

    //Add New Cast/Crew Member
    $('#cast-modal, #crew-modal').on('click', '.add-person', function () {
        var modalId = '#' + $(this).closest('.modal').attr('id');
        var url = modalId === '#cast-modal' ? '../../Movie/AddCastMember' : '../../Movie/AddCrewMember';
        var nextIndex = $(modalId + ' table tbody tr').length;

        $.ajax({
            url: url,
            data: { index: nextIndex },
            type: 'GET'
        }).done(function (response) {
            $(modalId + ' table tbody').append(response);
            var newRow = modalId + ' table tr:last';

            //Initialize AJAX Bootstrap Select lists for the new dropdown(s) in the new row
            initializePersonPicker(newRow);

            if (modalId === '#crew-modal') {
                initializeDepartmentPicker(newRow);
            }
        });
    });

    //Delete Cast/Crew Member
    $('#cast-table, #crew-table').on('click', '.delete-person', function () {
        $(this).closest('td').find('.is-deleted').val('true');
        var row = $(this).closest('tr');
        row.hide();

        if ($(this).is('#cast-table')) {
            row.nextAll().each(function (index, row) {
                changeRowOrder($(row), -1);
            });
        }
    });

    //Reorder Cast Member
    $('.order-up, .order-down').click(function () {
        var row = $(this).closest('tr');
        var order = parseInt(row.find('input.order').val());
        var lastOrder = $('#crew-table tbody tr').length - 1;

        if ($(this).is('.order-up') && order !== 0) {
            var prevRow = $(row).prevAll('tr:visible:first');

            if (prevRow.length) {
                row.insertBefore(prevRow);
                changeRowOrder(row, -1);
                changeRowOrder(prevRow, 1);
            }
        }
        else if ($(this).is('.order-down') && order !== lastOrder) {
            var nextRow = $(row).nextAll('tr:visible:first');

            if (nextRow.length) {
                row.insertAfter(nextRow);
                changeRowOrder(row, 1);
                changeRowOrder(nextRow, -1);
            }
        }
    });
});

//Sort movie cards by attribute and direction
function sortMovies(movieSorter) {
    var attr = movieSorter.find('.dropdown-item.sort-movies.active').attr('id');
    var order = movieSorter.find('#sort-direction').attr('data-sort');

    var container = movieSorter.closest('div.movie-card-container');
    var cards = container.get(0).querySelectorAll('div.movie-card');
    tinysort(cards, { attr: attr, order: order });
}

function initializePersonPicker(container) {
    $(container + ' .person-picker')
        .selectpicker()
        .ajaxSelectPicker({
            ajax: {
                url: '../../Movie/GetPersonSelectData',
                data: {
                    query: '{{{q}}}'
                }
            }
        });
}

function initializeDepartmentPicker(container) {
    $(container + ' .department-picker')
        .selectpicker()
        .ajaxSelectPicker({
            ajax: {
                url: '../../Movie/GetDepartmentSelectData',
                data: {
                    query: '{{{q}}}'
                }
            }
        });
}

function changeRowOrder(row, change) {
    var orderInput = row.find('input.order');
    var order = parseInt(orderInput.val());
    orderInput.val(order + change);
}
