//seet navBar depending on user loging
$(document).ready(function () {
    if (sessionStorage.getItem("logged") == "true")
    {
        $("#NavBar").load("../View/NavBarLog.html");
    }
    else
    {
        $("#NavBar").load("../View/NavBar.html");
    }
});