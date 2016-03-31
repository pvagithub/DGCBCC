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
        'title': true,
        'force': 6
    }
    $scope.goTo = function (index) {

        if (index > 0 && index <= $scope.totalItems) {
            $scope.currentPage = index;
            if ($scope.currentPage > 6) {
                $scope.totalItems = 12;
            }
            $scope.showPopup();
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
        if ($scope.totalItems == 12) {
            $scope.pagingTotal();
        }
        if ($scope.currentPage == 12) {
            $scope.onSubmit();
            window.location = 'GopY/Index';
            $scope.currentPage = 13;
            $scope.config.showPager = false;
            $scope.mode = 'none';
        }
        if ($scope.currentPage == $scope.totalItems) {
            //$scope.onSubmit();
            //window.location = 'GopY/Index';
            $scope.mode = 'result';
            $scope.hidePopup();
            $scope.config.showPager = false;
        }
        $scope.goTo($scope.currentPage + 1);
    }
    $scope.review = function () {
        $scope.mode = 'review';
        $scope.hidePopup();
        $scope.config.showPager = true;
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
        var _donViID = $("#cbDonVi option:selected").val();
        var _soBN = $('#soBienNhan').val();
        if ($scope.config.submit == true) {
            $http.post('/DanhGia/SaveDanhGia?' + 'DanhSachKQ=' + _danhSachKQ + '&iDHoSo=' + IDHoSo + '&iDonViID=' + _donViID + '&soBN=' + _soBN, answers).success(function (data, status) {
                if (data.result == true) {
                    window.location = 'GopY/Index';
                    $scope.mode = 'result';
                    $scope.hidePopup();
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
             $scope.totalItems = $scope.currentPage < 7 ? $scope.config.force : $scope.questions.length;
             $scope.itemsPerPage = $scope.config.pageSize;
             $scope.currentPage = 1;
             $scope.mode = 'quiz';

             $scope.$watch('currentPage + itemsPerPage', function () {
                 var begin = (($scope.currentPage - 1) * $scope.itemsPerPage),
                   end = begin + $scope.itemsPerPage;
                 $("#currentPage").val(begin + $scope.itemsPerPage).trigger('change');
                 $scope.filteredQuestions = $scope.questions.slice(begin, end);
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

    $scope.isAnswered6Question = function (maxlen) {
        var count = 0;
        for (var index = 0; index < maxlen; index++) {
            $scope.questions[index].Options.forEach(function (element, index, array) {
                if (element.Selected == true) {
                    count++;
                }
            });
        }
        $scope.config.answered = count;
    };

    $scope.continue = function () {
        $scope.currentPage = 7;
        $scope.totalItems = 12;
        $scope.paging();
        $scope.config.showPager = true;
        $scope.mode = 'quiz';
        $scope.showPopup();
    }

    $scope.showPopup = function () {
        $('#fl813691').addClass('show').removeClass('hide');
    }

    $scope.hidePopup = function () {
        $('#fl813691').addClass('hide').removeClass('show');
    }

    $scope.paging = function () {
        $('.pagination_spa').find('.ng-scope').each(function (e) {
            var index = parseInt($(this).find('.ng-binding').text());
            if (index < 7) {
                $(this).find('.ng-binding').remove();
            }
        })
    }

    $scope.pagingTotal = function () {
        $('.pagination_spa').find('.ng-scope').each(function (e) {
            var index = parseInt($(this).find('.ng-binding').text());
            if (index > 6) {
                $(this).find('.ng-binding').text(index - $scope.config.force);
            }
        })
    }

}]);
