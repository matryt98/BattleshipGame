import axios from 'axios'

const axiosInstance = axios.create({
	baseURL: "http://localhost:5214/"
})

export default axiosInstance