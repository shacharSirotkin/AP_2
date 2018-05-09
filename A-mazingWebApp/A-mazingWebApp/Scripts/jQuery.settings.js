document.getElementById("rows").value = localStorage.rows;
document.getElementById("cols").value = localStorage.cols;
document.getElementById("algoSelect").value = localStorage.algoSelect;

//set save button function
function save()
{
    var rows = document.getElementById("rows").value;
    var cols = document.getElementById("cols").value;
    var algoSelect = document.getElementById("algoSelect").value;

    localStorage.rows = rows;
    localStorage.cols = cols;
    localStorage.algoSelect = algoSelect;
}