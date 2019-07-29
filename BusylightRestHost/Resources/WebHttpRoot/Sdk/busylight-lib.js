(function () {
    var instance = null;

    var dummy = function () {

    };

    var Busylight = function (options) {
        var appendTo = options['appendTo'] || document.body;
        var host = options['host'] || 'http://localhost:5748';
        var hostVersion = null;
        var available = false;
        var frameElement = false;
        var callbacks = {
            error: dummy,
            response: dummy,
            libReady: dummy
        };

        function communicationError() {
            callbacks.error({
                success: false,
                message: 'Failed to communicate with host app.'
            });
        }

        function createIframe() {
            frameElement = document.createElement('iframe');
            frameElement.src = host + "/communication-frame.html";
            frameElement.style.display = 'none';
            frameElement.onload = function () {
                send('version');
                setTimeout(function () {
                    if (!available) {
                        communicationError();
                    }
                }, 4000);
            };
            frameElement.onerror = function () {
                available = false;
                communicationError();
            };
            appendTo.appendChild(frameElement);
        }

        function packParameters(params) {
            var result = [];
            for (var k in params) {
                if (params.hasOwnProperty(k)) {
                    result.push({Key: k, Value: params[k]});
                }
            }
            return result;
        }

        function send(action, params) {
            if (frameElement && frameElement.contentWindow) {
                var parameters = packParameters(params || {});
                frameElement.contentWindow.postMessage({action: action, parameters: parameters}, '*')
            }
        }

        function receiveMessage(message) {
            var response = message.data;
            if (response.success) {
                if (response.action.action === 'version') {
                    available = true;
                    hostVersion = response.response.version;
                    callbacks.libReady(this);
                }
                callbacks.response(response);
            } else {
                callbacks.error(message);
            }
        }

        this.isAvailable = function () {
            return available;
        };

        this.getHostAppVersion = function () {
            return hostVersion;
        };

        this.onResponse = function (handler) {
            callbacks.response = handler;
            return this;
        };

        this.onError = function (handler) {
            callbacks.error = handler;
            return this;
        };

        this.onLibReady = function (handler) {
            callbacks.libReady = handler;
            return this;
        };

        this.color = function (cssRgbColor) {
            send('color', {color: cssRgbColor})
        };

        this.alert = function (cssRgbColor, soundName, soundVolume) {
            send('alert', {color: cssRgbColor, sound: soundName, volume: soundVolume})
        };

        this.blink = function (cssRgbColor, onTime, offTime) {
            send('blink', {color: cssRgbColor, onTime: onTime, offTime: offTime})
        };

        this.off = function () {
            send('off')
        };

        window.addEventListener("message", receiveMessage, false);
        createIframe();
    };

    function setup(options) {
        if (instance === null) {
            instance = new Busylight(options || {});
        }
        return instance;
    }

    window.ThuliumBusylight = {
        setup: setup,
        sounds: {
            OpenOffice: 'OpenOffice',
            Quiet: 'Quiet',
            Funky: 'Funky',
            FairyTale: 'FairyTale',
            KuandoTrain: 'KuandoTrain',
            TelephoneNordic: 'TelephoneNordic',
            TelephoneOriginal: 'TelephoneOriginal',
            TelephonePickMeUp: 'TelephonePickMeUp',
            IM1: 'IM1',
            IM2: 'IM2'
        }
    }
})();


