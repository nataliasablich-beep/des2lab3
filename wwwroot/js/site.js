// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    if (typeof $.validator !== "undefined") {
        $.extend($.validator.messages, {
            required: "Este campo es obligatorio.",
            email: "Ingrese un correo electrónico válido.",
            number: "Ingrese solo números.",
            digits: "Ingrese solo números.",
            range: $.validator.format("Ingrese un valor entre {0} y {1}."),
            minlength: $.validator.format("Ingrese al menos {0} caracteres."),
            maxlength: $.validator.format("Ingrese como máximo {0} caracteres."),
            equalto: "Los valores no coinciden."
        });
    }
});
