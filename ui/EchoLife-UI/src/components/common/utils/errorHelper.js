export function FormatErrorInfo(errors) {
  // 检查输入是否为数组
  if (!Array.isArray(errors)) {
    throw new TypeError('Input must be an array of error objects.')
  }

  for (const error of errors) {
    if (
      typeof error !== 'object' ||
      error === null ||
      !('code' in error) ||
      !('description' in error)
    ) {
      throw new TypeError("Each error object must contain 'code' and 'description' properties.")
    }
  }

  // 格式化错误信息
  const formattedErrors = errors.map((error) => {
    return `Error Code: ${error.code}, Description: ${error.description}`
  })

  return formattedErrors.join('\n')
}
