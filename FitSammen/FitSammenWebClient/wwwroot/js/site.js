
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




