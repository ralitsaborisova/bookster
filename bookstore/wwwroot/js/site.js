// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#explore').click(function () {
        window.location.href = '/Browse'; // This is the URL of the Books Razor page
    });
});
