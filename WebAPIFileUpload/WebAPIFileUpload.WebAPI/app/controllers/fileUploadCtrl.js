﻿'use strict';

angular.module('angularUploadApp')
    .controller('fileUploadCtrl', function ($scope, $http, $timeout, Upload) {
        $scope.progress = 0;
        $scope.sizeLimit = 2147483647; //About 2GB Size Limit
        $scope.upload = [];
        $scope.UploadedFiles = [];

        $scope.startUploading = function ($files) {
            $scope.UploadingFiles = [];
            //$files: an array of files selected
            for (var i = 0; i < $files.length; i++) {
                var $file = $files[i];

                $scope.UploadingFiles.push({Name: $file.name, Progress: 0});
                $scope.progress = 0;

                (function (index) {
                    $scope.upload[index] = Upload.upload({
                        url: "./api/fileupload", // webapi url
                        method: "POST",
                        file: $file
                    })
                    .progress(function (evt) {
                        $scope.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                        $scope.UploadingFiles[index].Progress = $scope.progress;
                    })
                    .success(function (data, status, headers, config) {
                        // file is uploaded successfully
                        $scope.UploadedFiles.push({ FileName: data.FileName, FilePath: data.LocalFilePath, FileLength: data.FileLength });
                    })
                    .error(function (data, status, headers, config) {
                        console.log(data);
                    });
                })(i);
            }
        }
    });
