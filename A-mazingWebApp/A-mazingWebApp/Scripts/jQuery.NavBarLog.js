//send hello to user
$(document).ready(function () {
    var hello = document.getElementById("helloUsername");
    if (hello) {
        hello.innerHTML = "Hello " + sessionStorage.getItem("userName") + "!";
    }
});

//set logOut button
$("#logOut").click(function logOut()
    {
        sessionStorage.setItem("logged", "false");
        sessionStorage.setItem("userName", "");
        window.location.replace("HomePage.html");
    });