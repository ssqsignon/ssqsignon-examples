angular.module('ssqSignonExampleApp', [ 'ui.bootstrap', 'angular-ssqsignon', 'ngResource' ])
    .config(function(authenticatorProvider, $httpProvider, $locationProvider) {
        $locationProvider.html5Mode(true);

        authenticatorProvider.proxy('/auth');
        $httpProvider.interceptors.push('appendAccessToken');
        $httpProvider.interceptors.push('refreshAccessToken');
    })
    .controller('landingCtrl', function($scope, $q, $resource, $modal, $location, authenticator) {

        $scope.logout = function() {
            $scope.cat = null;
            $scope.dog = null;
            $scope.loggedIn = false;
            authenticator.forgetMe()
                .then(init);
        };

        var cat = $resource('/cat', undefined, { get: 'GET' }),
            dog = $resource('/dog', undefined, { get: 'GET' });

        init();

        function init() {
            return authenticator.whoAmI()
                .catch(function(err) {
                    return err == 'ask-user' ? login() : $q.reject(err);
                })
                .then(function(me) {
                    if ($location.search().redirect_uri) {
                        ssoRedirect();
                    } else {
                        $scope.userId = me.userId;
                        $scope.permissions = me.scope;
                        $scope.loggedIn = true;
                        getProtectedResources();
                    }
                });
        }

        function login() {
            return $modal.open({
                size: 'sm',
                templateUrl: 'loginModal.html',
                controller: function($scope, $modalInstance) {
                    $scope.login = function() {
                        $scope.working = true;
                        authenticator.login($scope.username, $scope.password)
                            .then(function(access) {
                                $scope.working = false;
                                $modalInstance.close(access);
                            }, function() {
                                $scope.working = false;
                                $scope.loginFailed = true;
                            });
                    }
                }
            }).result;
        }

        function askAboutRedirect() {
            return $modal.open({
                size: 'sm',
                templateUrl: 'redirectModal.html',
                backdrop: 'static',
                controller: function($scope, $modalInstance, $location) {
                    $scope.redirectUri = $location.search().redirect_uri;
                    $scope.scope = $location.search().scope;
                    $scope.allow = function() {
                        $modalInstance.close(false);
                    };
                    $scope.deny = function() {
                        $modalInstance.close(true);
                    }
                }
            }).result;
        }

        function ssoRedirect() {
            askAboutRedirect()
                .then(function(denyAccess) {
                    return authenticator.ssoMaster.safeRedirect(denyAccess);
                });
        }

        function getProtectedResources() {
            cat.get(function(cat) {
                $scope.cat = cat.message;
            });
            dog.get(function(dog) {
                $scope.dog = dog.message;
            });
        }

    });