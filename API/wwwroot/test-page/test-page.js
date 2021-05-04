import TestComponent from '../test-component/test-component.js';
import BaseComponent from '../base-component/base-component.js'

export default class TestPage extends BaseComponent{
    components = [
        new TestComponent()
    ]
}