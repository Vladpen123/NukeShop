
$("#showAddCategoryModal").click(function () {
    $('#modal-add-category').modal('show');
})
$("#button").click(function (e) {
    e.preventsDefault();
    var model = $('#addCategoryForm').serialize();
    console.log(model);
    $.ajax({
        type: 'POST',
        url: 'admin/CategoryList',
        async: false,
        contentType: 'application/json',
        data: model,
        success: function (result) {
            $('#addPartial').html(result);
            $('#modal-add-category').modal('show');
        }
    });
})
