$(document).ready(function () {
    $('#btnsubmit').click(function (event) {
        event.preventDefault();
        var firstName = $('#firstname').val();
        var lastName = $('#lastname').val();
        var email = $('.email').val();

        //create doctor javascript object
        var doctor = {
            firstName: firstName,
            lastName: lastName,
            email: email            
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
                if (response != null) {
                    alert("Name : " + response.Name + ", Designation : " + response.Designation + ", Location :" + response.Location);
                } else {
                    alert("Something went wrong");
                }
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    });
});
