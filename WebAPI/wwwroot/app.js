import { Chat } from "./Api"

let SendMessageElement = document.getElementById("messageSender");
let chatTextInput = SendMessageElement.querySelector("textInput");

let chat = new Chat("/chat");
let ChatWasInitialised = false;


async function chatInit()
{
    try {
        await chat.StartConnection()
        ChatWasInitialised = true;
    }
    catch (error)
    {
    }
    SendMessageElement.disabled = !ChatWasInitialised;
}


async function onSendTextMessageButtonClick() 
{
    try {
        let message = chatTextInput.value;

        if (message === "") return;

        chatTextInput.value = "";

        await chat.SendTextMessage(message);
    }
    catch (e) {
        
    }
}

function addNewMessageHandler(message)
{
    let messageElement = document.createElement("p");
    console.log(message);
    messageElement.textContent = `${message.sendDateTime}/         ${message.senderName} ${message.message}`;
    document.getElementById("Chat").appendChild(messageElement);
}

function createMessageComponent()
{
    
}

document.getElementById("messageSender").addEventListener("click", onSendButtonClick);