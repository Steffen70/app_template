import BaseComponent from '../base-component/base-component.js'
import Navigation from '../navigation/navigation.js'
import LoginService from '../_services/login-service.js'

export default class LoginComponent extends BaseComponent {
    constructor() {
        super()

        this.login = async e => {
            e.preventDefault()

            const loginService = await LoginService.getInstanceAsync()

            const username = this.formNode.querySelector('input[name="username"]')?.value
            const password = this.formNode.querySelector('input[name="password"]')?.value

            if (await loginService.login(username, password)) {
                this.formNode.reset()
                Navigation.changeRoute('/test')
            }
        }

        this.super_intiAsync = this.initAsync
        this.initAsync = async () => {
            await this.super_intiAsync()

            this.formNode = this.node.querySelector('form')
            this.formNode.addEventListener('submit', this.login)
        }
    }
}