import axios from 'axios'
import { FamilyTreeRequest } from './familyDtos'
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
