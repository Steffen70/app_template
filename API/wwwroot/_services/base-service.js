import { pascalToKebabCase } from '../base-component/base-component.js'

export default class BaseService {
    static async getInstanceAsync() {
        const className = this.prototype.constructor.name

        if (!this._instance) {
            console.log('create instance of', className)
            const module = await import(`./${pascalToKebabCase(className)}.js`)
            this._instance = eval(`new module.default()`)
        }

        return this._instance
    }
}