import axios from 'axios'
import { FamilyHistoryRequest, FamilyTreeRequest } from './familyDtos'
import { PageInfo } from '@/utils/WillRequestDtos'

/**
 *
 * @param {FamilyTreeRequest} familyTreeRequest
 */
export async function CreateFamilyAsync(familyTreeRequest) {
  return await axios
    .post('families', familyTreeRequest, { withCredentials: true })
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
 * @param {PageInfo} pageInfo
 */
export async function GetMyFamiliesAsync(pageInfo) {
  return await axios
    .get('families', {
      withCredentials: true,
      params: { count: pageInfo.count, cursorId: pageInfo.cursorId },
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
 * @param {string} familyId
 */
export async function GetFamilyAsyn(familyId) {
  return await axios
    .get(`families/${familyId}`, {
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
 * @param {string} familyId
 * @param {FamilyTreeRequest} familyTreeRequest
 * @returns
 */
export async function UpdateFamilyAsync(familyId, familyTreeRequest) {
  return await axios
    .put(`families/${familyId}`, familyTreeRequest, { withCredentials: true })
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
 * @param {string} familyId
 * @returns
 */
export async function DeleteFamilyAsync(familyId) {
  return await axios
    .delete(`families/${familyId}`, { withCredentials: true })
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
 * @param {FamilyTreeRequest} familyTreeRequest
 */
export async function CreateMemberAsync(familyTreeRequest) {
  return await axios
    .post('families/members', familyTreeRequest, { withCredentials: true })
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
 * @param {string} familyId
 */
export async function GetFamiliyMembersAsync(familyId) {
  return await axios
    .get(`families/${familyId}/members`, {
      withCredentials: true,
    })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

export async function GetFamiliyMemberAsync(memberId) {
  return await axios
    .get(`families/members/${memberId}`, {
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
 * @param {string} familyId
 * @param {FamilyTreeRequest} familyTreeRequest
 * @returns
 */
export async function UpdateMemberAsync(familyId, familyTreeRequest) {
  return await axios
    .put(`families/members`, familyTreeRequest, { withCredentials: true })
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
 * @param {string} memberId
 * @returns
 */
export async function DeleteMemberAsync(memberId) {
  return await axios
    .delete(`families/members/${memberId}`, { withCredentials: true })
    .then((response) => {
      console.log(response)
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

// family history
/**
 *
 * @param {FamilyHistoryRequest} familyHistoryRequest
 * @returns
 */
export async function CreateFamilyHistoryAsync(familyHistoryRequest) {
  return await axios
    .post('family/history', familyHistoryRequest, { withCredentials: true })
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
 * @param {PageInfo} queryLifeHistoryRequest
 */
export async function GetMyFamilyHistoriesAsync(queryLifeHistoryRequest) {
  return await axios
    .get(`family/history`, {
      withCredentials: true,
      params: {
        familyId: queryLifeHistoryRequest.familyId,
        count: queryLifeHistoryRequest.count,
        cursorId: queryLifeHistoryRequest.cursorId,
      },
    })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

export async function GetMyFamilyHistoryAsync(historyId) {
  return await axios
    .get(`family/history/${historyId}`, {
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
 * @param {FamilyHistoryRequest} familyHistoryRequest
 * @returns
 */
export async function UpdataFamilyHistoryAsync(historyId, familyHistoryRequest) {
  return await axios
    .put(`family/history/${historyId}`, familyHistoryRequest, { withCredentials: true })
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
export async function DeleteFamilyHistoryAsync(historyId) {
  return await axios
    .delete(`family/history/${historyId}`, { withCredentials: true })
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
export async function CreateFamilySubSectionAsync(lifeSubSectionRequest) {
  return await axios
    .post('family/history/subsection', lifeSubSectionRequest, { withCredentials: true })
    .then((response) => {
      console.log(response)
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

export async function GetMyFamilySubsectionsAsync(historyId) {
  return await axios
    .get(`family/history/${historyId}/subsections`, {
      withCredentials: true,
    })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

export async function GetMyFamilySubSecionAsync(sectionId) {
  return await axios
    .get(`family/history/subsections/${sectionId}`, {
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
    .get(`family/history/subsections/${sectionId}/polish`, {
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
export async function UpdataFamilySubSectionAsync(sectionId, lifeSubSectionRequest) {
  return await axios
    .put(`family/history/subsections/${sectionId}`, lifeSubSectionRequest, {
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
 * @returns
 */
export async function DeleteFamilySubSectionAsync(sectionId) {
  return await axios
    .delete(`family/history/subsections/${sectionId}`, { withCredentials: true })
    .then((response) => {
      console.log(response)
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}
