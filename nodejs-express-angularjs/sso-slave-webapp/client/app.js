angular.module('ssqSignonExampleSSOSlaveApp', [ 'ui.bootstrap', 'angular-ssqsignon', 'ngResource' ])
    .config(function(authenticatorProvider, $httpProvider, $locationProvider) {
        $locationProvider.html5Mode(true);
        authenticatorProvider.proxy('/auth');
        $httpProvider.interceptors.push('appendAccessToken');
        $httpProvider.interceptors.push('refreshAccessToken');
    })
    .controller('landingCtrl', function($scope, $q, $resource, $http, $modal, $location, authenticator) {

        $scope.logout = function() {
            $scope.hamster = null;
            $scope.loggedIn = false;
            authenticator.forgetMe()
                .then(init);
        };

        $scope.startSSO = function() {
            window.location.assign('/loginwithmaster?state=somevalue');
        };

        var hamster = $resource('/hamster', undefined, { get: 'GET' });

        init();

        function init() {
            return (safeRedirected() ? finishSSO() : authenticator.whoAmI())
                .then(function(me) {
                    if (me != 'access-denied') {
                        $scope.userId = me.userId;
                        $scope.permissions = me.scope;
                        $scope.loggedIn = true;
                        getProtectedResources();
                    }
                }, function() {
                    $scope.loggedIn = false;
                });
        }

        function safeRedirected() {
            return (($location.search().code && $location.search().state) || $location.search().error);
        }

        function finishSSO() {
            if ($location.search().code && $location.search().state) {
                return authenticator.ssoSlave.consumeAuthorizationCode($location.search().code, 'http://localhost:9902')
                    .then(function(me) {
                        $location.search('code', undefined);
                        $location.search('state', undefined);
                        return me;
                    });
            } else if ($location.search().error) {
                $location.search('error', undefined);
                $scope.accessDenied = true;
                return $q.when('access-denied');
            }
        }

        function getProtectedResources() {
            hamster.get(function(hamster) {
                $scope.hamster = hamster.message;
            });
        }

    });