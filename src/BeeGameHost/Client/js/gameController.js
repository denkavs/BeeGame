(function () {
    'use strict';
    angular.module('BeeModule')
        .controller('GameController', GameController);

    GameController.$inject = ['$scope', '$rootScope', '$http', '$q', '$window', '$location', 'gameService'];
    function GameController($scope, $rootScope, $http, $q, $window, $location, gameService) {
        var vm = this;

        vm.GameId = 0;
        vm.gameState = gameStateInText(0);
        vm.bees = [];
        vm.gameCompleted = false;
        var hittedBee = null;

        vm.Hit = Hit;
        vm.GetNameBeeById = GetNameBeeById;
        vm.IsCurrentHitted = IsCurrentHitted;

        function Hit() {

            if (vm.gameCompleted) {
                // start new game
                vm.GameId = 0;
                vm.gameCompleted = false;
                vm.bees = [];
                vm.gameState = gameStateInText(0);
            }
            else {

                if (vm.GameId === 0) {
                    gameService.getNewGame(function (response, status) {
                        if (status === 200) {
                            InitFields(response);
                        }
                    }, function (response) {
                    });
                }
                else {
                    gameService.postHitGame(vm.GameId, function (response, status) {

                        if (status === 200) {
                            InitFields(response);
                        }
                    }, function (response) {

                    });
                }
            }
        };

        function InitFields(response) {
            vm.gameCompleted = isGameFinished(response.State);
            vm.gameState = gameStateInText(response.State);
            vm.GameId = response.GameId;
            vm.bees = response.Bees;
            hittedBee = response.SelectedBee;
        }

        function gameStateInText(state) {
            var res = "Unknown";

            switch (state) {
                case 0:
                    res = "Init";
                    break;
                case 1:
                    res = "Started";
                    break;
                case 2:
                    res = "InProgress";
                    break;
                case 3:
                    res = "Finished";
                    break;
                case 4:
                    res = "Failed";
                    break;
            }
            return res;
        };

        function GetNameBeeById(id) {
            var res = "Unknown";

            switch (id) {
                case 0:
                    res = "Unknown";
                    break;
                case 1:
                    res = "Queen";
                    break;
                case 2:
                    res = "Worker";
                    break;
                case 3:
                    res = "Drone";
                    break;
            }
            return res;
        }

        function isGameFinished(state) {
            var res = false;
            if (state === 3)
                res = true;
            return res;
        };

        function IsCurrentHitted(id) {
            var res = false;
            if (hittedBee !== null) {
                if (hittedBee.Id === id)
                    res = true;
            }

            return res;
        }
    }
})();