import BaseComponent from '../base-component/base-component.js'

export default class Navigation extends BaseComponent {
    onPageChange = new CustomEvent('onPageChange')
    onPageChangeLocked = false
    eventListenersAdded = false
    constructor() {
        super()
    }

    async initAsync() {
        await super.initAsync()

        if (this.eventListenersAdded)
            return

        this.navLinks = this.node.querySelectorAll('.nav-link')

        this.navLinks.forEach(n => n.addEventListener('click', e => this.changeRoute(e.target.getAttribute('data-page'))))

        window.addEventListener('popstate', () => {
            this.onPageChangeLocked = true

            document.dispatchEvent(this.onPageChange)
        })

        this.eventListenersAdded = true
    }

    changeRoute(page) {
        if (this.onPageChangeLocked)
            return
        this.onPageChangeLocked = true

        window.history.pushState({}, page, `${window.location.origin}${page}`)
        document.dispatchEvent(this.onPageChange)
    }
}