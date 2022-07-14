import httpService from "../httpService";

const API_URL = "/Authentication";

const login = (username, password) => httpService.post(`${API_URL}/login`,{username,password});

const getUser = (username, token) => httpService.get(`${API_URL}/GetUserInfo/${username}`,null, {Authorization: `Bearer ${token}` });

const authService = {
    login,
    getUser
}


export default authService;