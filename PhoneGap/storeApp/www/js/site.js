apiUrl = "https://phonegap-api.azurewebsites.net/api/";

$(document).bind('deviceready', function () {
    $('#register-form').on('submit', function (e) {
        e.preventDefault();
        var data = objectifyForm($('#register-form').serializeArray());
        $.ajax({
            type: 'POST',
            url: apiUrl + 'users/register',
            data: JSON.stringify(data),
            crossDomain: true,
            headers: {
                "Content-Type": "application/json; charset=UTF-8"
            },
            success: function (resultData) {
                alert('You may now login to your account');
                window.location.href = 'user.html';
            },
            error: function (xhr, status, error) {
                alert(xhr.responseJSON.message);
            }
        });
    });

    $('#login-form').on('submit', function (e) {
        e.preventDefault();
        var data = objectifyForm($('#login-form').serializeArray());
        $.ajax({
            type: 'POST',
            url: apiUrl + 'users/authenticate',
            data: JSON.stringify(data),
            crossDomain: true,
            headers: {
                "Content-Type": "application/json; charset=UTF-8"
            },
            success: function (resultData) {
                console.log(resultData);
                localStorage.setItem('user', JSON.stringify(resultData));
                window.location.href = 'index.html';
            },
            error: function (xhr, status, error) {
                alert(xhr.responseJSON.message);
            }
        });
    });
});

function objectifyForm(formArray) { //serialize data function

    var returnArray = {};
    for (var i = 0; i < formArray.length; i++) {
        returnArray[formArray[i]['name']] = formArray[i]['value'];
    }
    return returnArray;
}