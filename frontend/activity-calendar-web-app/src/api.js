import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5181/api',
    withCredentials: true,
});

api.interceptors.response.use(
    (response) => response,
    async (error) => {
        const originalRequest = error.config;
        if (error.response.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;
            try {
                await api.post('/Authorization/RefreshToken', {}, { withCredentials: true });
                return api(originalRequest);
            } catch (refreshError) {
                console.error('Failed to refresh token:', refreshError);
            }
        }
        return Promise.reject(error);
    }
);

export default api;
