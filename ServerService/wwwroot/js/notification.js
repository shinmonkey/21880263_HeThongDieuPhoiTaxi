"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

//Disable the send button until connection is established.
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (username, password) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you 
    // should be aware of possible script injection concerns.
    li.textContent = `${username} says ${password}`;
});
connection.on("ReceiveJWT", function (username, jwt) {
    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    // We can assign user-supplied strings to an element's textContent because it
    // is not interpreted as markup. If you're assigning in any other way, you
    // should be aware of possible script injection concerns.
    console.log(window.location.port);
    li.textContent = `${username} says ${jwt}`;
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    console.log("Hub Connected");
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var username = document.getElementById("userInput").value;
    var password = document.getElementById("messageInput").value;
    connection.invoke("Login", username, password).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});