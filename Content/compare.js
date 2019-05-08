var app = angular.module('compapp', []);

app.controller
    ('comparer', function ($scope)
    {
        $scope.comp = function (pwd,cpwd)
        {
            if ($scope.pwd != $scope.cpwd) {
                alert("Password and Confirm Password doesn't match");
                $scope.cpwd = null;
            }
        }

       
    }

)