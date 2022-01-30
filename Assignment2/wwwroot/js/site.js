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

// LOGIN 

$(function() {

    $("input[type='password'][data-eye]").each(function(i) {
        var $this = $(this),
            id = 'eye-password-' + i,
            el = $('#' + id);

        $this.wrap($("<div/>", {
            style: 'position:relative',
            id: id
        }));

        $this.css({
            paddingRight: 140
        });
        $this.after($("<div/>", {
            html: 'Show',
            class: 'btn btn-primary btn-sm',
            id: 'passeye-toggle-'+i,
        }).css({
            position: 'absolute',
            right: 10,
            top: ($this.outerHeight() / 2) - 12,
            padding: '2px 7px',
            fontSize: 12,
            cursor: 'pointer',
        }));

        $this.after($("<input/>", {
            type: 'hidden',
            id: 'passeye-' + i
        }));

        var invalid_feedback = $this.parent().parent().find('.invalid-feedback');

        if(invalid_feedback.length) {
            $this.after(invalid_feedback.clone());
        }

        $this.on("keyup paste", function() {
            $("#passeye-"+i).val($(this).val());
        });
        $("#passeye-toggle-"+i).on("click", function() {
            if($this.hasClass("show")) {
                $this.attr('type', 'password');
                $this.removeClass("show");
                $(this).removeClass("btn-outline-primary");
            }else{
                $this.attr('type', 'text');
                $this.val($("#passeye-"+i).val());
                $this.addClass("show");
                $(this).addClass("btn-outline-primary");
            }
        });
    });

    $(".my-login-validation").submit(function() {
        var form = $(this);
        if (form[0].checkValidity() === false) {
            event.preventDefault();
            event.stopPropagation();
        }
        form.addClass('was-validated');
    });
});