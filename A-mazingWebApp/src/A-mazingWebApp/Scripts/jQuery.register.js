//set registration button
$("#registerBtn").click(function register() {
    var username = document.getElementById("username").value;
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    var confirmPassword = document.getElementById("confirmPassword").value;

    var usr = {
        Username: username,
        Email: email,
        Password: password
    }

    //check passwords identity
    if (password == confirmPassword) {
        $(".loader")[0].style.display = "block";
        var uri = '../api/Users';
        $.post(uri, usr).done(function (data) {
            $(".loader")[0].style.display = "none";
            if (data == "Success")
            {
                sessionStorage.setItem("PnotifyLog", "true");
                window.location.replace("Login.html");
            }
            else
            {
                new PNotify(
                    {
                        title: 'error!',
                        text: 'Username is already exist! Choose another one.',
                        type: 'error',
                        hide: true
                    });
            }
        });
    }
    else
    {
        new PNotify(
            {
                title: 'error!',
                text: 'Check your password! You need to type it twice.',
                type: 'error',
                hide: true
            });
    }
});