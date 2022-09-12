// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function somenteNumeros(e) {
    var charCode = e.charCode ? e.charCode : e.keyCode;
    if (charCode != 8 && charCode != 9 && charCode != 13) {
        if ((charCode < 48 || charCode > 57)) {
            return false;
        }
    }
}

var elemento = document.getElementById("saudacao");
var saudacao = "", d = new Date().getHours();

if (d <= 12) {
    saudacao = "Bom dia";
}
else if (d <= 18) {
    saudacao = "Boa tarde";
}
else {
    saudacao = "Boa noite";
}
elemento.innerHTML = saudacao + ", seja bem-vindo!";