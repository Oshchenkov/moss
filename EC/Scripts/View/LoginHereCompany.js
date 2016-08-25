﻿$(document).ready(function () {
    $('#submit').on('click', function () {
        if (($('input[name=login]').val() != "") && ($('input[name=password]').val() != "")) {

            LoginHere($('input[name=login]').val(), $('input[name=password]').val());
        }
    });

    function LoginHere(login, password) {

      //  console.log(login);
       // console.log(password);


        $.ajax({
            method: "POST",
            url: "/Login/Login",
            data: { login: login, password: password }
        }).done(function (data) {//data from server
          //  console.log(data);

            if (data != '') {
                var str = window.location.href;
                str = str.toLocaleLowerCase().split("login");

                window.location.href = str[0] + data;
            }
            else
                $('input[name=password]').val() = '';
            console.log(data);// alert(data);
        }).fail(function (error) {
            console.log(error);
        });
    }
});