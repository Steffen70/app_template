import BaseComponent from '../base-component/base-component.js'

export default class TestComponent extends BaseComponent {
    constructor() {
        super()

        this.super_intiAsync = this.initAsync
        this.initAsync = async () => {
            await this.super_intiAsync()

            // get members from api/members/test-filter
        }
    }
}