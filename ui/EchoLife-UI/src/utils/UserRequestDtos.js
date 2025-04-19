export class RegisterUserRequest {
    constructor(username, password, ensurePassword) {
        this.Username = username
        this.Password = password
        this.EnsurePassword = ensurePassword
    }
}

export class LoginRequest{
    constructor(username, password,rememberMe) {
        this.Username = username
        this.Password = password
        this.RememberMe = rememberMe
    }
}

export class UserInfoResponse{
    constructor(username) {
        this.Username = username
    }
}