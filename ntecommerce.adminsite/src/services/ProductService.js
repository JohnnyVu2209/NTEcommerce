import httpService from '../httpService';

const API_URL = "/Product";

const getProductList = (PageNumber,PageSize,OrderBy,ProductName,CategoryName) => httpService.get(`${API_URL}/getListProducts`, {PageNumber,PageSize,OrderBy,ProductName,CategoryName});

const getProduct = (id) => httpService.get(`${API_URL}/getProduct/${id}`);

const createProduct = (data) => httpService.postForm(`${API_URL}/createProduct`,data);

const updateProduct = (id,data) => httpService.putForm(`${API_URL}/updateProduct/${id}`,data);

const productService = {
    getProductList,
    getProduct,
    createProduct,
    updateProduct
}

export default productService;
