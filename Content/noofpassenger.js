var app = angular.module('passapp', []);

app.controller
('passenger', function ($scope)
    {
        $scope.add = function (num1, num2)
        {
            if ($scope.num1 < 0 || $scope.num2<0) {
                $scope.num1 = null;
                $scope.num2 = null;
                alert("Sorry! You Entered negative number");
            }
       
            $scope.result = parseInt(num1) + parseInt(num2);

            if ($scope.result > 7 || $scope.result == 0 || $scope.result == NaN)
            {
                $scope.num1 = null;
                $scope.num2 = null;
                alert("Sorry! You exceeded the limit");
            
            }

        }
    }
 );


