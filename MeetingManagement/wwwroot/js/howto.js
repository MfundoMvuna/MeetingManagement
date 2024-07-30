document.addEventListener("DOMContentLoaded", function () {
    // Smooth scrolling
    const links = document.querySelectorAll('nav ul li a');
    for (const link of links) {
        link.addEventListener('click', smoothScroll);
    }

    function smoothScroll(event) {
        event.preventDefault();
        const targetId = event.currentTarget.getAttribute("href").substring(1);
        const targetSection = document.getElementById(targetId);

        window.scrollTo({
            top: targetSection.offsetTop,
            behavior: "smooth"
        });
    }

    // Lazy loading sections
    const sections = document.querySelectorAll('.content-section');
    const options = {
        threshold: 0.1
    };

    const observer = new IntersectionObserver(handleIntersect, options);
    sections.forEach(section => observer.observe(section));

    function handleIntersect(entries, observer) {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('active');
                observer.unobserve(entry.target);
            }
        });
    }
});