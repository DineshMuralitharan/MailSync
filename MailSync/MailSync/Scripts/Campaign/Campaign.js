var typeId = "";

$(document).ready(function () {
    $('.selectpicker').selectpicker();
});

$("#new-subscribe").on("click", function () {

    $("#campaign-name").val("New Subscription");
    $(".campaign-section").addClass("hide");
    $("#new-subscribe-section").removeClass("hide");
    $(".modal-footer").removeClass("hide");

    $("#template-section").addClass("hide").fadeOut(1000);
    $("#list").removeClass("hide").fadeIn(1000);
    typeId = 5;
});

$("#birthday-wish").on("click", function () {

    $("#campaign-name").val("Birthday wishes");
    $(".campaign-section").addClass("hide");
    $("#birthday-wish-section").removeClass("hide");
    $(".modal-footer").removeClass("hide");

    $("#template-section").addClass("hide").fadeOut(1000);
    $("#list").removeClass("hide").fadeIn(1000);
    typeId = 6;
});

$("#blog-update").on("click", function () {

    $("#campaign-name").val("Blog updates");
    $(".campaign-section").addClass("hide");
    $("#blog-update-section").removeClass("hide");
    $(".modal-footer").removeClass("hide");

    $("#template-section").addClass("hide").fadeOut(1000);
    $("#list").removeClass("hide").fadeIn(1000);
    typeId = 1;
});

$("#abandoned-cart").on("click", function () {

    $("#campaign-name").val("Recover abandoned carts");
    $(".campaign-section").addClass("hide");
    $("#recover-cart-section").removeClass("hide");
    $(".modal-footer").removeClass("hide");

    $("#template-section").addClass("hide").fadeOut(1000);
    $("#list").removeClass("hide").fadeIn(1000);
    typeId = 2;
});

$("#release-notify").on("click", function () {

    $("#campaign-name").val("Release notifications");
    $(".campaign-section").addClass("hide");
    $("#release-automation-section").removeClass("hide");
    $(".modal-footer").removeClass("hide");

    $("#template-section").addClass("hide").fadeOut(1000);
    $("#list").removeClass("hide").fadeIn(1000);
    typeId = 3;
});

$("#renewal-notify").on("click", function () {

    $("#campaign-name").val("Renewal Automation");
    $(".campaign-section").addClass("hide");
    $("#renewal-automation-section").removeClass("hide");
    $(".modal-footer").removeClass("hide");

    $("#template-section").addClass("hide").fadeOut(1000);
    $("#list").removeClass("hide").fadeIn(1000);
    typeId = 4;
});

//$(document).on("click", function () {
//    CloseModel();
//});

$("#create-campaign").on("click", function () {
    var data = '{"campaignName":"' + $("#campaign-name").val() + '","' + "campaignType" + '":"' + typeId + '","' + "customerListId" + '":"' + $("#user-role").val() + '"}';
    $.ajax({
        type: "POST",
        url: "/campaign/add",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.result == true) {
                window.location.replace("/campaign/wizard/" + typeId);
            }
        }
    });
});

function CloseModel() {
    $("#campaign-name").val("");
    $(".campaign-section").addClass("hide");
    $(".modal-footer").addClass("hide");

    $("#template-section").removeClass("hide");
    $("#list").addClass("hide");

    $("#myModal").modal('hide');
}