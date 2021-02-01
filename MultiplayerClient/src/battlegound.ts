import * as signalR from "@microsoft/signalr"
import Player from "./models/player";

export default class Menu extends Phaser.Scene {
    player: Phaser.GameObjects.Image;
    cursors: Phaser.Types.Input.Keyboard.CursorKeys = null;
    connection: signalR.HubConnection;
    playerId: number;

    updateFrekvens: number = 30;
    movementSpeed: number = 1;

    objects: Player[];
    bodies = new Map<number, Phaser.Types.Physics.Arcade.SpriteWithDynamicBody>();

    constructor() {
        super('demo');

        this.connection = new signalR.HubConnectionBuilder()
            .withUrl("https://localhost:5001/gamehub")
            .build();;

        this.setupResivers();

        this.connection.start()
            .then(() => this.connection.invoke("Join", "player", 0, 0))
            .then(() => console.log(`Send Join`))
            .catch(err => console.error(err));
    }

    dataToArray = (data: Object) => {
        const arr: Player[] = [];
        for (const [key, value] of Object.entries(data)) {
            arr.push(value)
        }
        return arr;
    }

    setupResivers() {
        this.connection.on("PlayerPositions", (data) => {
            // update the objects
            this.objects = data;

            this.objects.forEach(o => {
                const body = this.bodies.get(o.id);

                if (body) {
                    // body.setX(o.position.x);
                    // body.setY(o.position.y);
                    this.tweens.add({
                        targets: body,
                        x: o.position.x,
                        y: o.position.y,
                        ease: 'Power0',
                        duration: this.updateFrekvens,
                        // onStart: function () { console.log('onStart'); console.log(arguments); },
                        // onComplete: function () { console.log('onComplete'); console.log(arguments); },
                        // onYoyo: function () { console.log('onYoyo'); console.log(arguments); },
                        // onRepeat: function () { console.log('onRepeat'); console.log(arguments); },
                    });
                }
            })
        });

        this.connection.on("PlayerJoined", (data) => {
            // create bodies of the objects
            console.log("PlayerJoined", data);
            this.createBody(data);
        });

        this.connection.on("ConfirmedID", data => {
            console.log("Player ID: ", data);
            this.playerId = data;
        })
    }

    createBody = (players: Player[]) => {
        players.forEach(o => {
            // add if it does not exist.
            if (!this.bodies.has(o.id) && o.id !== this.playerId) {
                this.bodies.set(o.id, this.physics.add.sprite(o.position.x, o.position.y, 'pipe'))
            }
        })
    }

    sendPlayerInfo(id: number, pos: Position): void {
        this.connection
            .invoke("SendMessage", id, pos.x, pos.y)
            .catch(function (err) {
                return console.error(err.toString());
            });
    }


    preload() {

    }

    create() {
        this.cursors = this.input.keyboard.createCursorKeys();
        this.player = this.add.image(400, 70, 'logo');
    }

    update() {
        this.inputControl();
    }

    inputControl = () => {
        if (this.input.keyboard.checkDown(this.cursors.left, this.updateFrekvens)) {
            this.player.x -= this.movementSpeed;
            this.sendPlayerInfo(this.playerId, { x: this.player.x, y: this.player.y } as Position);
        }
        else if (this.input.keyboard.checkDown(this.cursors.right, this.updateFrekvens)) {
            this.player.x += this.movementSpeed;
            this.sendPlayerInfo(this.playerId, { x: this.player.x, y: this.player.y } as Position);
        }

        if (this.input.keyboard.checkDown(this.cursors.up, this.updateFrekvens)) {
            this.player.y -= this.movementSpeed;
            this.sendPlayerInfo(this.playerId, { x: this.player.x, y: this.player.y } as Position);
        }
        else if (this.input.keyboard.checkDown(this.cursors.down, this.updateFrekvens)) {
            this.player.y += this.movementSpeed;
            this.sendPlayerInfo(this.playerId, { x: this.player.x, y: this.player.y } as Position);
        }
    }

}


