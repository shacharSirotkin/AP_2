(function ($) {
    $.fn.mazeBoard = function (mazeData, startRow, startCol, exitRow, exitCol, playerImage, exitImage, isEnabled, callBackFunc) {
        var context;
        var rows;
        var cols;
        var playerImg;
        var cellWidth;
        var cellHeight;
        var currentRow;
        var currentCol;
        var endRow;
        var endCol;
        var initialRow;
        var initialCol;
        var mazeDataMatrix;

        var canvas = this[0];
        context = canvas.getContext("2d");
        rows = mazeData.length;
        cols = mazeData[0].length;
        playerImg = playerImage;
        cellWidth = canvas.width / cols;
        cellHeight = canvas.height / rows;
        currentRow = startRow;
        currentCol = startCol;
        mazeDataMatrix = mazeData;
        endRow = exitRow;
        endCol = exitCol;
        initialRow = startRow;
        initialCol = startCol;
        canvas = this;

        //move opponent
        this.movePlayerExternally = function (direction) {
            switch (direction) {
                case "ArrowLeft":
                    drawPlayerImage(currentCol - 1, currentRow);
                    break;
                case "ArrowRight":
                    drawPlayerImage(currentCol + 1, currentRow);
                    break;
                case "ArrowUp":
                    drawPlayerImage(currentCol, currentRow - 1);
                    break;
                case "ArrowDown":
                    drawPlayerImage(currentCol, currentRow + 1);
                    break;
                default:
                    break;
            }
        };

        //move player
        function movePlayer(event) {
            var direction = event.key;
            switch (direction) {
                case "ArrowLeft":
                    if (currentCol == 0) {
                        break;
                    }
                    if (mazeDataMatrix[currentRow][currentCol - 1] == 0) {
                        drawPlayerImage(currentCol - 1, currentRow);
                        callBackFunc(direction);
                    }
                    break;
                case "ArrowUp":
                    if (currentRow == 0) {
                        break;
                    }
                    if (mazeDataMatrix[currentRow - 1][currentCol] == 0) {
                        drawPlayerImage(currentCol, currentRow - 1);
                        callBackFunc(direction);
                    }
                    break;
                case "ArrowRight":
                    if (mazeDataMatrix[currentRow][currentCol + 1] == 0) {
                        drawPlayerImage(currentCol + 1, currentRow);
                        callBackFunc(direction);
                    }
                    break;
                case "ArrowDown":
                    if (mazeDataMatrix[currentRow + 1][currentCol] == 0) {
                        drawPlayerImage(currentCol, currentRow + 1);
                        callBackFunc(direction);
                    }
                    break;
                default:
                    return;
            }
            if (currentRow == endRow && currentCol == endCol) {
                callBackFunc("you lost the game");
                document.removeEventListener('keydown', movePlayer);
                new PNotify({
                    title: 'WOW! Amazing! YOU WON!!',
                    text: '',
                    type: 'success',
                    hide: false
                });
                var uri = "../api/Users/UpdateScores/" + sessionStorage.getItem("userName") + "/win";
                $.getJSON(uri).done(function (data) {
                    alert(data);
                });
            }
        };

        //draw Player Image
        function drawPlayerImage(playerCol, playerRow) {
            context.fillStyle = "#FFFFFF";
            context.fillRect(cellWidth * currentCol, cellHeight * currentRow, cellWidth, cellHeight);
            context.fillStyle = "#000000";
            currentCol = playerCol;
            currentRow = playerRow;
            context.drawImage(playerImg, currentCol * cellWidth, currentRow * cellHeight, cellWidth, cellHeight);
        };

        //solve the maze
        this.solve = function (solution) {
            var canvas = $(this)[0];
            var context = canvas.getContext("2d");
            drawPlayerImage(initialCol, initialRow);
            var size = solution.length
            var i = 0;
            var timer = setInterval(function () {
                switch (solution[i]) {
                    case "0":
                        drawPlayerImage(currentCol - 1, currentRow);
                        break;
                    case "1":
                        drawPlayerImage(currentCol + 1, currentRow);
                        break;
                    case "2":
                        drawPlayerImage(currentCol, currentRow - 1);
                        break;
                    case "3":
                        drawPlayerImage(currentCol, currentRow + 1);
                        break;
                    default:
                        break;
                }
                i++;
                if (currentRow == endRow && currentCol == endCol) {
                    clearInterval(timer);
                    document.removeEventListener('keydown', movePlayer);
                }
            }, 100);
        };

        //end multiplayer game
        this.endGame = function () {
            document.removeEventListener('keydown', movePlayer);
            new PNotify({
                title: "You've been defeated!",
                text: '',
                type: 'error',
                hide: false
            });
        }

        //draw one maze
        function drawMaze() {
            for (var i = 0; i < rows; i++) {
                for (var j = 0; j < cols; j++) {
                    if (mazeDataMatrix[i][j] == 1) {
                        context.fillRect(cellWidth * j, cellHeight * i, cellWidth, cellHeight);
                    }
                }
            }
            context.drawImage(playerImage, startCol * cellWidth, startRow * cellHeight, cellWidth, cellHeight);
            context.drawImage(exitImage, exitCol * cellWidth, exitRow * cellHeight, cellWidth, cellHeight);
            //adding movePlayer function to the keydown event
            if (isEnabled) {
                document.addEventListener('keydown', movePlayer, false);
            }
        }
             
        drawMaze();
        return this;
    };
})(jQuery);