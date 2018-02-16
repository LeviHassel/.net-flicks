$(function () {
    //Set up Tempus Dominus DateTime Picker: https://tempusdominus.github.io/bootstrap-4/
    $("#datetimepicker").datetimepicker({
        format: "L",
        viewMode: "years"
    });

    //Add New Person in Edit Crew modal
    $(document).on("click", ".add-person", function () {
        var nextIndex = $("#people-table tbody tr").length;

        $.ajax({
            url: "../../Movie/AddPerson",
            data: { index: nextIndex },
            type: "GET"
        }).done(function (response) {
            $("#people-table tbody").append(response);
        });
    });

    //Delete Person in Edit Crew modal
    $("#people-table").on("click", ".delete-person", function () {
        $(this).closest("td").find("input").val("true");
        $(this).closest("tr").hide();
        
    });
});