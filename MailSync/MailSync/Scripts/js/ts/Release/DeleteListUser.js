


function deleteconfirmationdialog(teamId, customerId , customerName) {
    swal({
        title: "Are you sure?",
        text: "Do you want to delete the " + customerName + " from the list?",
        type: "warning",
        showCancelButton: true,
        confirmButtonClass: "btn-danger",
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        closeOnConfirm: false,
        closeOnCancel: true
    },
        function (isConfirm) {
            if (isConfirm) {
                deleteTeam(teamId, customerId, customerName);
            }
        });
}


function deleteTeam(teamId, userID, customerName) {
    $(".sweet-alert").hide();
    $.ajax({
        type: 'POST',
        url: "/lists/userdelete/"+teamId +"/" + userID,
        dataType: 'json',
        error: function () {
            swal("Error", "An error occurred while processing. Please try again later.", "error");
        },
        success: function (status) {
            if (status.data === true) {
                swal({
                    title: "Success",
                    text: "The " + customerName + " has been deleted successfully.",
                    type: "success",
                    showCancelButton: false,
                    confirmButtonClass: "btn-default",
                    confirmButtonText: "Ok",
                    cancelButtonText: "No",
                    closeOnConfirm: true,
                    closeOnCancel: false
                },
                    function (isConfirm) {
                        $(".sweet-alert").hide();
                        refresh();
                    });
            } else {
                swal("Error", "An error occurred while processing. Please try again later.", "error");
            }
        },
        complete: function () { 
        }
    });
}
