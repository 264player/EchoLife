export class FamilyTreeRequest {
  constructor(name) {
    this.name = name
  }
}

export class FamilyTreeResponse {
  constructor(id, name, createdAt, createdUserId) {
    this.id = id
    this.name = name
    this.createdUserId = createdUserId
    this.createdAt = createdAt
  }
}

export class FamilyMemberRequest {
  constructor(
    familyId,
    DisplayName,
    gender,
    fatherId,
    motherId,
    spouseId,
    birthDate,
    DeathDate,
    generation,
    powerLevel,
  ) {
    this.familyId = familyId
    this.DisplayName = DisplayName
    this.gender = gender
    this.fatherId = fatherId
    this.motherId = motherId
    this.spouseId = spouseId
    this.birthDate = birthDate
    this.DeathDate = DeathDate
    this.generation = generation
    this.powerLevel = powerLevel
  }
}

export class FamilyMemberResponse {
  constructor(
    id,
    userId,
    familyId,
    displayName,
    gender,
    fatherId,
    motherId,
    spouseId,
    birthDate,
    deathDate,
    generation,
    powerLevel,
  ) {
    this.id = id
    this.userId = userId
    this.familyId = familyId
    this.displayName = displayName
    this.gender = gender
    this.fatherId = fatherId
    this.motherId = motherId
    this.spouseId = spouseId
    this.birthDate = birthDate
    this.deathDate = deathDate
    this.generation = generation
    this.powerLevel = powerLevel
  }
}

const Genders = ['unknown', 'male', 'female']
const ChineseGenderMap = {
  unknown: '未知',
  male: '男',
  female: '女',
}

const GenderArray = Object.entries(ChineseGenderMap).map(([key, value]) => ({
  key,
  value,
}))

export { Genders, ChineseGenderMap, GenderArray }

export class FamilySubsectionRequest {
  constructor(title, content, familyHistoryId, fatherId, index) {
    this.title = title
    this.content = content
    this.familyHistoryId = familyHistoryId
    this.fatherId = fatherId
    this.index = index
  }
}

export class FamilySubSectionResponse {
  constructor(id, title, content, fatherId, familyHistoryId, index, createdAt, updatedAt) {
    this.id = id
    this.title = title
    this.content = content
    this.fatherId = fatherId
    this.familyHistoryId = familyHistoryId
    this.index = index
    this.createdAt = createdAt
    this.updatedAt = updatedAt
  }
}

export class FamilyHistoryRequest {
  constructor(title, familyId) {
    this.title = title
    this.familyId = familyId
  }
}
