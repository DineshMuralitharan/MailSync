
var campignId = 0;
var typeId = 0;

$(document).ready(function () {
    typeId = $("#type-id").val();

    if ($(".label-danger").length != 0) {
        $("#Next-btn").attr("disabled", "disabled");
    }
});

$("#add-email-details").on("click", function () {
    var data = '{"emailSubject":"' + $("#emailSubject").val() + '","' + "name" + '":"' + $("#name").val() + '","' + "campaignId" + '":"' + campignId + '","' + "emailAddress" + '":"' + $("#emailAddress").val() + '"}';
    $.ajax({
        type: "POST",
        url: "/campaign/addemail",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            window.location.reload();
        }
    });
});

function AssignCampaignId(id) {
    campignId = id;
}

$("#create-schedule").on("click", function () {

    var trigger = document.getElementById('once').checked;
    var isOnce = false;
    var isEveryDay = false;

    if (trigger) {
        isOnce = true;
        isEveryDay = false;
    } else {
        isOnce = false;
        isEveryDay = true;
    }

    var data = '{"isOnce":"' + isOnce + '","' + "isEveryDay" + '":"' + isEveryDay + '","' + "campaignId" + '":"' + campignId + '","' + "date" + '":"' + $("#date-trigger").val() + '"}';
    $.ajax({
        type: "POST",
        url: "/campaign/addSchedule",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            window.location.reload();
        }
    });
});

$("#trigger-add").on("click", function () {

    var data = '{"campaignTypeId":"' + typeId + '"}';
    $.ajax({
        type: "POST",
        url: "/campaign/addTrigger",
        data: data,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            window.location.reload();
        }
    });
});
