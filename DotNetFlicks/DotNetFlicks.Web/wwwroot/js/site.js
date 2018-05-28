$(function () {
    //Initialize Bootstrap tooltips on page load
    $('[data-toggle="tooltip"]').tooltip();

    //Initialize Bootstrap tooltips on DataTable draw
    $('.dataTable').on('draw.dt', function () {
        $('[data-toggle="tooltip"]').tooltip();
    });

    //Update image modals
    $('.image-modal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);

        var title = button.data('title');
        var imageSource = button.data('img-src');

        var modal = $(this);
        modal.find('.modal-title').text(title);
        modal.find('img').attr('src', imageSource);
    });

    //Initialize DataTable for Movies, Genres and Departments
    $('.data-table').DataTable({
        stateSave: true,
        fixedHeader: {
            headerOffset: $('.navbar').outerHeight()
        }
    });

    //Initialize People DataTable (this one uses AJAX to increase page loading speed)
    $('.people-data-table').DataTable({
        stateSave: true,
        fixedHeader: {
            headerOffset: $('.navbar').outerHeight()
        },
        serverSide: true,
        autoWidth: false,

        ajax: {
            url: '/Person/LoadData',
            type: 'POST'
        },

        rowId: 'Id',

        columns: [
            {
                className: 'd-table-cell align-middle',
                data: 'Name',
                render: function (data, type, full, meta) {
                    return '<a href="Person/View/' + full.Id + '" class="custom-link">' + full.Name + '</a >';
                }
            },
            {
                className: 'd-none d-md-table-cell align-middle',
                orderable: false,
                render: function (data, type, full, meta) {
                    if (full.ImageUrl) {
                        return '<button type="button" class="btn btn-secondary" data-toggle="modal" data-target=".image-modal" data-title="' + full.Name + '" data-img-src="' + full.ImageUrl + '"><i class="fas fa-image"></i></button>';
                    } else {
                        return '';
                    }
                }
            },
            {
                className: 'd-none d-md-table-cell',
                orderable: true,
                data: 'Roles.length',
                render: function (data, type, full, meta) {
                    return '<button type="button" class="btn btn-secondary" data-toggle="tooltip" data-html="true" title="' + full.RolesTooltip + '">' + full.Roles.length + '</button >';
                }
            },
            {
                orderable: false,
                render: function (data, type, full, meta) {
                    return '<div class="d-flex"><a class="btn btn-primary ml-2" href="Person/Edit/' + full.Id + '"><i class="fas fa-edit"></i></a><a class="btn btn-primary ml-2" href="Person/Delete/' + full.Id + '"><i class="fas fa-trash"></i></a></div >';
                }
            }
        ]
    });

    //Sort movie cards by type
    $(document).on('click', '.sort-movies', function () {
        $('.dropdown-item.sort-movies.active').removeClass('active text-white');
        $(this).addClass('active text-white');
        $('#sort-movies-dropdown').text(this.text);
        sortMovies();
    });

    //Toggle ascending/descending sort for movie cards
    $('#sort-direction').on('click', function () {
        $(this).attr('data-sort', $(this).attr('data-sort') === 'desc' ? 'asc' : 'desc');
        $(this).find('[data-fa-i2svg]').toggleClass('fa-arrow-up').toggleClass('fa-arrow-down');
        sortMovies();
    });

    //Toggled open/closed icon on collapsible cards
    $('.collapse-card').on('click', function () {
        $(this)
            .find('[data-fa-i2svg]')
            .toggleClass('fa-chevron-up')
            .toggleClass('fa-chevron-down');
    });

    //Initialize AJAX Bootstrap Select lists on Cast/Crew modal open for faster load times
    $('#cast-modal, #crew-modal').on('show.bs.modal', function () {
        initializePersonPicker('#' + $(this).attr('id'));

        if ($(this).is('#crew-modal')) {
            initializeDepartmentPicker();
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
        else if (order !== lastOrder) {
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
function sortMovies() {
    var attr = $('.dropdown-item.sort-movies.active').attr('id');
    var order = $('#sort-direction').attr('data-sort');
    tinysort('div.movie-column', { attr: attr, order: order });
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