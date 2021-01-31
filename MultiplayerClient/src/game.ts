import 'phaser';
import Menu from './battlegound';

export default class Demo extends Phaser.Scene {

    constructor() {
        super({ key: 'Menu' })
    }

    create() {
        this.add.text(0, 0, 'Click to add new Scene');

        this.input.once('pointerdown', function () {
            this.scene.add('MainScene', Menu, true)
        }, this);
    }

}

const config = {
    type: Phaser.AUTO,
    backgroundColor: '#125555',
    width: 800,
    height: 600,
    scene: Demo,
    fps: { min: 20 },
    physics: {
        default: 'arcade',
        arcade: { debug: true },
    },
};

const game = new Phaser.Game(config);
