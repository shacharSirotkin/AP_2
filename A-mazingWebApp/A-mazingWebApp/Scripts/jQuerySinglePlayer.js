$(document).ready(function () {
    //set default details
    document.getElementById("rows").value = localStorage.rows;
    document.getElementById("cols").value = localStorage.cols;
    document.getElementById("algoSelect").value = localStorage.algoSelect;
    var canvas1;

    //addding click function to start button
    $("#startBtn").click(function () {
        var context = $("#mazeCanvas")[0].getContext("2d")
        context.clearRect(0, 0, $("#mazeCanvas")[0].width, $("#mazeCanvas")[0].height);
        $("#solveBtn").off("click");
        $(".loader")[0].style.display = "block";
        var name = document.getElementById("name").value;
        var rows = document.getElementById("rows").value;
        var cols = document.getElementById("cols").value;
        document.title = name;
        //getting maze from server
        var uri = "../api/SinglePlayer/" + name + "/" + rows + "/" + cols;
        $.getJSON(uri).done(function (data) {
            var startImg = document.getElementById("player");
            var endImg = document.getElementById("end");
            var mazeString = data.Maze;
            var mazeRows = data.Rows;
            var mazeCols = data.Cols;
            var startRow = data.Start.Row;
            var startCol = data.Start.Col;
            var endRow = data.End.Row;
            var endCol = data.End.Col;
            var maze = parseMazeString(mazeString, mazeRows, mazeCols);
            var startRow = data.Start.Row;
            var loaders = $(".loader");
            loaders[0].style.display = "none";
            canvas1 = $("#mazeCanvas").mazeBoard(maze, startRow, startCol, endRow, endCol, startImg, endImg, true, function (dummy) { });
            $("#mazeCanvas").show();
        });
    //addding click function to solve button
    $("#solveBtn").click(solveMaze);
    });

    //solve the maze
    function solveMaze() {
        var name = document.getElementById("name").value;
        var algo = document.getElementById("algoSelect").value;
        var algoNum;
        if (algo == "BFS") {
            algoNum = 0;
        } else {
            algoNum = 1;
        }
        var uri = "../api/SinglePlayer/" + name + "/" + algoNum;
        $.getJSON(uri).done(function (data) {
            var solution = data.Solution;
            canvas1.solve(solution);
        });
    }
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