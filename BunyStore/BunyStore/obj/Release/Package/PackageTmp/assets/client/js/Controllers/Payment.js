$(document).ready(function () {
    $("#access").click(function () {
        var name = $('#txtName').text();
        var mobile = $('#txtMobile').text();
        var address = $('#txtAddress').text();
        var email = $('#txtEmail').text();

        var model = { name: name, phone: mobile, address: address, email: email }

        if ($("#paypal").is(":checked") == true) {
            $.ajax({
                url: 'http://localhost:62443/Payment/Control',
                type: 'POST',
                data: model,
                dataType: "jsonp",
            });
        }
    });

    function myFunction(click_id) {
        if(click_id == "paypal")
        {
            var li = document.createElement("li");
            var List = document.getElementById("myList");
            List.replaceChild(li, List.childNodes[1]);
                    
            var temp = List.childNodes[1]
            var a = document.createElement("a");
            var textnode = document.createTextNode("thanh toan paypal");
            a.setAttribute("id", "test");
            a.setAttribute("href", "http://localhost:62443/Payment/PaymentWithPaypal");
            a.setAttribute("class", "btn btn-success form-control");
            a.setAttribute("style", "margin-top:10px");
            a.appendChild(textnode);
            temp.appendChild(a);
        }
        else if(click_id == "momo")
        {
            var li = document.createElement("li");
            var List = document.getElementById("myList");
            List.replaceChild(li, List.childNodes[1]);
                    
            var temp = List.childNodes[1]
            var a = document.createElement("a");
            var textnode = document.createTextNode("thanh toan momo");
            a.setAttribute("id", "test");
            a.setAttribute("href", "http://localhost:62443/Payment/PaymentWithMoMo");
            a.setAttribute("class", "btn btn-success form-control");
            a.setAttribute("style", "margin-top:10px");
            a.appendChild(textnode);
            temp.appendChild(a);
        }
        else
        {
            var li = document.createElement("li");
            var List = document.getElementById("myList");
            List.replaceChild(li, List.childNodes[1]);
                    
            var temp = List.childNodes[1]
            var a = document.createElement("a");
            var textnode = document.createTextNode("thanh toan khi giao hang");
            a.setAttribute("id", "test");
            a.setAttribute("href", "http://localhost:62443/Payment/PaymentWithPaypal");
            a.setAttribute("class", "btn btn-success form-control");
            a.setAttribute("style", "margin-top:10px");
            a.appendChild(textnode);
            temp.appendChild(a);
        }
    }
});




