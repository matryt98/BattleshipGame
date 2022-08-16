import axios from 'axios'

const axiosInstance = axios.create({
	baseURL: "https://localhost:5214/"
})

export default axiosInstance