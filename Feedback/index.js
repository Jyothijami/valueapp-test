Survey
    .StylesManager
//.applyTheme("bootstrap");
showNavigationButtons: true;
goNextPageAutomatic: true;
showProgressBar: "top";
cookieName: "myuniquesurveyid",

//var json = {
//    //showNavigationButtons: true,
//    //goNextPageAutomatic: true,
//    //showProgressBar: "top",
//    cookieName: "myuniquesurveyid",
//}
Survey.defaultBootstrapMaterialCss.navigationButton = "btn btn-green";
Survey.defaultBootstrapMaterialCss.rating.item = "btn btn-default my-rating";
Survey.Survey.cssType = "bootstrapmaterial";

window.survey = new Survey.Model(json);

survey
    .onComplete
    .add(function (result) {
        document
            .querySelector('#surveyResult')
            .innerHTML = "result: " + JSON.stringify(result.data);
    });
//survey.showProgressBar = 'bottom';
//var storageName = "Survey_Feedback_history";
//function saveSurveyData(survey) {
//    var data = survey.data;
//    data.pageNo = survey.currentPageNo;
//    window
//        .localStorage
//        .setItem(storageName, JSON.stringify(data));
//}
//survey
//    .onPartialSend
//    .add(function (survey) {
//        saveSurveyData(survey);
//    });
//survey
//    .onComplete
//    .add(function (survey, options) {
//        saveSurveyData(survey);
//    });

//survey.sendResultOnPageNext = true;
//var prevData = window
//    .localStorage
//    .getItem(storageName) || null;
//if (prevData) {
//    var data = JSON.parse(prevData);
//    survey.data = data;
//    if (data.pageNo) {
//        survey.currentPageNo = data.pageNo;
//    }
//}
$("#surveyElement").Survey({ model: survey });

function animate(animitionType, duration) {
    if (!duration)
        duration = 1000;
    var element = document.getElementById("surveyElement");
    $(element).velocity(animitionType, { duration: duration });
}

var doAnimantion = true;
survey
    .onCurrentPageChanging
    .add(function (sender, options) {
        if (!doAnimantion)
            return;
        options.allowChanging = false;
        setTimeout(function () {
            doAnimantion = false;
            sender.currentPage = options.newCurrentPage;
            doAnimantion = true;
        }, 500);
        animate("slideUp", 500);
    });
survey
    .onCurrentPageChanged
    .add(function (sender) {
        animate("slideDown", 500);
    });
survey
    .onCompleting
    .add(function (sender, options) {
        if (!doAnimantion)
            return;
        options.allowComplete = false;
        setTimeout(function () {
            doAnimantion = false;
            sender.doComplete();
            doAnimantion = true;
        }, 500);
        animate("slideUp", 500);
    });
animate("slideDown", 1000);