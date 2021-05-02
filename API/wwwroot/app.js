import BaseComponent from './base-component/base-component.js'
import Navigation from './navigation/navigation.js'
import Router from './router/router.js'

class App extends BaseComponent {
    navigation = new Navigation()
    router = new Router()

    components = [
        this.navigation,
        this.router
    ]

    constructor() {
        super()

        document.addEventListener('onPageChange', () => this.onPageChange())
    }

    onPageChange() {
        let routerNode = this.node.querySelector('.router')

        if (!routerNode)
            return

        let currentPage = this.router.routes.find(r => r.component.name == routerNode.getAttribute('data-current-page'))?.path
        console.log(currentPage, window.location.pathname.split('/')[1], currentPage == window.location.pathname.split('/')[1])

        if (currentPage == window.location.pathname.split('/')[1]) {
            this.navigation.onPageChangeLocked = false
            return
        }

        routerNode.remove()
        this.node.append(document.createElement('router'))

        this.node = this.router.node = undefined

        this.initAsync()
            .then(this.navigation.onPageChangeLocked = false)
    }

    async createComponentDivAsync() {
        return document.body
    }
}

const app = new App()
app.initAsync()