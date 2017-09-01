'use strict';

$(document).ready(function () {
    // Materialize
    $(function () {
        $('.button-collapse').sideNav();
        $('.parallax').parallax();
        $('.scrollspy').scrollSpy();
    });

    // Validation
    $('form').addTriggersToJqueryValidate().triggerElementValidationsOnFormValidation();

    $('input').elementValidAndInvalid(function (element) {
        $(element).removeClass('error').addClass('valid');
    }, function (element) {
        $(element).removeClass('valid').addClass('error');
    });

    //
});

