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
    this.id = id
    this.userId = userId
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
