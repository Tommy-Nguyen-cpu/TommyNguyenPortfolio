// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function createAlert(message) {
    $('html,body').animate({ scrollTop: '0px' });
    let alert = `<div class="alert alert-danger alert-dismissable fade show text-center">
${message }
<button type="button" class="close" data-dismiss="alert" aria-label="Close">
        <span aria-hidden="true">&times;</span>
    </button>`;
    $('#alert-box').append($(alert).hide().delay(100).slideDown(700));
}