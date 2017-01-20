(function () {
    'use strict';
    angular.module('BeeModule').service('gameService', gameService);

    gameService.$inject = ['ajaxService'];

    function gameService(ajaxService) {

        this.getNewGame = function (successFunction, errorFunction) {
            var uri = "http://localhost:9001/api/game";
            ajaxService.AjaxGet(uri, successFunction, errorFunction);
        };

        this.postHitGame = function (gameId, successFunction, errorFunction) {
            var uri = "http://localhost:9001/api/game/" + gameId.toString();
            ajaxService.AjaxPost(null, uri, successFunction, errorFunction);
        };
    }
})();