export default class ComponentLoader {
    constructor(sectionNode, components) {
        this.sectionNode = sectionNode;
        this.components = components;

        this.compSelector = this.components
            .map(c => c.name)
            .filter((value, index, self) => self.indexOf(value) === index)

        this.addStylesheets = () => {
            this.compSelector
                .filter(name => this.sectionNode.querySelector(`.${name}, ${name}`))
                .map(name => this.components.find(c => c.name === name))
                .filter(c => !document.head.querySelector(`link[data-component="${c.name}"]`) && c.stylesheetLink)
                .forEach(c => document.head.append(c.stylesheetLink))
        }

        this.addComponents = () => {
            const compPlaceholders = this.sectionNode.querySelectorAll(this.compSelector.join(', '))

            compPlaceholders.forEach(current => current.parentNode.replaceChild(
                this.components.find(c => c.name === current.tagName.toLowerCase()).node,
                current))
        }
    }
}