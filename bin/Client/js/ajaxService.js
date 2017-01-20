(function () {
    'use strict';
    angular.module('BeeModule').service('ajaxService', ajaxService);

    ajaxService.$inject = ['$http'];

    function ajaxService($http) {

        this.AjaxPost = function (data, route, successFunction, errorFunction) {
            $http.post(route, data).then(function (response) {
                successFunction(response.data, response.status);
            }, function (response) {
                errorFunction(response);
            });
        };

        this.AjaxGet = function (route, successFunction, errorFunction) {
            $http({ method: 'GET', url: route }).then(function (response) {
                successFunction(response.data, response.status);
            }, function (response) {
                errorFunction(response);
            });
        }
    }

})();