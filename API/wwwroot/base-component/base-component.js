import ComponentLoader from '../_helpers/component-loader.js'

export default class BaseComponent {
    components = []
    constructor() {
        this.name = pascalToKebabCase(this.constructor.name);
    }

    async createComponentDivAsync() {
        const div = document.createElement('div')

        div.classList.add(this.name)

        await fetch(`./${this.name}/${this.name}.html`)
            .then(d => d.text())
            .then(html => div.innerHTML = html)

        return div
    }

    async initAsync() {
        if (this.node)
            return

        const result = this.createComponentDivAsync()

        if (result instanceof Promise)
            this.node = await result
        else
            this.node = result

        if (this.components.length < 1)
            return

        const componentLoader = new ComponentLoader(this.node, this.components)

        let asyncFunctions = this.components
            .map(c => c.initAsync())

        await Promise.all(asyncFunctions)

        componentLoader.addComponents()
    }
}

function pascalToKebabCase(str) {
    return str[0].toLowerCase() + str.slice(1, str.length).replace(/[A-Z]/g, letter => `-${letter.toLowerCase()}`)
}