var app = angular.module('customerapp', []);

app.controller
    ('validate', function ($scope)
    {
        $scope.test = function (text0,text1,text2)
        {
            $scope.ans =text0+" "+ text1 +" "+ text2;
        }

        $scope.agecal = function (date)
        {
                var ageDifMs = Date.now() - date.getTime();
                var ageDate = new Date(ageDifMs);
                if ((Math.abs(ageDate.getUTCFullYear() - 1970)) < 10) {
                    alert("Age must be above 10 years");
                    $scope.date = null;
                }
                else if ((Math.abs(ageDate.getUTCFullYear() - 1970)) > 10) {
                    $scope.age = Math.abs(ageDate.getUTCFullYear() - 1970);
                }

        }
    }

)