import Greeter from '../greeter/greeter.js'
import SayCiao from '../say-ciao/say-ciao.js';
import BaseComponent from '../base-component/base-component.js'

export default class HomePage extends BaseComponent {
    components = [
        new Greeter('Michi'),
        new Greeter('Jonas'),
        new SayCiao()
    ]
}