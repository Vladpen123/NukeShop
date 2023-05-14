    $("#showEditProductModal").click(function () {
        $('#modal-edit-product').modal('show');
        })
    $("#button").click(function (e) {
        e.preventsDefault();
        var model = $('#addEditForm').serialize();
    console.log(model);
    $.ajax({
        type: 'POST',
    url: 'admin/ProductList',
    async: false,
    contentType: 'application/json',
    data: model,
    success: function (result) {
        $('#editPartial').html(result);
    $('#modal-edit-product').modal('show');
                }
            });
        })