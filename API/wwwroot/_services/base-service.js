import { pascalToKebabCase } from '../base-component/base-component.js'

export default class BaseService {
    static async getInstanceAsync() {
        const className = this.prototype.constructor.name

        if (!this._instancePromise)
            this._instancePromise = new Promise(async resolve => {
                const module = await import(`./${pascalToKebabCase(className)}.js`)
                resolve(eval(`new module.default()`))
            })

        return await this._instancePromise
    }
}