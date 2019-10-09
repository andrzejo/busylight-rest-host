(function () {
    var instance = null;

    var dummy = function () {

    };

    var Busylight = function (options) {
        var appendTo = options['appendTo'] || document.body;
        var transport = options['transport'] || 'display'; // or 'active' - mixed active/display content
        var host = options['host'] || 'http://localhost:5748';
        var hostVersion = 'n/a';
        var available = false;
        var transportElement;
        var self = this;
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
            window.addEventListener("message", receiveMessage, false);

            transportElement = document.createElement('iframe');
            transportElement.src = host + "/communication-frame.html";
            transportElement.style.display = 'none';
            transportElement.onload = function () {
                send('version');
                setTimeout(function () {
                    if (!available) {
                        communicationError();
                    }
                }, 4000);
            };
            transportElement.onerror = function () {
                available = false;
                communicationError();
            };
        }

        function parseImgData() {
        }

        function createImage() {
            transportElement = document.createElement('img');
            transportElement.style.display = 'none';
            transportElement.onload = function () {
                if (!available) {
                    callbacks.libReady(self);
                }
                available = true;
                parseImgData();
            };
            transportElement.onerror = function () {
                available = false;
                communicationError();
            };

            send('version');

            setTimeout(function () {
                if (!available) {
                    communicationError();
                }
            }, 4000);
        }

        function createTransportElement() {
            if (transport === 'active') {
                createIframe();
            } else {
                createImage();
            }
            appendTo.appendChild(transportElement);
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

        function createActionObject(action, params) {
            var parameters = packParameters(params || {});
            return {action: action, parameters: parameters};
        }

        function sendViaIframe(action, params) {
            if (transportElement && transportElement.contentWindow) {
                transportElement.contentWindow.postMessage(createActionObject(action, params), '*')
            } else {
                communicationError();
            }
        }

        function sendViaImg(action, params) {
            if (transportElement) {
                var url = host + '/passive_action'
                    + '?action=' + encodeURIComponent(JSON.stringify(createActionObject(action, params)))
                    + '&id=' + Math.random().toString(36).substr(2, 9);
                transportElement.src = url;
            } else {
                communicationError();
            }
        }

        function send(action, params) {
            if (transport === 'active') {
                sendViaIframe(action, params);
            } else {
                sendViaImg(action, params);
            }
        }

        function receiveMessage(message) {
            var response = message.data;

            if (message.origin !== host) {
                return;
            }

            if (response.success) {
                if (response.action.action === 'version') {
                    available = true;
                    hostVersion = response.response.version;
                    callbacks.libReady(self);
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
            send('color', {color: '#000000'})
        };

        createTransportElement();
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
        },
        soundVolume: {
            Mute: 'Mute',
            Low: 'Low',
            Middle: 'Middle',
            High: 'High',
            Max: 'Max'
        }
    }
})();
