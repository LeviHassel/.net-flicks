﻿$(function () {
    //Initialize AJAX Bootstrap Select lists for faster load times
    initializePersonPicker();
    initializeDepartmentPicker();

    //Initialize DataTable for Movies, Genres and Departments
    $('.data-table').DataTable({
        stateSave: true,
        fixedHeader: {
            headerOffset: $(".navbar").outerHeight()
        }
    });

    //Initialize People DataTable (this one uses AJAX to increase page loading speed)
    $(".people-data-table").DataTable({
        stateSave: true,
        fixedHeader: {
            headerOffset: $(".navbar").outerHeight()
        },
        serverSide: true,
        autoWidth: false,

        ajax: {
            url: "/Person/LoadData",
            type: "POST"
        },

        rowId: 'Id',

        columns: [
            {
                className: "d-table-cell align-middle",
                data: "Name",
                render: function (data, type, full, meta) {
                    return '<a href="Person/View/' + full.Id + '" class="custom-link">' + full.Name + '</a >';
                }
            },
            {
                className: "d-none d-md-table-cell align-middle",
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
                className: "d-none d-md-table-cell",
                orderable: true,
                data: "Roles.length",
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

    //Initialize Bootstrap tooltips on page load
    $('[data-toggle="tooltip"]').tooltip();

    //Initialize Bootstrap tooltips on DataTable draw
    $('.dataTable').on('draw.dt', function () {
        $('[data-toggle="tooltip"]').tooltip();
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

    //Update image modals
    $('.image-modal').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);

        var title = button.data('title');
        var imageSource = button.data('img-src');

        var modal = $(this);
        modal.find('.modal-title').text(title);
        modal.find('img').attr('src', imageSource);
    });

    //Add New Cast Member in Edit Crew modal
    $(document).on('click', '.add-cast-member', function () {
        var nextIndex = $('#cast-table tbody tr').length;

        $.ajax({
            url: '../../Movie/AddCastMember',
            data: { index: nextIndex },
            type: 'GET'
        }).done(function (response) {
            $('#cast-table tbody').append(response);

            //Refresh bootstrap-select so that it sets up the JS for new dropdowns
            initializePersonPicker();
        });
    });

    //Add New Crew Member in Edit Crew modal
    $(document).on('click', '.add-crew-member', function () {
        var nextIndex = $('#crew-table tbody tr').length;

        $.ajax({
            url: '../../Movie/AddCrewMember',
            data: { index: nextIndex },
            type: 'GET'
        }).done(function (response) {
            $('#crew-table tbody').append(response);

            //Refresh bootstrap-select so that it sets up the JS for new dropdowns
            initializePersonPicker();
            initializeDepartmentPicker();
        });
    });

    //Delete Person in Edit Cast and Edit Crew modals
    $('.people-table').on('click', '.delete-person', function () {
        $(this).closest('td').find('.is-deleted').val('true');
        $(this).closest('tr').hide();
    });

    //Move Cast Members up and down in Edit Cast modal
    $(".order-up, .order-down").click(function () {
        var row = $(this).closest("tr");
        var order = parseInt(row.find("input.order").val());
        var lastOrder = $('#crew-table tbody tr').length - 1;

        if ($(this).is(".order-up") && order !== 0) {
            changeRowOrder(row, -1);
            changeRowOrder(row.prev(), 1);
            row.insertBefore(row.prev());
        }
        else if (order !== lastOrder) {
            changeRowOrder(row, 1);
            changeRowOrder(row.next(), -1);
            row.insertAfter(row.next());
        }
    });
});

//Sort movie cards by attribute and direction
function sortMovies() {
    var attr = $('.dropdown-item.sort-movies.active').attr('id');
    var order = $('#sort-direction').attr('data-sort');
    tinysort('div.movie-column', { attr: attr, order: order });
}

function initializeDepartmentPicker() {
    $('.department-picker')
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

function initializePersonPicker() {
    $('.person-picker')
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

function changeRowOrder(row, change) {
    var orderInput = row.find("input.order");
    var order = parseInt(orderInput.val());
    orderInput.val(order + change);
}