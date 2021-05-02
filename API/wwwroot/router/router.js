import BaseComponent from '../base-component/base-component.js'
import HomePage from '../home-page/home-page.js'
import NotFound from '../not-found/not-found.js'
import SomeOtherPage from '../some-other-page/some-other-page.js'

export default class Router extends BaseComponent {
    routes = [
        {
            path: 'home-page',
            component: new HomePage()
        },
        {
            path: 'some-other-page',
            component: new SomeOtherPage()
        }
    ]
    constructor() {
        super()
    }

    async createComponentDivAsync() {
        const page = window.location.pathname.split('/')[1]

        const component = this.routes.find(r => r.path === page)?.component

        if (!component)
            component = new NotFound()

        this.components = [component]

        const div = document.createElement('div')
        div.classList.add(this.name)

        div.setAttribute('data-current-page', component.name)

        div.append(document.createElement(component.name))

        return div
    }
}