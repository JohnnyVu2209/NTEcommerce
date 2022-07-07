import axios from 'axios';

const endpoint = 'https://localhost:7012/api';

const instance = axios.create({
    baseURL:endpoint,
    "Content-Type": "application/json"
});

const get = (url, params) => instance.get(url, { params});

const post = (url, data, params) =>  instance.post(url, data, {params});

const postForm = (url, data, params) => instance.postForm(url, data, {params});

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
