// JavaScript source code
var URL = 'ws://127.0.0.1:8181';

var chatClient = null;

function connect() {
    chatClient = new WebSocket(URL);
    chatClient.onmessage = function (event) {
        resultInput = document.getElementById("Sonuc");
        try {
            var jsonObj = JSON.parse(event.data);
            try {
                resultInput.value = jsonObj.sonuc;
            } catch (e) {
                document.getElementById(" messageLabel").value = JSON.stringify(jsonObj);
            }
        } catch (e) {
            document.getElementById("messageLabel").innerHTML = JSON.stringify(event.data);
        }
    };
}

function disconnect() {
    chatClient.close();
}

function topla() {
    document.getElementById("Sonuc").value = "";
    sendMessage("Topla");
}
function Cikar() {
    document.getElementById("Sonuc").value = "";
    sendMessage("Cikar");
}
function Carp() {
    document.getElementById("Sonuc").value = "";
    sendMessage("Carp");
}
function Bol() {
    document.getElementById("Sonuc").value = "";
    sendMessage("Bol");
}


function sendMessage(operation) {
    var sayi1 = document.getElementById("sayi1").value.trim();
    var sayi2 = document.getElementById("sayi2").value.trim();
    if (sayi1 === "") {
        alert("Lütfen Sayi1 için değer giriniz!");
    } else {
        if (sayi2 === "")
            alert("Lütfen sayi2 için değer giriniz!");
        else {
            var jsonObj = { "sayi1": sayi1, "sayi2": sayi2, "operation": operation };
            try {
                chatClient.send(JSON.stringify(jsonObj));
            } catch (e) {
                alert(e)
            }
        }
    }
}
