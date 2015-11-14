
$(document).ready(function() {

    var authenticator = $.authenticator($.appConfig.moduleName, $.appConfig.clientId)
            .autoAppendAccessToken()
            .autoRefreshAccessToken(),
        loginPromise = null,
        user = null;

    setupUI();

    init();

    function init() {
        authenticator.whoAmI()
            .then(null, function(err) {
                return err == 'ask-user' ? showLogin() : err;
            })
            .then(function(me) {
                if (window.location.search.search(/redirect_uri=.*/) != -1) {
                    ssoRedirect();
                } else {
                    user = me;
                    $('#user-id').text(user.userId);
                    $('#welcome-text').show();
                    getProtectedResources();
                }
            });
    }

    function setupUI() {
        $('#login-button').click(function() {
            $('#login-spinner').show();
            authenticator.login($('#username').val(), $('#password').val())
                .then(function(me) {
                    loginPromise.resolve(me);
                    $('#login-modal').modal('hide');
                }, function() {
                    $('#login-failed').show();
                })
                .always(function() {
                    $('#login-spinner').hide();
                });
        });

        $('#login-failed-close').click(function() {
            $('#login-failed').hide();
        });

        $('#logout-button').click(function() {
            authenticator.forgetMe()
                .then(function() {
                    $('#cat-message').text('');
                    $('#cat').hide();
                    $('#dog-message').text('');
                    $('#dog').hide();
                    $('#welcome-text').hide();
                    init();
                });
        });

        $('#allow-redirect-button').click(function() {
            authenticator.ssoMaster.safeRedirect();
        });

        $('#deny-redirect-button').click(function() {
            authenticator.ssoMaster.safeRedirect(true);
        });
    }

    function showLogin() {
        loginPromise = $.Deferred();
        $('#login-modal').modal({ backdrop: 'static', keyboard: false });
        return loginPromise;
    }

    function ssoRedirect() {
        $('#redirect-uri').text(queryString('redirect_uri'));
        $('#redirect-scope').text(queryString('scope'));
        $('#redirect-modal').modal({ backdrop: 'static', keyboard: false });
    }

    function getProtectedResources() {
        $.get('/cat')
            .done(function(cat) {
                $('#cat-message').text(cat.message);
                $('#cat').show();
            });
        $.get('/dog')
            .done(function(dog) {
                $('#dog-message').text(dog.message);
                $('#dog').show();
            });
    }

    function queryString(key) {
        key = key.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + key + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    }
});
