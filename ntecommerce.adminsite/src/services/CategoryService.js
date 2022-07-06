import httpService from '../httpService';

const API_URL = "/Category";

const getListCategories = (PageNumber, PageSize, Name, OrderBy) => {
    const res = httpService.get(`${API_URL}/getList`,{PageNumber, PageSize, Name, OrderBy});
    return res;
};

const service = {
    getListCategories
};

export default service;