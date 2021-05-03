import User, { sayHelloBack } from '../_models/user.js'
import BaseComponent from '../base-component/base-component.js'
import SayCiao from '../say-ciao/say-ciao.js'

export default class Greeter extends BaseComponent {
    components = [new SayCiao()]

    constructor(username) {
        super()
        this.user = new User(username)

        this.super_intiAsync = this.initAsync
        this.initAsync = async () => {
            await this.super_intiAsync()

            this.greeterNode = this.node.querySelector('.greeter')
            this.responseNode = this.node.querySelector('.response')

            this.greeterNode.textContent = this.user.sayHello()
            this.responseNode.textContent = sayHelloBack(this.user)
        }
    }
}