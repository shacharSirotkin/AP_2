$(document).ready(function () {
    showRank();
});

//show user's ranks
function showRank()
{
    $(".loader")[0].style.display = "block";
    var uri = "../api/Users";
    $.get(uri).done(function (data) {
        var userrsArray = $(data).toArray(s => new { s: Username, s: Password, s: Email, s: Wins, s: Losses });
        userrsArray.sort(function (a, b) { return b.Wins - a.Wins });
        var table = document.getElementById("usersTable");
        var i;       
        for (i = 0; i < userrsArray.length; i++) {
            var row = table.insertRow(i + 1);
            var rank = row.insertCell(0);
            var username = row.insertCell(1);
            var wins = row.insertCell(2);
            var losses = row.insertCell(3);

            rank.innerHTML = i + 1;
            username.innerHTML = userrsArray[i].Username;
            wins.innerHTML = userrsArray[i].Wins;
            losses.innerHTML = userrsArray[i].Losses;
        }
        $(".loader")[0].style.display = "none";
    });
}