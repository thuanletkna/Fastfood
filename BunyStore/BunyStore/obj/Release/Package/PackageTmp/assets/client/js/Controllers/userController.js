var user = {
    init: function () {

        user.loadProvince();

        user.registerEvent();
        
    },
    registerEvent: function () {
        $('#ddlProvince').off('change').on('change', function () {
            var value = $(this).val();
            if (value != '') {
                user.loadDistrict(value);
            }
            else {
                $('#ddlDistrict').html('');
            }
        });
        $('#ddlDistrict').off('change').on('change', function () {
            var value = $(this).val();
            if (value != '') {
                user.loadPrecinctID(value);
            }
            else {
                $('#ddlPrecinctID').html('');
            }
        });
        
        
    },
    
    loadProvince: function () {

        $.ajax({
            url: '/User/LoadProvince',
            type: "POST",
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">--Chọn tỉnh thành--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.Name + '">' + item.Name + '</option>'
                    });
                    $('#ddlProvince').html(html);
                }
            }
        })
    },
    loadDistrict: function (value) {
        $.ajax({
            url: '/User/LoadDistrict',
            type: "POST",
            data: { provinceID: value },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">--Chọn quận huyện--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.Name + '">' + item.Name + '</option>'
                    });
                    $('#ddlDistrict').html(html);
                }
            }
        })
    },
    loadPrecinctID: function (value) {
        $.ajax({
            url: '/User/LoadPrecinctID',
            type: "POST",
            data: { precientID: value },
            dataType: "json",
            success: function (response) {
                if (response.status == true) {
                    var html = '<option value="">--Chọn thị trấn/xã--</option>';
                    var data = response.data;
                    $.each(data, function (i, item) {
                        html += '<option value="' + item.Name + '">' + item.Name + '</option>'
                    });
                    $('#ddlPrecinctID').html(html);
                }
            }
        })
    }
}
user.init();
