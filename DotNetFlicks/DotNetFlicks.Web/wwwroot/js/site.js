$(function () {

    //Initialize DataTables
    $('.data-table').DataTable({
        "deferRender": true
    });

    $(".people-data-table").DataTable({
        serverSide: true,

        ajax: {
            url: "/Person/LoadData",
            type: "POST"
        },

        rowId: 'Id',

        columns: [
            {
                className: "d-table-cell align-middle",
                autoWidth: true,
                data: "Name",
                render: function (data, type, full, meta) {
                    return '<a href="Person/View/' + full.Id + '" style="color: black">' + full.Name + '</a >';
                }
            },
            {
                className: "d-none d-md-table-cell align-middle",
                autoWidth: true,
                orderable: false,
                render: function (data, type, full, meta) {
                    return '<button type="button" class="btn btn-secondary" data-toggle="modal" data-target=".image-modal" data-title="' + full.Name + '" data-img-src="' + full.ImageUrl + '"><i class="fas fa-image"></i></button>';
                }
            },
            {
                className: "d-none d-md-table-cell",
                autoWidth: true,
                orderable: false,
                render: function (data, type, full, meta) {
                    return '<button type="button" class="btn btn-secondary" data-toggle="modal" data-target=".image-modal" data-title="' + full.Name + '" data-img-src="' + full.ImageUrl + '"><i class="fas fa-image"></i></button>';
                },
            },
            {
                orderable: false,
                render: function (data, type, full, meta) {
                    return '<div class="d-flex"><a class="btn btn-primary ml-2" href="Person/Edit/' + full.Id + '"><i class="fas fa-edit"></i></a><a class="btn btn-primary ml-2" href="Person/Delete/' + full.Id + '"><i class="fas fa-trash"></i></a></div >';
                }
            }
        ]
    });

    //Set up Bootstrap tooltips
    $('[data-toggle="tooltip"]').tooltip();

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
            $('.selectpicker').selectpicker();
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
            $('.selectpicker').selectpicker();
        });
    });

    //Delete Person in Edit Cast and Edit Crew modals
    $('.people-table').on('click', '.delete-person', function () {
        $(this).closest('td').find('input').val('true');
        $(this).closest('tr').hide();
    });
});

//Sort movie cards by attribute and direction
function sortMovies() {
    var attr = $('.dropdown-item.sort-movies.active').attr('id');
    var order = $('#sort-direction').attr('data-sort');
    tinysort('div.movie-column', { attr: attr, order: order });
}