$(document).ready(function () {
   
    getAllDoctors();
    $('#btnsubmit').click(function (event) {
        event.preventDefault();
        var firstName = $('#firstname').val();
        var lastName = $('#lastname').val();
        var email = $('#email').val();
        var description = $('#description').val();
        var isformvalid = true;
        clearPreValidationErrors();

        if (!firstName) {
            console.log('inside firstname:', firstName);
            $('.spnfirstname').text('FirstName is Required!').addClass('field-validation-error').show();

        }

        if (!lastName) {
            $('.spnlastname').text('LastName is Required!').addClass('field-validation-error').show();

        }

        if (!email) {
            $('.spnemail').text('Email is Required!').addClass('field-validation-error').show();

        }

        if (!description) {
            $('.spndescription').text('Description is Required!').addClass('field-validation-error').show();

        }

        if (!firstName || !lastName || !email || !description) {
            console.log(firstName);
            isformvalid = false;
            return false;
        }

        function clearPreValidationErrors() {
            if (firstName) {
                $('.spnfirstname').hide();
            }
            if (lastName) {
                $('.spnlastname').hide();
            }
            if (email) {
                $('.spnemail').hide();
            }
            if (description) {
                $('.spndescription').hide();
            }
        }

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
    $('#petTypeSelect').change(function (e) {
        var petTypeId = $('#petTypeSelect').val();
        var petTypeIdint = parseInt(petTypeId);
        if (petTypeIdint > 0) {
            $.ajax({
                type: "GET",
                url: `/appointment/GetBreedsByPetTypeId?petTypeId=${petTypeId}`,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(response);
                    if (response != null) {
                        $('#ddl-breed').empty();
                        $.each(response, function (index, value) {
                            console.log("value is:", value);
                            console.log("index is :", index);
                            $('#ddl-breed').append($('<option>').text(value.name).attr('value', value.id));
                        });
                        $('#dvbreedtype').removeClass('hidden');
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
        }
        else {
            $('#dvbreedtype').addClass('hidden');
        }

    })
    $('#btncreate').click(function (event) {

        event.preventDefault();
        var petname = $('#petname').val();
        var pettypeid = $('#petTypeSelect').val();
        console.log('pettypeid', pettypeid);
        var petbreedtypeid = $('#ddl-breed').val();
        console.log('petbreedtypeid', petbreedtypeid);
        var doctorid = $('#ddl-doctor').val();
        console.log('doctorid', doctorid);
        var description = $('#description').val();
        var isformvalid = true;
        clearPreValidationErrors();
        if (!petname) {
            console.log('inside petname:', petname);
            $('.spnpetname').text('petname is Required!').addClass('field-validation-error').show();
            isformvalid = false;
        }
        if (!pettypeid || pettypeid== 0 ) {
            console.log('inside pettypeid:', pettypeid);
            $('.spnpetid').text('pettype is Required!').addClass('field-validation-error').show();
            isformvalid = false;
        }
        
        if (!doctorid) {
            console.log('inside doctorid:', doctorid);
            $('.spndoctorname').text('doctorname is Required!').addClass('field-validation-error').show();
            isformvalid = false;
        }
        if (!description) {
            console.log('inside description:', description);
            $('.spndescription').text('description is Required!').addClass('field-validation-error').show();
            isformvalid = false;
        }
        if (!petbreedtypeid) {
            console.log('inside petbreed:', petbreed);
            $('.spnpetbreed').text('petbreed is Required!').addClass('field-validation-error').show();
            isformvalid = false;
        }
        if (!isformvalid) {
            return false; 
        }        
        function clearPreValidationErrors() {
            $('.spnpetname, .spnpetid, .spnpetbreed, .spndoctorname, .spndescription').text('').removeClass('field-validation-error').hide();
        }


        var appointment = {
            petName: petname,
            petTypeId: pettypeid,
            breedId: petbreedtypeid,
            doctorid: doctorid,
            description: description

        };
        var appointmentJson = JSON.stringify(appointment);
        console.log(appointmentJson);

        $.ajax({
            type: "POST",
            url: "/Appointment/CreateAppointment",
            data: appointmentJson,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                window.location.href = 'Appointment/GetAllAppointments';
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
    function getAllDoctors() {
        $.ajax({
            type: "GET",
            url: "/Appointment/GetAllDoctors",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {                
                if (response != null) {
                    var firstitem = { id: 0, fullName: 'select a doctor' };
                    response.unshift(firstitem);
                    console.log(response);
                    $.each(response, function (index, doctor) {                        
                        console.log("doctor is:", doctor);
                        console.log("index is :", index);
                        $('#ddl-doctor').append($('<option>').text(doctor.fullName).attr('value', doctor.id));
                    });
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
    }
    $('.lnk-delete').on('click', function (event) {
        event.preventDefault();
        var appointmentId = $(this).data('id');
        $.ajax({
            type: 'Delete',
            url: '/Appointment/Delete',
            data: {
                deleteId: appointmentId
            },
            success: function (response) {
                if (response.success) {

                    console.log('Appointment deleted successfully');
                    setTimeout(function () {
                        location.reload();
                    }, 100);
                } else {

                    console.log('Error: ' + response.errorMessage);
                }
            },
            error: function (xhr, textStatus, errorThrown) {

                console.log('AJAX Error: ' + errorThrown);
            }
        });
    });
 });