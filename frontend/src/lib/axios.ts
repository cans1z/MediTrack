import axios, {InternalAxiosRequestConfig} from 'axios';

const axiosInstance = axios.create({
  baseURL: 'http://localhost:5000/api', // API URL
  // baseURL: 'http://localhost:8000/api', // API URL
  headers: {
    'Content-Type': 'application/json',
  },
});

// interceptor для установки токена авторизации
axiosInstance.interceptors.request.use((config: InternalAxiosRequestConfig) => {
  const token = localStorage.getItem('token');

  if (token) {
    // передаем токен в query
    config.params = {
      ...config.params, // Сохраняем существующие параметры
      token: token     // Добавляем токен
    };

    // передаем токен в хедере
    config.headers.authorization = token;
  }

  return config;
});

export default axiosInstance;