$("#showEditCategoryModal").click(function () {
    $('#modal-edit-category').modal('show');
})
$("#button").click(function (e) {
    e.preventsDefault();
    var model = $('#addEditForm').serialize();
    console.log(model);
    $.ajax({
        type: 'POST',
        url: 'admin/CategoryList',
        async: false,
        contentType: 'application/json',
        data: model,
        success: function (result) {
            $('#editPartial').html(result);
            $('#modal-edit-category').modal('show');
        }
    });
})