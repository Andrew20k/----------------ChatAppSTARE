(async function () {
    const username = document.getElementById("user").value;
    const userMessage = document.getElementById("userMessage");
    const buttonSend = document.getElementById("buttonSend");
    const userMessages = document.getElementById("userMessages");

    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/chat")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    async function start() {
        try {
            await connection.start();
            console.log("SignalR Connected.");
            await connection.invoke("GetChatHistory");
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    }

    $(buttonSend).click(async () => {
        console.log("Co� zosta�o wypisane w konsoli!");
        const message = $(userMessage).val();

        if (!message || message === '') {
            return;
        }

        try {
            await connection.invoke("SendMessage", {
                message,
                username
            });
            $(userMessage).val('');
        } catch (err) {
            console.error(err);
        }
    });

    connection.onclose(async () => {
        await start();
    });

    connection.on("ReceiveMessage", (payload) => {
        const { username, message, formattedCreatedOn } = payload;
        const li = document.createElement("li");
        li.innerHTML = `<strong>${formattedCreatedOn}, ${username}:</strong>${message}`;
        userMessages.prepend(li);
    });

    connection.on("ReceiveMessageHistory", (messages) => {
        userMessages.innerHTML = '';
        messages.reverse().forEach(payload => {
            const { username, message, formattedCreatedOn } = payload;
            const li = document.createElement("li");
            li.innerHTML = `<strong>${formattedCreatedOn}, ${username}:</strong>${message}`;
            userMessages.appendChild(li);
        });
    });

    start();
})();
