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
  constructor(id, testaordId, contentId, name) {
    this.id = id
    this.testaordId = testaordId
    this.contentId = contentId
    this.name = name
  }
}

export class WillRequest {
  constructor(name) {
    this.Name = name
  }
}

export class PutWillRequest {
  constructor(name, versionId) {
    this.Name = name
    this.VersionId = versionId
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
