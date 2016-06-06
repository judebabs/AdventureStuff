$(function () {
    var chat = $.connection.chatHub;

    $('#chat-message').focus();

    $.connection.hub.start().done() = function () {
        $('#sendMessage').on('click', function () {
            chat.server.send('TestName', $('#chat-message').val());
            $('#chat-messge').val('').focus();
        });
    };

    chat.client.addMessage = function (name, message) {
        var encodedName = $("<div>" + name
            + "</div>");
        var encodedMessage = $("<div>" + message
            + "</div>");

        var listItem = $("<li>" + encodedName + " : " + encodedMessage + "</li>");

        $('#discussion').append(listItem);
    };

})
