import axios from 'axios'
import { QueryWillsRequest, QueryWillVersionsRequest, WillVersionRequest } from './WillRequestDtos'
import { WillRequest } from './WillRequestDtos'

/**
 *
 * @param {QueryWillsRequest} QueryWillsRequest
 */
export async function GetMyWillsAsync(QueryWillsRequest) {
  return await axios
    .get('wills', {
      withCredentials: true,
      params: { count: QueryWillsRequest.Count, cursorId: QueryWillsRequest.CusorId },
    })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

/**
 *
 * @param {string} willId
 */
export async function GetWillAsyn(willId) {
  return await axios
    .get(`wills/${willId}`, {
      withCredentials: true,
    })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

/**
 *
 * @param {WillRequest} WillRequest
 */
export async function CreateWillAsync(WillRequest) {
  return await axios
    .post('wills', WillRequest, { withCredentials: true })
    .then((response) => {
      console.log(response)
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

/**
 *
 * @param {string} WillId
 * @param {PutWillRequest} putWillRequest
 * @returns
 */
export async function UpdateWillAsync(WillId, putWillRequest) {
  return await axios
    .put(`wills/${WillId}`, putWillRequest, { withCredentials: true })
    .then((response) => {
      console.log(response)
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

/**
 *
 * @param {string} willId
 * @returns
 */
export async function DeleteWillAsync(willId) {
  return await axios
    .delete(`wills/${willId}`, { withCredentials: true })
    .then((response) => {
      console.log(response)
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

/**
 *
 * @param {string} willId
 * @param {QueryWillVersionsRequest} queryWillVersionsRequest
 * @returns
 */
export async function GetWillVersionsAsync(willId, queryWillVersionsRequest) {
  return await axios
    .get(`wills/${willId}/versions`, {
      withCredentials: true,
      params: { count: queryWillVersionsRequest.Count, cursorId: queryWillVersionsRequest.CusorId },
    })
    .then((response) => {
      console.log(response)
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

/**
 *
 * @param {string} willId
 * @param {WillVersionRequest} willVersionRequest
 * @param {boolean} isDraft
 * @returns
 */
export async function CreateWillVersionAsync(willId, willVersionRequest, isDraft) {
  return await axios
    .post(`wills/${willId}/versions`, willVersionRequest, {
      withCredentials: true,
      params: { isDraft: isDraft },
    })
    .then((response) => {
      console.log(response)
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

/**
 *
 * @param {string} versionId
 * @param {WillVersionRequest} willVersionRequest
 * @returns
 */
export async function UpdateWillVersionAsync(versionId, willVersionRequest) {
  return await axios
    .put(`wills/versions/${versionId}`, willVersionRequest, { withCredentials: true })
    .then((response) => {
      console.log(response)
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

/**
 *
 * @param {string} versionId
 * @returns
 */
export async function DeleteWillVersionAsync(versionId) {
  return await axios
    .delete(`wills/versions/${versionId}`, { withCredentials: true })
    .then((response) => {
      console.log(response)
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}
