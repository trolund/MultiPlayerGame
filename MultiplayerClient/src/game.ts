import 'phaser';
import Communicator from './communicator';

export default class Demo extends Phaser.Scene {

    com = new Communicator();

    constructor() {
        super('demo');

    }

    preload() {
        this.load.glsl('stars', 'assets/starfields.glsl.js');
    }

    create() {
        this.add.shader('Plasma', 0, 412, 800, 172).setOrigin(0);

        this.add.image(400, 300, 'libs');

        const logo = this.add.image(400, 70, 'logo');
    }
}

const config = {
    type: Phaser.AUTO,
    backgroundColor: '#125555',
    width: 800,
    height: 600,
    scene: Demo
};

const game = new Phaser.Game(config);
