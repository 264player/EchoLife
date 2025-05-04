import axios from 'axios'
import { RegisterUserRequest, LoginRequest } from './UserRequestDtos'

/**
 *
 * @param {RegisterUserRequest} RegisterUserRequest
 * @returns
 */
export async function RegisterAsync(RegisterUserRequest) {
  return await axios
    .post('account/register', RegisterUserRequest, { withCredentials: true })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error.response.data }
    })
}

/**
 *
 * @param {LoginRequest} LoginRequest
 * @returns
 */
export async function LoginAsync(LoginRequest) {
  return await axios
    .post('account/login', LoginRequest, { withCredentials: true })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error.response.data }
    })
}

export async function GetUserInfoAsync() {
  return await axios
    .get('account/userinfo', { withCredentials: true })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

export async function LogOutAsync() {
  return await axios
    .get('account/logout', { withCredentials: true })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

export async function RefreshAsync() {
  return await axios
    .post('account/refresh', '{}', { withCredentials: true })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

export async function BecomeAReviewerAsync() {
  return await axios
    .post('account/become-reviewer', '{}', { withCredentials: true })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}
