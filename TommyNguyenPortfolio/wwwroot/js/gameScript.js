// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Generates the new position of the button for the first game. Also changes the inner text of the button to a string in the array.
let currentIndex = 0;
function GameOneButtonMove() {
    const messageToUser = ["Come on!", "Is that all you got?", "This is getting kind of boring, you know?", "How about using your head a little?", "You suck at this game!", "Uh oh..."];
    $("#ButtonContainer").mousemove(function () {
        if ($("#ButtonContainer").innerWidth() == 0 || $("#ButtonContainer").innerHeight() == 0 || !document.getElementById("ButtonContainer")) {
            $("#clickMe").html(messageToUser[5]);
            return;
        }
        if ($(window).innerHeight() > 200 && $(window).innerWidth() > 200) {
            const newPosition = randomNumber();
            $("#ButtonContainer").css({
                'top': newPosition[0],
                'left': newPosition[1],
                'position': 'relative'
            });
            $("#clickMe").html(messageToUser[currentIndex % 4]);
            currentIndex++;
        }
        else {
            $("#clickMe").html(messageToUser[5]);
            return;
        }
    });
}

//Generates the new position that can either be negative or positive.
function randomNumber() {
    let negativeOrNot = Math.random() > .5 ? -1 : 1;
    let Y = negativeOrNot*Math.random() * 100 + 1;
    let X = negativeOrNot * Math.random() * 100 + 1;
    const newPositionOfButton = [Y, X];
    return newPositionOfButton;
}

function RedirectToGame2Page() {
    window.location.href = window.location.origin + "/TommysGame/Game2";
}

function TallPlayer(player) {
    player.style.height = "50px";
    player.style.width = "50px";
}

function ShortPlayer(player) {
    player.style.height = "30px";
    player.style.width = "30px";
}

function PlayerPickedBluePill() {
    window.location.href = "https://youtu.be/UkBmv2C8EX8";
}

function PlayerPickedRedPill() {
    window.location.href = window.location.origin + "/TommysGame/RedPillPicked";
}

function redirectToHome() {
    window.location.href = window.location.origin;
}

var isOverSilhouette = false;
//Changes the silhouette to open mouth when the player hovers over the silhouette
//Fixed it. Turns out I needed the full address instead of a relative path (since relative path doesn't work in JS).
function mouseOverSilhouette() {
    var addressOfImage = window.location.origin + "/Content/SilhouetteMouthOpen.png";
    $("#SilhouetteClosedMouth").attr('src', addressOfImage);
}

//Changes image back to original image when mouse is off silhouette.
function mouseOffSilhouette() {
    var addressOfImage = window.location.origin + "/Content/SilhouetteMouthClosed.png";
    //$("#SilhouetteClosedMouth").attr('src', addressOfImage);
    if (document.getElementById("SilhouetteClosedMouth") != null) {
        document.getElementById("SilhouetteClosedMouth").src = "../Content/SilhouetteMouthClosed.png";
    }
}

function wrongAnswer() {
    var addressOfImage = window.location.origin + "/Content/SilhouetteMouthClosedWrong.png";
    $("#SilhouetteClosedMouth").attr('src', addressOfImage);
}
function rightAnswer() {
    var addressOfImage = window.location.origin + "/Content/SilhouetteMouthClosedRight.png";
    $("#SilhouetteClosedMouth").attr('src', addressOfImage);
}

function whichAnswerIsCorrect(button1, button2, button3) {
    var button1txt = button1.textContent || button1.innerText;
    var button2txt = button2.textContent || button2.innerText;
    var button3txt = button3.textContent || button3.innerText;
    if (button1txt.includes(":CORRECT") && button2txt.includes(":CORRECT") && button3txt.includes(":CORRECT")) {
        return "ALL";
    }
    else if (button2txt.includes(":CORRECT")) {
        return button2.id;
    }
    else if (button3txt.includes(":CORRECT")) {
        return button3.id;
    }
    else if (button1txt.includes(":CORRECT")){
        return button1.id;
    }
}


var correctAnswer = "";
var IndexRightNow = -1;
function assignedQuestionsAndAnswersToButtons(button) {
    IndexRightNow = IndexRightNow + 1;
    var QuestionsArray = ["What was the first company Tommy interned for?", "What was the first independent project Tommy worked on?", "What company did Tommy recently received a badge for?", "What skill was Tommy most endorsed for?", "What was the one thing Tommy did that Garnet said 'you will be able to sleep comfortably at night'?", "Do you think the games are neat? (Yes, there is a right answer to this :) )"];

    var AnswersArray = [["InterlinkONE", "State Street:CORRECT", "Wentworth Institute of Technology"], ["Phoenix Pho Database Project", "Personal Assistant Sebastian", "Tag You're It:CORRECT"], ["Google", "Amazon", "IBM:CORRECT"], ["Leadership", "Teamwork:CORRECT", "Customer Service"], ["Comment/Simplify code", "Test code:CORRECT", "Asked questions"], ["Yes:CORRECT", "Yes:CORRECT", "Yes:CORRECT"]];
    if (IndexRightNow < QuestionsArray.length) {
        document.getElementById("Question").textContent = QuestionsArray[IndexRightNow];
        document.getElementById("button1").textContent = AnswersArray[IndexRightNow][0];
        document.getElementById("button2").textContent = AnswersArray[IndexRightNow][1];
        document.getElementById("button3").textContent = AnswersArray[IndexRightNow][2];
        correctAnswer = whichAnswerIsCorrect(document.getElementById("button1"), document.getElementById("button2"), document.getElementById("button3"))
        document.getElementById("button1").textContent = AnswersArray[IndexRightNow][0].replace(":CORRECT", "");
        document.getElementById("button2").textContent = AnswersArray[IndexRightNow][1].replace(":CORRECT", "");
        document.getElementById("button3").textContent = AnswersArray[IndexRightNow][2].replace(":CORRECT", "");
    }
    else {
        window.location.href = window.location.origin + "/TommysGame/FinalPage";
    }
}

function isSelectedButtonTheRightAnswer(button) {

    var buttonID = $(button).attr("id");
    if (buttonID == correctAnswer || correctAnswer == "ALL") {
        rightAnswer();
        setTimeout(() => { assignedQuestionsAndAnswersToButtons(button); }, 3000);
    }
    else {
        var addressOfImage = window.location.origin + "/Content/SilhouetteMouthClosedWrong.png";
        $("#SilhouetteClosedMouth").attr('src', addressOfImage);
    }
}