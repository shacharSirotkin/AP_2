$(document).ready(function () {
    showPNotify();
});

//notify registration succes
function showPNotify()
{
    if (sessionStorage.getItem("PnotifyLog") == "true") {
        new PNotify(
            {
                title: 'user registered!',
                text: 'You are successfully registered!\nLog in to play multiplayer!',
                type: 'success',
                hide: true
            });
        sessionStorage.setItem("PnotifyLog", "false");
    }
}

//log in to the game
function login() {
    $(".loader")[0].style.display = "block";
    var username = document.getElementById("username").value;
    var password = document.getElementById("password").value;

    var userToFind = {
        Username: username,
        Email: "email",
        Password: password
    }

    var uri = "../api/Users/" + username;
    $.get(uri, userToFind).done(function (data)
    {
        $(".loader")[0].style.display = "none";
        if (data)
        {
            var userUsermame = data.Username;
            var userEmail = data.Email;
            var userPassword = data.Password;
            var userWins = data.Wins;
            var userLosses = data.Losses;

            sessionStorage.setItem("logged", "true");
            sessionStorage.setItem("userName", userUsermame);
            window.location.replace("HomePage.html");
        }
        else 
        {
            new PNotify(
            {
                title: 'Incorrect details!',
                text: 'Your username or password are incorrect, please try again.',
                type: 'error',
                hide: true
            });
        }
    });

}