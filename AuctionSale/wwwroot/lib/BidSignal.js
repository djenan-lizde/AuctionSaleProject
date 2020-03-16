$(document).ready(function () {
    let connection = new signalR.HubConnectionBuilder().withUrl("/notifications").build();

    //ovo recieveMessage je funk koju server poziva
    //kako reaguje klijentska strana
    connection.on("BidUpdate", (message, newPrice, id) => {
        document.getElementById("currentPrice-" + id.toString()).innerHTML = newPrice;
        alert(message);
    });

    //dohvatiti values propertya
    connection.on("NewItemMessage", (message) => {
        alert(message);
    });

    connection.start();

    $("#AddItemForm").on("submit", function () {
        var message = "Please refresh the browser because new auction item has been added. :D"

        connection.invoke("NewItem", message).catch((err) => {
            return console.error(err.toString());
        });
    })

    //bid part
    Bid = function (id) {

        var currentPrice = document.getElementById("currentPrice-" + id).innerHTML;
        var dollars50 = document.getElementById("50dollars-" + id).value;
        var dollars100 = document.getElementById("100dollars-" + id).value;

        if (document.getElementById("50dollars-" + id).checked) {
            document.getElementById("currentPrice-" + id).innerHTML = (parseInt(currentPrice, 10) + parseInt(dollars50, 10)).toString();
            var newPrice = parseInt(currentPrice, 10) + parseInt(dollars50, 10);
            ajaxCallPost(id, newPrice);
            //napraviti da pise koji user i za sta je biddao
            var message = "Neko je biddao";
            connection.invoke("Send", message, newPrice, id).catch((err) => {
                return console.error(err.toString());
            });
        }

        if (document.getElementById("100dollars-" + id).checked) {
            document.getElementById("currentPrice-" + id).innerHTML = (parseInt(currentPrice, 10) + parseInt(dollars100, 10)).toString();
            var newPrice = parseInt(currentPrice, 10) + parseInt(dollars100, 10);
            ajaxCallPost(id, newPrice);
            var message = "Neko je biddao";
            connection.invoke("Send", message, newPrice, id).catch((err) => {
                return console.error(err.toString());
            });
        }
    }
    function ajaxCallPost(id, newPrice) {
        $.ajax({
            type: "POST",
            url: "/Item/UserBid?id=" + id + "&newPrice=" + newPrice,
            data: { id: id, newPrice: newPrice },
            success: function (data) {
                $('#myModal').modal('show');
            }
        });
    }

});
setInterval(function () {
    var x = document.getElementsByClassName("timer");
    for (var i = 0; i < x.length; i++) {
        x[i].value -= 1;
        if (x[i].value == 0) {
            DeclareWinner(x[i].id);
        }
    }
}, 1000);

function DeclareWinner(id) {
    window.location.href = '@Url.Action("DeclareWinner", "Item")/' + id;
}