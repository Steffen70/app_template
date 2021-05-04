import LoginComponent from '../login-component/login-component.js';
import BaseComponent from '../base-component/base-component.js'

export default class LandingPage extends BaseComponent {
    components = [
        new LoginComponent()
    ]
}