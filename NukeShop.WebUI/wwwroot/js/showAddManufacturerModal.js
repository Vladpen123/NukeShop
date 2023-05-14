
$("#showAddManufacturerModal").click(function () {
    $('#modal-add-manufacturer').modal('show');
})
$("#button").click(function (e) {
    e.preventsDefault();
    var model = $('#addManufacturerForm').serialize();
    console.log(model);
    $.ajax({
        type: 'POST',
        url: 'admin/ManufacturerList',
        async: false,
        contentType: 'application/json',
        data: model,
        success: function (result) {
            $('#addPartial').html(result);
            $('#modal-add-manufacturer').modal('show');
        }
    });
})
