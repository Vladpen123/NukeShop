
$("#showAddProductModal").click(function () {
    $('#modal-add-product').modal('show');
})
$("#button").click(function (e) {
    e.preventsDefault();
    var model = $('#addProductForm').serialize();
    console.log(model);
    $.ajax({
        type: 'POST',
        url: 'admin/ProductList',
        async: false,
        contentType: 'application/json',
        data: model,
        success: function (result) {
            $('#addPartial').html(result);
            $('#modal-add-product').modal('show');
        }
    });
})
