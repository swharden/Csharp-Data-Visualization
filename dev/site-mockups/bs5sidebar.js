document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll('.sidebar .nav-link').forEach(function (element) {
        element.addEventListener('click', function (e) {
            let nextEl = element.nextElementSibling;
            let parentEl = element.parentElement;
            if (nextEl) {
                e.preventDefault();
                let collapsableEl = new bootstrap.Collapse(nextEl);
                if (nextEl.classList.contains('show')) {
                    collapsableEl.hide();
                } else {
                    collapsableEl.show();
                    var opened_submenu = parentEl.parentElement.querySelector('.submenu.show');
                    if (opened_submenu) {
                        new bootstrap.Collapse(opened_submenu);
                    }
                }
            }
        });
    })
});