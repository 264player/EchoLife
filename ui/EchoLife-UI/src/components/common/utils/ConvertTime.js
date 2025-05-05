export function ConvertUTCToBeijingTime(utcTimeStr) {
  if (utcTimeStr == null || utcTimeStr == '') {
    return ''
  }
  // 创建一个 Date 对象
  const utcDate = new Date(utcTimeStr)

  // 转换为东八区（北京时间）
  const beijingOffset = 8 * 60 // 东八区偏移分钟数
  const localTime = new Date(utcDate.getTime() + beijingOffset * 60 * 1000)

  // 格式化为中文时间字符串
  const year = localTime.getFullYear()
  const month = String(localTime.getMonth() + 1).padStart(2, '0')
  const day = String(localTime.getDate()).padStart(2, '0')
  const hour = String(localTime.getHours()).padStart(2, '0')
  const minute = String(localTime.getMinutes()).padStart(2, '0')
  const second = String(localTime.getSeconds()).padStart(2, '0')

  return `${year}年${month}月${day}日 ${hour}时${minute}分${second}秒`
}
