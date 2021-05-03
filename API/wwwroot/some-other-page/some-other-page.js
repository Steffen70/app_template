import SayCiao from '../say-ciao/say-ciao.js';
import BaseComponent from '../base-component/base-component.js'

export default class SomeOtherPage extends BaseComponent{
    components = [
        new SayCiao()
    ]
}