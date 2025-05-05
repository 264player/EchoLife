import axios from 'axios'

export async function UploadAsync(url, file) {
  return await axios
    .put(url, file, {
      headers: {
        'Content-Type': file.type,
      },
      baseURL: '',
    })
    .then((response) => {
      console.log(response)
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

export async function ListObjectsAsync() {
  return await axios
    .get(`objects`, {
      withCredentials: true,
    })
    .then((response) => {
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}

export async function DeleteObjectAsync(file) {
  return await axios
    .delete(`objects`, { withCredentials: true, params: { fileName: file } })
    .then((response) => {
      console.log(response)
      return { result: true, response: response.data }
    })
    .catch((error) => {
      return { result: false, response: error }
    })
}
