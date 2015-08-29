angular.module('ssqSignonExampleSSOSlaveApp', [ 'ui.bootstrap', 'angular-ssqsignon', 'ngResource', 'ssqSignonExampleConfig' ])
    .config(function(authenticatorProvider, $httpProvider, $locationProvider, SSQSIGNON_MODULE_NAME, SSQSIGNON_CLIENT_ID) {
        $locationProvider.html5Mode(true);
        authenticatorProvider.init(SSQSIGNON_MODULE_NAME, SSQSIGNON_CLIENT_ID, undefined, 'https://tinyusers.azurewebsites.net');
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

        var hamster = $resource('/hamster', undefined, { get: 'GET' });

        init();

        function init() {
            return authenticator.whoAmI()
                .catch(function(err) {
                    if (err == 'ask-user') {
                        return loginWithMaster();
                    } else {
                        return $q.reject(err);
                    }
                })
                .then(function(me) {
                    if (me != 'redirecting' || me != 'access-denied') {
                        $scope.userId = me.userId;
                        $scope.permissions = me.scope;
                        $scope.loggedIn = true;
                        getProtectedResources();
                    }
                });
        }

        function loginWithMaster() {
            if ($location.search().code && $location.search().state) {
                return $q(function (resolve, reject) {
                    $http.post('/swapcode', { code: $location.search().code, state: $location.search().state })
                        .success(function (access) {
                            $location.search('code', undefined);
                            $location.search('state', undefined);
                            resolve(authenticator.ssoSlave.useTokens(access));
                        })
                        .error(function (data, status) {
                            reject({ data: data, status: status });
                        });
                });
            } else if ($location.search().error) {
                $location.search('error', undefined);
                $scope.accessDenied = true;
                return $q.when('access-denied');
            } else {
                var uri = authenticator.ssoSlave.loginWithMaster('http://localhost:9901', 'hamster', 'xyz', 'http://localhost:9902');
                return $q.when('redirecting');
            }
        }

        function getProtectedResources() {
            hamster.get(function(hamster) {
                $scope.hamster = hamster.message;
            });
        }

    });