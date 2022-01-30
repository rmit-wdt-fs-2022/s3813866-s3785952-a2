// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function tabify (target) {
    // (A) GET HEADERS & CONTENTS
    let wrapper = document.getElementById(target),
        header = document.querySelector(`#${target} > ul`),
        headtabs = document.querySelectorAll(`#${target} > ul > li`),
        bodytabs = document.querySelectorAll(`#${target} > div`);

    // (B) ADD CSS + ONCLICK TOGGLE
    wrapper.classList.add("tabWrap");
    header.classList.add("tabHead");
    for (let i=0; i<headtabs.length; i++) {
        bodytabs[i].classList.add("tabBody");
        headtabs[i].onclick = () => {
            for (let j=0; j<headtabs.length; j++) {
                if (i==j) {
                    headtabs[j].classList.add("open");
                    bodytabs[j].classList.add("open");
                } else {
                    headtabs[j].classList.remove("open");
                    bodytabs[j].classList.remove("open");
                }
            }
        };
    }

    // (C) SET DEFAULT OPEN TAB IF NONE IS DEFINED
    if (wrapper.querySelector(".open") == null) {
        headtabs[0].classList.add("open");
        bodytabs[0].classList.add("open");
    }
}

// (D) ATTACH TABS
window.onload = () => {
    tabify("myTabs");
};