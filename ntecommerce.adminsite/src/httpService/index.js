import axios from 'axios';

const endpoint = 'https://localhost:7012/api';

const instance = axios.create({
    baseURL:endpoint,
    "Content-Type": "application/json"
});

const get = (url, params) => instance.get(url, { params});

const post = (url, params, data) =>  instance.post(url, data, {params});

const postForm = (url, params, data) => instance.postForm(url, data, {params});

const put = (url, params, data) => instance.put(url, data, {params});

const putForm= (url, params, data) => instance.putForm(url, data, {params});

const httpService = {
    get,
    post,
    postForm,
    put,
    putForm
};

export default httpService;
