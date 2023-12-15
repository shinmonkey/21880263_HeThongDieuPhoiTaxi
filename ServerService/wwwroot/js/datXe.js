"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

connection.On < Boolean, String > ("ReceiveOrder", (check, message) => {
    console.log(message);
    if (check) {
        alert("Đã nhận đơn");
    } else {
        alert("Đơn gửi thất bại")
    }
});

connection.start().then(function () {
    console.log("Hub Connected");
}).catch(function (err) {
    return console.error(err.toString());
});
document.getElementById("send").addEventListener("click", function (event) {
    var order = {
        DxNgayGio: document.getElementById("datetime").value,
        KhTen: document.getElementById("KhTen").value,
        KhPhone: document.getElementById("KhPhone").value,
        DxDiadiemdon: document.getElementById("address").value,
        LxId: document.getElementById("Lxid").value,
        DxGpsLat: document.getElementById("latitude").value,
        DxGpsLon: document.getElementById("longitude").value,
    }
    var message = JSON.stringify(order);
    // Gửi tin nhắn đến SignalR Hub
    connection.invoke("SendOrder", message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});
