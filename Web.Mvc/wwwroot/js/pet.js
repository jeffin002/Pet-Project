$(document).ready(function () {
    $('#btnsubmit').click(function (event) {
        event.preventDefault();
        var firstName = $('#firstname').val();
        var lastName = $('#lastname').val();
        var email = $('#email').val();
        var description = $('#description').val();

        //create doctor javascript object
        var doctor = {
            firstName: firstName,
            lastName: lastName,
            email: email,  
            description: description
        };

        var doctorJson = JSON.stringify(doctor);
        console.log(doctorJson);

        $.ajax({
            type: "POST",
            url: "/doctor/index",
            data: JSON.stringify(doctor),
            contentType: "application/json; charset=utf-8",
            dataType: "json",

            success: function (response) {
                console.log(response);
                if (response != null) {
                    alert("Name : " + response.firstName);
                } else {
                    alert("Something went wrong");
                }
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                console.log(response);
                alert(response.responseText);
            }
        });
    });
});
