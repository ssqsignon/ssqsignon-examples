
$(document).ready(function() {

    var authenticator = $.authenticator.proxy('/auth')
            .autoAppendAccessToken()
            .autoRefreshAccessToken(),
        user = null;

    setupUI();

    init();

    function init() {
        (safeRedirected() ? finishSSO() : authenticator.whoAmI())
            .then(function(me) {
                if (me != 'access-denied') {
                    user = me;
                    $('#user-id').text(user.userId);
                    $('#welcome-text').show();
                    $('#login-button').hide();
                    getProtectedResources();
                }
            });
    }

    function setupUI() {
        $('#login-button').click(function() {
            window.location.assign('/loginwithmaster?state=somevalue');
        });

        $('#logout-button').click(function() {
            authenticator.forgetMe()
                .then(function() {
                    $('#hamster-message').text('');
                    $('#hamster').hide();
                    $('#welcome-text').hide();
                    $('#access-denied').hide();
                    $('#login-button').show();
                });
        });
    }

    function safeRedirected() {
        return ((queryString('code') && queryString('state')) || queryString('error'));
    }

    function finishSSO() {
        if (queryString('code') && queryString('state')) {
            return authenticator.ssoSlave.consumeAuthorizationCode(queryString('code'), 'http://localhost:9902')
                .then(function(me) {
                    clearQueryString();
                    return me;
                });
        } else if (queryString('error')) {
            clearQueryString();
            $('#access-denied').show();
            return $.when('access-denied');
        }
    }

    function getProtectedResources() {
        $.get('/hamster')
            .done(function(hamster) {
                $('#hamster-message').text(hamster.message);
                $('#hamster').show();
            });
    }

    function queryString(key) {
        key = key.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + key + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }

    function clearQueryString() {
        location.search = '';
    }
});
