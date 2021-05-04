import BaseService from "./base-service.js";

export default class LoginService extends BaseService {
    constructor() {
        super()

        this.setUser = (user, login = true) => {
            if (login)
                localStorage.setItem('user', JSON.stringify(user))

            this.user = user
        }

        // this.getDecodedToken = token => JSON.parse(atob(token.split('.')[1]))

        this.login = async (username, password) => {
            console.log('login', username, password)

            this.setUser({ username })

            return true
        }

        this.logout = () => {
            console.log('logout', this.user?.username)

            this.user = undefined
            localStorage.removeItem('user')
        }

        const user = JSON.parse(localStorage.getItem('user'))
        if (user)
            this.setUser(user, false)
    }
}