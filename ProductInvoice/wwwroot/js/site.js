// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


AddProductFunc = (url,title)=> {
    $.ajax({
        url: url,
        type: "GET",
        success: function (res) {
            $('#form-modal .modal-body').html(res);
            $('#form-modal .modal-title').html(title);
            $('#form-modal').modal('show');
        }
    });
}

ajaxPost = form => {
    
    $.ajax({
        type: "POST",
        url: form.action,
        data: new FormData(form),
        contentType: false,
        processData: false,
        success: function (res) {
            if (res.isValid) {
                $('#id').html(res.html);
                $('#form-modal .modal-body').html('');
                $('#form-modal .modal-title').html('');
                $('#form-modal').modal('hide');
            }
            else {
                return false;
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
    return false;
}