/// <reference path="../lib/jquery/jquery.js" />
$(document).ready(function () {
    var inputs = $("input[type=email], input:password, input[type=text]");
    for (var index = 0; index < inputs.length; index++) {
        var input = inputs[index];
        if (input.value.length === 0) {
            input.focus();
            break;
        }
    }
});