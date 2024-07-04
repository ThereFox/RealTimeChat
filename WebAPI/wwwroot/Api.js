import sygnalR from "https://cdnjs.cloudflare.com/ajax/libs/microsoft-signalr/6.0.1/signalr.js";

class Chat
{
    constructor(url)
    {
        this._connection = new sygnalR.HubConnectionBuilder()
            .withUrl("/chat")
            .build();
    }
    
    async function StartConnection()
    {
        await this._connection.start();
    }
    
    async function SendTextMessage(message)
    {
        await this._connection.invoke("AddNewMessage");
    }
    async function addResiveMessageHandler(handler)
    {
        
    }
    
}



//Обработка события прихода нового сообщения
hubConnection.on("Receive", (message) => {
    let messageElement = document.createElement("p");
    console.log(message);
    messageElement.textContent = `${message.sendDateTime}/         ${message.senderName} ${message.message}`;
    document.getElementById("Chat").appendChild(messageElement);
});

//Запуск соединения и активация кнопки
hubConnection.start()
    .then(function () {
        document.getElementById("messageSender").disabled = false;
    })
    .catch(function (err) {
        return console.error(err.toString());
    });