import sygnalR from "signalr";


class Chat
{

    constructor(url)
    {
        this._connection = new sygnalR.HubConnectionBuilder()
            .withUrl(url)
            .build();

            return;
    }
    
    async StartConnection()
    {
        return await this._connection.start();
    }

    async StopConnection()
    {
        return await this._connection.stop();
    }

    async SendTextMessage(message)
    {
        return await this._connection.invoke("AddNewMessage");
    }
    AddResiveMessageHandler(handler)
    {
        _connection.on("Receive", handler);
    }
    
}

class MessageDTO
{
    constructor(Type, Content)
    {
        this._type = Type;
        this._content = Content;
    }

    ToString()
    {
        return JSON.stringify({
            "messageType" : this._type,
            "message" : this._content
        });
    }

}

const FileContentId = 1;
const TextContentId = 0;

let SendMessageElement = document.querySelector("Chat__MessageInpute__TextSender");
let chatTextInput = document.querySelector(".Chat__MessageInpute__Message");
let SendFileInput = SendMessageElement.querySelector(".FileInput");

const currentChatIdHardCode = "7f8f0942-7139-45c0-8bb5-2496edec0997";

let chat = new Chat("/chat");
let ChatWasInitialised = false;

(
    async function()
    {
        await chatInit();
        
        SendMessageElement.disabled = !ChatWasInitialised;

        if(ChatWasInitialised === false)
        {
            return;
        }

        SendMessageElement.addEventListener("click", (evnt) => onSendTextMessageButtonClick);
        FileInput.addEventListener("change", async (ex) =>
        {
            let fileInfo = ex.dataTransfer.files[0];
            await LoadFile(fileInfo)

        })
    }
)();



async function chatInit()
{
    try {
        await chat.StartConnection()
        ChatWasInitialised = true;
    }
    catch (error)
    {
    }
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

function createMessageComponent(messageContent, isMyCreation)
{
    let content;

    if(messageContent.messageType == FileContentId)
    {
        content = createFileContentComponent(messageContent.message)
    }
    else if(messageContent.messageType == TextContentId)
    {
        content = createTextContentComponent(messageContent.message);
    }
    else
    {
        throw new Error("invalid content type");
    }

    


}

function createTextContentComponent(messageContent)
{
    let component = document.createElement("span");
    component.classList.add("UserMessage");
    component.textContent = messageContent;

    return component;
}
function createFileContentComponent(messageContent)
{
    let downloadImage = document.createElement("img");
    downloadImage.src = "./download.png";

    let downloadButton = document.createElement("button");
    downloadButton.classList.add("FileDownload");
    downloadButton.id = "testFile.png";
    downloadButton.children = downloadImage;

    downloadButton.addEventListener("click", async () =>
    {
        let file = await DownloadFile(downloadButton.id);
        console.log("messageContent");
        console.log(await file.text());
    });

    let fileNameComponent = document.createElement("span");
    fileNameComponent.classList.add("UserMessage");
    fileNameComponent.textContent = messageContent;

    let resultComponent = document.createElement("span");
    resultComponent.classList.add("UserMessage", "FileMessage");

    resultComponent.children = [fileNameComponent, downloadButton];

    return resultComponent;
}

async function DownloadFile(fileName)
{
    let url = document.baseURI + "/file/" + "get/" + fileName;

    let response = await fetch(url);
    
    if(response.status != "200")
    {
        throw new Error(response.text);
    }

    let file = await response.blob();
    
    console.log(await file.text());

    return file;

}

async function LoadFile(fileInfo)
{
    
}

async function SendFileToServer(fileInfo)
{
    let url = document.baseURI + "/save" + "/" + fileInfo.name;

    let result = await fetch(url,
        {
            method: 'POST',
            headers: {
                'Content-Type': 'multipart/form-data'
            },
            body: JSON.stringify(file.blob())
        }
    );

    if(result.ok == false)
    {
        throw new Error();
    }

    return;
}