document.addEventListener('DOMContentLoaded', function() {
    let header = document.querySelector('.header-main-dynamic');
    window.addEventListener('mousemove', function(e) {
        if (e.clientY < 100 && window.scrollY  > 100) {
            header.classList.add('active');
        } else {
            header.classList.remove('active');
        }
    });
    window.addEventListener('scroll', function(e) {
        if (window.scrollY < 100) {
            header.classList.remove('active');
        }
    })
});