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

        this.onPageChange = () => {
            let routerNode = this.node.querySelector('.router')

            if (!routerNode)
                return

            let currentPage = this.router.routes.find(r => r.component.name == routerNode.getAttribute('data-current-page'))?.path
            if (currentPage == window.location.pathname.split('/')[1]) {
                Navigation.onPageChangeLocked = false
                return
            }

            routerNode.remove()
            document.head.querySelectorAll(`link[data-component]`)?.forEach(l => l.remove())

            this.node.append(document.createElement('router'))

            this.node = this.router.node = undefined

            this.initAsync()
                .then(Navigation.onPageChangeLocked = false)
        }

        document.addEventListener('onPageChange', this.onPageChange)

        this.createComponentDivAsync = async () => {
            return document.body
        }
    }
}

const app = new App()
app.initAsync()