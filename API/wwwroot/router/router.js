import BaseComponent from '../base-component/base-component.js'
import LandingPage from '../landing-page/landing-page.js'
import NotFound from '../not-found/not-found.js'
import TestPage from '../test-page/test-page.js'

export default class Router extends BaseComponent {
    routes = [
        {
            path: '',
            component: new LandingPage()
        },
        {
            path: 'test',
            component: new TestPage()
        }
    ]
    constructor() {
        super()

        this.createComponentDivAsync = async () => {
            const page = window.location.pathname.split('/')[1]

            let component = this.routes.find(r => r.path === page)?.component

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
}