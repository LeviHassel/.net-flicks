$(function () {
    //Set up Bootstrap tooltips
    $('[data-toggle="tooltip"]').tooltip();

    //Add New Person in Edit Crew modal
    $(document).on("click", ".add-person", function () {
        var nextIndex = $("#people-table tbody tr").length;

        $.ajax({
            url: "../../Movie/AddPerson",
            data: { index: nextIndex },
            type: "GET"
        }).done(function (response) {
            $("#people-table tbody").append(response);

            //Refresh bootstrap-select so that it sets up the JS for new dropdowns
            $(".selectpicker").selectpicker();
        });
    });

    //Delete Person in Edit Crew modal
    $("#people-table").on("click", ".delete-person", function () {
        $(this).closest("td").find("input").val("true");
        $(this).closest("tr").hide();
    });
});