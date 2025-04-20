export class LifePointRequest {
  constructor(content, hidden) {
    this.content = content
    this.hidden = hidden
  }
}

export class LifePointResponse {
  constructor(id, userId, content, createdAt, updatedAt) {
    this.id = id
    this.userId = userId
    this.content = content
    this.createdAt = createdAt
    this.updatedAt = updatedAt
  }
}

export class LifeSubSectionResponse {
  constructor(id, title, content, fatherId, lifeHistoryId, index, createdAt, updatedAt) {
    this.id = id
    this.title = title
    this.content = content
    this.fatherId = fatherId
    this.lifeHistoryId = lifeHistoryId
    this.index = index
    this.createdAt = createdAt
    this.updatedAt = updatedAt
  }
}
