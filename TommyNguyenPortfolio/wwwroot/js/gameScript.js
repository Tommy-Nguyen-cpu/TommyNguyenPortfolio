// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
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
    window.location.href = window.location.origin + "/TommysGame/BluePillPicked";
}

function PlayerPickedRedPill() {
    window.location.href = window.location.origin + "/TommysGame/RedPillPicked";
}

function redirectToHome() {
    window.location.href = window.location.origin;
}