import { PageInfo } from '@/utils/WillRequestDtos'
import axios from 'axios'
import { LifeHistoryRequest, LifeSubsectionRequest } from './LifeDtos'

/**
 *
 * @param {LifePointRequest} lifePointRequest
 */
export async function CreateLifePointAsync(lifePointRequest) {
  return await axios
    .post('life/points', lifePointRequest, { withCredentials: true })
    .then((response) => {
      console.log(response)
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

export async function InviteOtherUserToPointAsync(pointId, userIdList) {
  return await axios
    .post(`life/points/${pointId}/join`, userIdList, { withCredentials: true })
    .then((response) => {
      console.log(response)
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

export async function LeavePointAsync(pointId) {
  return await axios
    .delete(`life/points/${pointId}/leave`, { withCredentials: true })
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
 * @param {PageInfo} queryLifePointsRequest
 */
export async function GetMyLifePointsAsync(userId, queryLifePointsRequest) {
  return await axios
    .get(`${userId}/life/points`, {
      withCredentials: true,
      params: { count: queryLifePointsRequest.count, cursorId: queryLifePointsRequest.cursorId },
    })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

export async function GetLifePointsAsync(queryLifePointsRequest) {
  return await axios
    .get(`life/points`, {
      withCredentials: true,
      params: { count: queryLifePointsRequest.count, cursorId: queryLifePointsRequest.cursorId },
    })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

export async function UpdataLifePointAsync(pointId, lifePointRequest) {
  return await axios
    .put(`life/points/${pointId}`, lifePointRequest, { withCredentials: true })
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
 * @param {string} pointId
 * @returns
 */
export async function DeleteLifePointAsync(pointId) {
  return await axios
    .delete(`life/points/${pointId}`, { withCredentials: true })
    .then((response) => {
      console.log(response)
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

// personal history
/**
 *
 * @param {LifeHistoryRequest} lifeHistoryRequest
 * @returns
 */
export async function CreateLifeHistoryAsync(lifeHistoryRequest) {
  return await axios
    .post('life/history', lifeHistoryRequest, { withCredentials: true })
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
 * @param {PageInfo} queryLifePointsRequest
 */
export async function GetMyLifeHistoriesAsync(queryLifeHistoryRequest) {
  return await axios
    .get(`life/history`, {
      withCredentials: true,
      params: { count: queryLifeHistoryRequest.count, cursorId: queryLifeHistoryRequest.cursorId },
    })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

export async function GetMyLifeHistoryAsync(historyId) {
  return await axios
    .get(`life/history/${historyId}`, {
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
 * @param {string} historyId
 * @param {LifeHistoryRequest} lifeHistoryRequest
 * @returns
 */
export async function UpdataLifeHistoryAsync(historyId, lifeHistoryRequest) {
  return await axios
    .put(`life/history/${historyId}`, lifeHistoryRequest, { withCredentials: true })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

/**
 *
 * @param {string} historyId
 * @returns
 */
export async function DeleteLifeHistoryAsync(historyId) {
  return await axios
    .delete(`life/history/${historyId}`, { withCredentials: true })
    .then((response) => {
      console.log(response)
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

// subSection
/**
 *
 * @param {LifeSubsectionRequest} lifeSubSetionRequest
 * @returns
 */
export async function CreateLifeSubSectionAsync(lifeSubSectionRequest) {
  return await axios
    .post('life/history/subsection', lifeSubSectionRequest, { withCredentials: true })
    .then((response) => {
      console.log(response)
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

export async function GetMyLifeSubsectionsAsync(historyId) {
  return await axios
    .get(`life/history/${historyId}/subsections`, {
      withCredentials: true,
    })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

export async function GetMyLifeSubSecionAsync(sectionId) {
  return await axios
    .get(`life/history/subsections/${sectionId}`, {
      withCredentials: true,
    })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

export async function GetSubSectionPolishAsync(sectionId) {
  return await axios
    .get(`life/history/subsections/${sectionId}/polish`, {
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
 * @param {string} sectionId
 * @param {LifeSubsectionRequest} lifeSubSectionRequest
 * @returns
 */
export async function UpdataLifeSubSectionAsync(sectionId, lifeSubSectionRequest) {
  return await axios
    .put(`life/history/subsections/${sectionId}`, lifeSubSectionRequest, { withCredentials: true })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

/**
 *
 * @param {string} sectionId
 * @returns
 */
export async function DeleteLifeSubSectionAsync(sectionId) {
  return await axios
    .delete(`life/history/subsections/${sectionId}`, { withCredentials: true })
    .then((response) => {
      console.log(response)
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}
