$(document).ready(function () {
    // Materialize
    $(function () {
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

    // Side menus 
    $('.open-notifications').click(function () {
        $('.notifications').animate({ 'width': 'toggle' });
    })

    $('.open-messages').click(function () {
        $('.messages').animate({ 'width': 'toggle' });
    })

});

