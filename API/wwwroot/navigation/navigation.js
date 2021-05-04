import BaseComponent from '../base-component/base-component.js'
import LoginService from '../_services/login-service.js'

export default class Navigation extends BaseComponent {
    static onPageChange = new CustomEvent('onPageChange')
    static onPageChangeLocked = false
    eventListenersAdded = false

    static changeRoute(page) {
        if (this.onPageChangeLocked)
            return
        this.onPageChangeLocked = true

        window.history.pushState({}, page, `${window.location.origin}${page}`)
        document.dispatchEvent(this.onPageChange)
    }

    constructor() {
        super()

        this.logout = async e => {
            e.preventDefault()

            const loginService = await LoginService.getInstanceAsync()
            loginService.logout()

            Navigation.changeRoute('/')
        }

        this.loadData = async () => {
            const loginService = await LoginService.getInstanceAsync()
            const username = loginService.user?.username

            this.node.querySelector('span#current-user').textContent = username
        }

        this.super_intiAsync = this.initAsync
        this.initAsync = async () => {
            await this.super_intiAsync()

            if (this.eventListenersAdded)
                return

            this.navLinks = this.node.querySelectorAll('.nav-link')

            this.navLinks.forEach(n => n.addEventListener('click', e => Navigation.changeRoute(e.target.getAttribute('data-page'))))

            window.addEventListener('popstate', () => {
                Navigation.onPageChangeLocked = true

                document.dispatchEvent(Navigation.onPageChange)
            })

            this.eventListenersAdded = true

            this.node.querySelector('form').addEventListener('submit', this.logout)
        }
    }
}