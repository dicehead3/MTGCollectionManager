(function ($, window) {

    "use strict";

    var defaults = {
        selectors: {
            AssignButton: '#UserIndex',
        },
        urls: {
            GetUsers: null
        }
    };

    var userIndexController = function (options) {

        var self = this;
        this.options = $.extend(defaults, options);
    };

    window.UserIndexController = userIndexController;

})($, window);