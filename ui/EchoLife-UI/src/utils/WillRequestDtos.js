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
    this.Name = name
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

const willTypes = [
  'SelfWritten',
  'WrittenByOthers',
  'Audio',
  'Video',
  'Living',
  'Notarized',
  'Trust',
]
export { willTypes }
