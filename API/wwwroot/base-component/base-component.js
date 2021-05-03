import ComponentLoader from '../_helpers/component-loader.js'

export default class BaseComponent {
    components = []
    constructor() {
        this.name = pascalToKebabCase(this.constructor.name);

        this.createComponentDivAsync = async () => {
            const div = document.createElement('div')

            div.classList.add(this.name)

            await fetch(`./${this.name}/${this.name}.html`)
                .then(d => d.text())
                .then(html => div.innerHTML = html)

            return div
        }

        this.generateStylesheetLink = () => {
            const url = `./${this.name}/${this.name}.css`

            if (!urlExists(url))
                return undefined

            const link = document.createElement('link')
            link.rel = 'stylesheet';
            link.type = 'text/css';
            link.href = url

            link.setAttribute('data-component', this.name)

            return link
        }

        this.initAsync = async () => {
            if (!this.node) {
                const result = this.createComponentDivAsync()

                if (result instanceof Promise)
                    this.node = await result
                else
                    this.node = result
            }

            if (!this.stylesheetLink)
                this.stylesheetLink = this.generateStylesheetLink()

            if (this.components.length >= 1) {
                let asyncFunctions = this.components
                    .map(c => c.initAsync())

                await Promise.all(asyncFunctions)
            }

            const componentLoader = new ComponentLoader(this.node, this.components)

            componentLoader.addStylesheets()

            if (this.components.length >= 1) {
                componentLoader.addComponents()
            }
        }
    }
}

export function pascalToKebabCase(str) {
    return str[0].toLowerCase() + str.slice(1, str.length).replace(/[A-Z]/g, letter => `-${letter.toLowerCase()}`)
}

export function urlExists(url) {
    var http = new XMLHttpRequest()
    http.open('HEAD', url, false)
    http.send()

    return http.status != 404
}