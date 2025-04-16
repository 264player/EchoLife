export class RegisterUserRequest {
    constructor(username, password, ensurePassword) {
        this.Username = username
        this.Password = password
        this.EnsurePassword = ensurePassword
    }
}

export class LoginRequest{
    constructor(username, password) {
        this.Username = username
        this.Password = password
        this.RememberMe = true
    }
}

export class UserInfoResponse{
    constructor(username) {
        this.Username = username
    }
}