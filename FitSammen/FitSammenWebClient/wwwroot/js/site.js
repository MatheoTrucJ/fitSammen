// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ClickLeft() {
    
    const liste = document.querySelector('.hold-liste');
    liste.scrollBy({
        left: -270,
        behavior: 'smooth'
    });
}

function ClickRight() {
    const liste = document.querySelector('.hold-liste');
    liste.scrollBy({
        left: 270,
        behavior: 'smooth'
    });
}
