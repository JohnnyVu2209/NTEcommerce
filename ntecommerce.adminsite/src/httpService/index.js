/* eslint-disable react-hooks/rules-of-hooks */
import axios from 'axios';

const endpoint = 'https://localhost:7012/api';

export const instance = axios.create({
    baseURL: endpoint,
    "Content-Type": "application/json",
});

const get = (url, params, headers) => instance.get(url, { params, headers });

const post = (url, data, params) => instance.post(url, data, { params });

const postForm = (url, data, params) => instance.postForm(url, data, { params });

const put = (url, data, params) => instance.put(url, data, { params });

const putForm = (url, data, params) => instance.putForm(url, data, { params });

const del = (url, params) => instance.delete(url, { params });

const httpService = {
    get,
    post,
    postForm,
    put,
    putForm,
    del
};



export default httpService;
