var typeId = "";

$(document).ready(function () {
    $('.selectpicker').selectpicker(); 
});

 

$("#create-group").on("click", function () {
    var data = '{"groupName":"' + $("#group-name").val()+'"}';
    $.ajax({
        type: "POST",
        url: "/group/add",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            window.location.replace("group");
        }
    });
});

function CloseModel() {
    $("#group-name").val(""); 
    $(".modal-footer").addClass("hide");

    $("#template-section").removeClass("hide");
    $("#list").addClass("hide");

    $("#myModal").modal('hide');
}