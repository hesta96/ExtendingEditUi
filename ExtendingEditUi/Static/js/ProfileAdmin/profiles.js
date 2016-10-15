var profilesApp = angular.module('ProfilesApp', []);

profilesApp.controller('profilesCtrl', function ($scope, $http) {

    $scope.currentPage = 1;
    $scope.userProfiles = null;
    $scope.currentObject = null;
    $scope.editObject = false;
    $scope.totalUserProfiles = 0;
    $scope.portalObjectsPerPage = 15;

    $scope.pagination = {
        current: 1
    }

    $scope.search = function () {
        var query = "";
        
        if ($scope.searchString) {
            query = $scope.searchString;
        }
        
        $scope.userProfiles = null;
        $http.get('/api/ProfileApi/SearchUserProfiles/?searchString=' + query)
			.then(function (result) {
			    $scope.totalUserProfiles = result.data.Count;
			    $scope.userProfiles = result.data;
			});
    };

    $scope.EditUserProfile = function (userProfile) {
        if ($scope.editObject === true) {
            $scope.currentObject = null;
            $scope.editObject = false;
        } else {
            $http.get('/api/ProfileApi/GetUserProfile?userId=' + userProfile.UserId)
                .success(function (data) {
                    $scope.currentObject = data;
                    $scope.editObject = true;
                });
        }
    };

    $scope.CancelEditUserProfile = function () {
        $scope.currentObject = null;
        $scope.editObject = false;
    };

    $scope.submitEditUserProfile = function () {
        if ($scope.currentObject != null && $scope.currentObject.UserId != null) {

            $http.post('/api/ProfileApi/UpsertUserProfile', $scope.currentObject)
                .success(function (data) {
                    $scope.currentObject = null;
                    $scope.editObject = false;
                    $scope.search($scope.searchString);
                })
                .error(function () {
                    alert("FEL");
                });
        }
    };
});