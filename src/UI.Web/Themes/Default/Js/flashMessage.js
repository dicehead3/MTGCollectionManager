(function ($, window) {

    "use strict";

    var defaults = {
        selectors: {
            flashMessageContainer: '#flashMessages',
        }
    };

    var addFlashMessage = function (title, message, type, options) {

        options = $.extend(defaults, options);

        // Default to type 'info' if type is not specified or valid
        if ($.inArray(type, ['success', 'info', 'warning', 'error']) == -1) {
            type = 'info';
        }

        var messageTemplate =   '<div class="alert alert-' + type + ' alert-block" onclick="$(this).fadeOut()">' +
                                    '<h4>' + title + '</h4>' +
                                    message +
                                '</div>';

        $(messageTemplate).appendTo(options.selectors.flashMessageContainer);
    };

    window.AddFlashMessage = addFlashMessage;

})($, window);