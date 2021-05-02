export default class ComponentLoader {
    constructor(sectionNode, components) {
        this.sectionNode = sectionNode;
        this.components = components;
    }

    addComponents() {
        const compSelector = this.components
            .map(c => c.name)
            .filter((value, index, self) => self.indexOf(value) === index)
            .join(', ')

        const compPlaceholders = this.sectionNode.querySelectorAll(compSelector)

        compPlaceholders.forEach(current => current.parentNode.replaceChild(
            this.components.find(comp => comp.name === current.tagName.toLowerCase()).node,
            current))
    }
}