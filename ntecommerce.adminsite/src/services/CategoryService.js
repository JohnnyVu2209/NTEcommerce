import httpService from '../httpService';

const API_URL = "/Category";

const getListCategories = (PageNumber, PageSize, Name, OrderBy) => {
    const res = httpService.get(`${API_URL}/getList`,{PageNumber, PageSize, Name, OrderBy});
    return res;
};

const createCategory =  (data) => httpService.post(`${API_URL}/createCategory`,data);

const getCategoryDetail =  (id) => httpService.get(`${API_URL}/getCategory/${id}`);

const categoryService = {
    getListCategories,
    createCategory,
    getCategoryDetail
};

export default categoryService;