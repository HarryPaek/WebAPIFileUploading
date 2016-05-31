'use strict';

angular.module('angularUploadApp', ['ngRoute', 'ngFileUpload'])

.config(function ($routeProvider) {
    $routeProvider
    .when('/', {
        templateUrl: 'app/templates/fileUpload.html',
        controller: 'fileUploadCtrl'
    })
    .otherwise({
        redirectTo: '/'
    });
});
