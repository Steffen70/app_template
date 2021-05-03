export default class User {
    constructor(name) {
        this.name = name

        this.sayHello = () => `Hello, my name is ${this.name}`
    }
}

export function sayHelloBack(user) {
    return `Hey ${user.name}, nice to meet you!`
}