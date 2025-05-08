export class QueryWillsRequest {
  /**
   *
   * @param {int} count
   * @param {string?} cursorId
   */
  constructor(count, cursorId) {
    this.Count = count
    this.CusorId = cursorId
  }
}

export class WillResponse {
  constructor(id, testaordId, contentId, name, willType) {
    this.id = id
    this.testaordId = testaordId
    this.contentId = contentId
    this.name = name
    this.willType = willType
  }
}

export class WillRequest {
  constructor(name) {
    this.name = name
  }
}

export class PutWillRequest {
  constructor(name, versionId, willType) {
    this.Name = name
    this.VersionId = versionId
    this.willType = willType
  }
}

export class QueryWillVersionsRequest {
  /**
   *
   * @param {int} count
   * @param {string?} cursorId
   */
  constructor(count, cursorId) {
    this.Count = count
    this.CusorId = cursorId
  }
}

export class WillVersionRequest {
  constructor(willType, value) {
    this.WillType = willType
    this.Value = value
  }
}

export class WillVersionResponse {
  constructor(id, willId, value, willType, createdAt, updateAt) {
    this.id = id
    this.willId = willId
    this.value = value
    this.willType = willType
    this.createdAt = createdAt
    this.updateAt = updateAt
  }
}

export class PageInfo {
  constructor(count, cursorId) {
    this.count = count
    this.cursorId = cursorId
  }
}

export class ReviewResponse {
  constructor(id, reviewerId, status, reviewedAt, createdAt, comments, willVersion) {
    this.id = id
    this.reviewerId = reviewerId
    this.status = status
    this.reviewedAt = reviewedAt
    this.createdAt = createdAt
    this.comments = comments
    this.willVersion = willVersion
  }
}

export class ReviewRequest {
  constructor(comment, status) {
    this.comment = comment
    this.status = status
  }
}

// willTypes
const willTypes = [
  'selfWritten',
  'writtenByOthers',
  'audio',
  'video',
  'living',
  'notarized',
  'trust',
]

const willTypeMap = {
  selfWritten: '自书遗嘱',
  writtenByOthers: '代书遗嘱',
  audio: '录音遗嘱',
  video: '录像遗嘱',
  living: '口头遗嘱',
  notarized: '公证遗嘱',
  trust: '信托遗嘱',
}
const willTypeArray = Object.entries(willTypeMap).map(([key, value]) => ({
  key,
  value,
}))

const willTypeMapLowerCase = Object.fromEntries(
  Object.entries(willTypeMap).map(([key, value]) => [key.toLowerCase(), value]),
)

export function GetChineseWillType(willType) {
  if (!willType || typeof willType !== 'string') {
    throw new Error('Invalid input: willType must be a string')
  }

  const lowerCaseWillType = willType.toLowerCase()

  const chineseName = willTypeMapLowerCase[lowerCaseWillType]

  if (!chineseName) {
    throw new Error(`Invalid willType: ${willType}`)
  }
}

// willReviewTypes
const reviewStatus = ['pendding', 'inProgress', 'approved', 'rejected']

const reviewStatusMap = {
  pendding: '未受理',
  inProgress: '审核中',
  approved: '完成',
  rejected: '未完成',
}

export { willTypes, willTypeArray, willTypeMap, reviewStatusMap }
