import { instance } from './index';

const setup = (store) => {
    instance.interceptors.request.use(
        (config) => {
            const tokenInfo = JSON.parse(localStorage.getItem("tokenInfo"));
            if (tokenInfo)
                config.headers.Authorization = `Bearer ${tokenInfo.token}`;

            return config;
        }, (error) => Promise.reject(error)
    )
}

export default setup;