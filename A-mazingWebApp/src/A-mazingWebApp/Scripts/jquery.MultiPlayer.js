$(document).ready(function () {

    //setting defaults
    document.getElementById("rows").value = localStorage.rows;
    document.getElementById("cols").value = localStorage.cols;

    var myCanvas;
    var opponentCanvas;
    var multiGamesHub = $.connection.multiGameHub;

    //set gameList with knockout
    var viewModel = function () {
        var self = this;
        self.games = ko.observableArray();
        multiGamesHub.client.fillGameSelection = function (games) {
            self.games(games.gameList);
        };
    };
    ko.applyBindings(new viewModel());

    //draw the mazes from server
    multiGamesHub.client.drawMazes = function (maze) {
        var playerImg = document.getElementById("player");
        var endImg = document.getElementById("end");
        var opponentImg = document.getElementById("opponent");
        var mazeString = maze.Maze;
        var mazeRows = maze.Rows;
        var mazeCols = maze.Cols;
        var startRow = maze.Start.Row;
        var startCol = maze.Start.Col;
        var endRow = maze.End.Row;
        var endCol = maze.End.Col;
        var m = parseMazeString(mazeString, mazeRows, mazeCols);
        var startRow = maze.Start.Row;
        myCanvas = $("#myCanvas").mazeBoard(m, startRow, startCol, endRow, endCol, playerImg, endImg, true, function (message) {
            multiGamesHub.server.sendPlayMessage(message);
        });
        opponentCanvas = $("#opponentCanvas").mazeBoard(m, startRow, startCol, endRow, endCol, opponentImg, endImg, false, function (dummy) { });
        var loaders = $(".loader");
        loaders[0].style.display = "none";
        loaders[1].style.display = "none";
    };

    //send messsage from other client
    multiGamesHub.client.gotPlayMessage = function (playMessage) {
        if (playMessage === "you lost the game") {
            myCanvas.endGame();
            var uri = "../api/Users/UpdateScores/" + sessionStorage.getItem("userName") + "/lost";
            $.getJSON(uri).done(function (data) {
                alert(data);
            });
        }
        else {
            opponentCanvas.movePlayerExternally(playMessage);
        }
    };

    $.connection.hub.start().done(function () {
        //get games
        multiGamesHub.server.gameList();
            //join game in server
        ; $("#joinBtn").click(function () {
            $(".loader")[0].style.display = "block";
            $(".loader")[1].style.display = "block";
            gamesList = document.getElementById("gamesDropdown");
            selectedGame = gamesList.options[gamesList.selectedIndex].value;
            document.title = selectedGame;
            multiGamesHub.server.joinGame(selectedGame);
        });
        //start game
        $("#startBtn").click(function () {
            if ($("#name").val() != "" && $("#rows").val() > 0 && $("#cols").val() > 0) {
                $(".loader")[0].style.display = "block";
                $(".loader")[1].style.display = "block";
                document.title = $("#name").val();
                multiGamesHub.server.startGame($("#name").val(), $("#rows").val(), $("#cols").val());
            }
            else {
                new PNotify({
                    title: 'Wrong arguments',
                    text: "Rows and Columns must be positive numbers. Name can't be empty.",
                });
            }
        });
    });
    //handle connection problems
    $.connection.hub.reconnecting(function () {
        new PNotify({
            title: 'Disconnected',
            text: 'trying to reconnect to the server',
            type: 'error'
        });
    });

    //parse the mazeString to matrix
    function parseMazeString(mazeString, rows, cols) {
        var maze = [];
        var index = 0;
        for (var i = 0; i < rows; i++) {
            maze.push([]);
            for (var j = 0; j < cols; j++) {
                maze[i].push(mazeString.charAt(index));
                index = index + 1;
            }
        }
        return maze;
    }
});