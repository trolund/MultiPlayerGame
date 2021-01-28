import * as signalR from "@microsoft/signalr"

export default class Communicator {

    connection: signalR.HubConnection;

    constructor() {
        this.connection = new signalR.HubConnectionBuilder()
            .withUrl("/gamehub")
            .build();;

        this.connection.start()
            .then(() => this.connection.invoke("Join", "player", 0, 0));

        this.setupResivers();
    }


    setupResivers() {
        this.connection.on("playerPositions", data => {
            console.log(data);
        });
    }

    sendEvent(id: number, pos: Position) {
        this.connection.invoke("SendMessage", id, pos).catch(function (err) {
            return console.error(err.toString());
        });
    }







}

