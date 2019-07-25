(function () {
    var endpointUrl = window.location.protocol + '//' + window.location.host + '/action';

    var post = function (url, data, success, error) {
        var request = new XMLHttpRequest();
        request.overrideMimeType("application/json");

        function sendError(message) {
            error(message, request.responseText, request.status);
        }

        request.onload = function () {
            var response = request.responseText;
            if (request.status >= 200 && request.status <= 299) {
                try {
                    response = response ? JSON.parse(response) : {};
                } catch (e) {
                    sendError('Failed to parse response');
                    return;
                }
                success && success(response, request.status);
            } else {
                sendError('Server respond with HTTP code ' + request.status);
            }
        };

        request.onerror = function (response) {
            error && sendError(response);
        };

        request.open("post", url, true);
        request.send(JSON.stringify(data));
    };

    function sendMessageToParent(message) {
        if (window && window.parent && window.parent.postMessage) {
            window.parent.postMessage(message, '*'); //todo: origin?
        }
    }

    function sendAction(action) {
        function toResponseObject(success, message, response, httpCode) {
            return {
                action: action,
                success: success,
                message: message,
                response: response,
                httpCode: httpCode
            };
        }

        function error(message, response, httpCode) {
            sendMessageToParent(toResponseObject(false, message, response, httpCode));
        }

        function success(response, httpCode) {
            sendMessageToParent(toResponseObject(true, null, response, httpCode));
        }

        post(endpointUrl, action, success, error);
    }

    function receiveMessage(event) {
        sendAction(event.data);
    }

    window.addEventListener("message", receiveMessage, false);
})();