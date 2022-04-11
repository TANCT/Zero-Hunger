$(document).ready(function () {

    $("#receiver").on('click',function () {
        var item = $(this).val();
        $.ajax({
            type: 'GET',
            url: "/api/deliveryitem/GetPhoneAdrs",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { 'id': item },
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                $('#receiverphone').html(data.userPhone);
                $('#receiveradrs1').html(data.userAdrs1);
                $('#receiveradrs2').html(data.userAdrs2);
            }
        });
    });

    $("#deliveryitem").on('click',function (){
        var dryItem = $(this).val();
        $.ajax({
            type: 'GET',
            url: "/api/deliveryitem/SetMaxQuantity",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("XSRF-TOKEN",
                    $('input:hidden[name="__RequestVerificationToken"]').val());
            },
            data: { "id": dryItem },
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                $("#quantity").val(data);
                $("#quantity").attr({ "max": data });
            }
        });
    });
});