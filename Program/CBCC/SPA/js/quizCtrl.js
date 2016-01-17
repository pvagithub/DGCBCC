app.controller('quizCtrl', ['$scope', '$http', 'helperService', function ($scope, $http, helper) {
    $scope.quizName = 'SPA/data/csharp.js';

    //Note: Only those configs are functional which is documented at: http://www.codeproject.com/Articles/860024/Quiz-Application-in-AngularJs
    // Others are work in progress.
    $scope.defaultConfig = {
        'allowBack': true,
        'allowReview': false,
        'autoMove': true,  // if true, it will move to next question automatically when answered.
        'duration': 0,  // indicates the time in which quiz needs to be completed. post that, quiz will be automatically submitted. 0 means unlimited.
        'pageSize': 1,
        'requiredAll': false,  // indicates if you must answer all the questions before submitting.
        'richText': false,
        'shuffleQuestions': false,
        'shuffleOptions': false,
        'showClock': true,
        'showPager': true,
        'theme': 'none',
        'submit': false,
        'title': true
    }
    $scope.goTo = function (index) {
        if (index > 0 && index <= $scope.totalItems) {
            $scope.currentPage = index;
            $scope.mode = 'quiz';
        }
        if (index > $scope.totalItems) index = $scope.totalItems;
        $scope.config.allowReview = (index == $scope.totalItems);
    }
    $scope.onSelect = function (question, option, check) {
        if (question.QuestionTypeId == 1) { //
            question.Answered = 0;
            question.Options.forEach(function (element, index, array) {
                if (element.Id == option.Id) {
                    var id = '#' + option.Id;
                    if ($(id).prop('checked') == true) {
                        question.Answered = element.Id;
                    }
                }
                else {
                    element.Selected = false;
                }
            });
        }
        if ($scope.currentPage == $scope.totalItems) {
            $scope.onSubmit();
            window.location = 'GopY/Index';
        }
        $scope.goTo($scope.currentPage + 1);
    }

    $scope.onSubmit = function () {
        var answers = [];
        $scope.questions.forEach(function (q, index) {
            if (q.QuestionTypeId == 1) {
                answers.push({ 'QuizId': $scope.quiz.Id, 'QuestionId': q.Id, 'Answered': q.Answered, "QuestionTypeId": q.QuestionTypeId });
            }
            // go text
            //else {
            //    answers.push({ 'QuizId': $scope.quiz.Id, 'QuestionId': q.Id, 'Answered': q.Options[0].Selected, "QuestionTypeId": q.QuestionTypeId });
            //}
        });

        var validate = true;
        $scope.config.submit = true;
        //bootbox.confirm("Bạn có chắc chắn kết thúc đánh giá?", function (result) {
        //    if (result) {
        //        var _danhSachKQ = JSON.stringify(answers);

        //        $http.post('/DanhGia/CheckDanhGia?' + 'DanhSachKQ=' + _danhSachKQ, answers).success(function (data, status) {
        //            if (data.result == true) {
        //                var IDHoSo = ($('#idHoSo').val());

        //                $http.post('/DanhGia/SaveDanhGia?' + 'DanhSachKQ=' + _danhSachKQ + '&iDHoSo=' + IDHoSo, answers).success(function (data, status) {
        //                    if (data.result == true) {
        //                        $scope.mode = 'result';
        //                    }
        //                    else {
        //                        //Bao loi
        //                    }
        //                });
        //            }
        //            else {
        //                $scope.mode = 'review';
        //            }
        //        });
        //    }
        //});


        var _danhSachKQ = JSON.stringify(answers);
        var IDHoSo = ($('#idHoSo').val());
        if ($scope.config.submit == true) {
            $http.post('/DanhGia/SaveDanhGia?' + 'DanhSachKQ=' + _danhSachKQ + '&iDHoSo=' + IDHoSo, answers).success(function (data, status) {
                if (data.result == true) {
                    $scope.mode = 'result';
                }
                else {
                    //Bao loi
                }
            });
        }
        //$http.post('/DanhGia/CheckDanhGia?' + 'DanhSachKQ=' + _danhSachKQ, answers).success(function (data, status) {
        //    if (data.result == true) {
        //        var IDHoSo = ($('#idHoSo').val());

        //        $http.post('/DanhGia/SaveDanhGia?' + 'DanhSachKQ=' + _danhSachKQ + '&iDHoSo=' + IDHoSo, answers).success(function (data, status) {
        //            if (data.result == true) {
        //                $scope.mode = 'result';
        //            }
        //            else {
        //                //Bao loi
        //            }
        //        });
        //    }
        //    else {
        //        $scope.mode = 'review';
        //    }
        //});
    }

    $scope.pageCount = function () {
        return Math.ceil($scope.questions.length / $scope.itemsPerPage);
    };

    //If you wish, you may create a separate factory or service to call loadQuiz. To keep things simple, i have kept it within controller.
    $scope.loadQuiz = function (file) {
        $http.get(file)
         .then(function (res) {
             $scope.quiz = res.data.quiz;
             $scope.config = helper.extend({}, $scope.defaultConfig, res.data.config);
             $scope.questions = $scope.config.shuffleQuestions ? helper.shuffle(res.data.questions) : res.data.questions;
             $scope.totalItems = $scope.questions.length;
             $scope.itemsPerPage = $scope.config.pageSize;
             $scope.currentPage = 1;
             $scope.mode = 'quiz';

             $scope.$watch('currentPage + itemsPerPage', function () {
                 var begin = (($scope.currentPage - 1) * $scope.itemsPerPage),
                   end = begin + $scope.itemsPerPage;
                 $("#currentPage").val(end);
                 
                 $scope.filteredQuestions = $scope.questions.slice(begin, end);
             });
             $http.post('/DanhGia/SelectGopYCauHoi?soBienNhan=' + $('#soBienNhan').val() + '&cauHoiId=' + $("#currentPage").val(), answers).success(function (data, status) {
                 $('#txtGopY').val(data);
             });
         });
    }
    $scope.loadQuiz($scope.quizName);

    $scope.isAnswered = function (index) {
        var answered = 'Chưa đánh giá';
        $scope.questions[index].Options.forEach(function (element, index, array) {
            if (element.Selected == true) {
                answered = 'Đã đánh giá';
                return false;
            }
        });
        return answered;
    };

    $scope.isTypping = function (index) {
        var answered = 'Chưa đánh giá';

        if ($scope.questions[index].Options[0].Selected != null && $scope.questions[index].Options[0].Selected.length > 0) {
            answered = 'Đã đánh giá';
        }

        return answered;
    };

    $scope.isCorrect = function (question) {
        var result = 'correct';
        question.Options.forEach(function (option, index, array) {
            if (helper.toBool(option.Selected) != option.IsAnswer) {
                result = 'wrong';
                return false;
            }
        });
        return result;
    };
}]);