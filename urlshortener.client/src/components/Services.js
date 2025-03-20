import axios from 'axios';

const API_URL = "https://localhost:7198/api";

export const login = async (username, password) => {
    const response = await axios.post(`${API_URL}/Auth/login`, {username, password});

    if (response.data.token) {
        localStorage.setItem('user', JSON.stringify(response.data))
    }

    return response.data;
};

export const logout = () => {
    localStorage.removeItem('user');
}

export const getCurrentUser = () => {
    return JSON.parse(localStorage.getItem('user'));
}

export const authHeader = () => {
    const user = getCurrentUser();
    
    if (user && user.token) {
      console.log(user.token)
      return { Authorization: `Bearer ${user.token}` };
    } else {
      return {};
    }
};

export const getAllUrls = async () => {
    const response = await axios.get(`${API_URL}/Url`);
    return response.data;
  };
  
export const getUrlById = async (id) => {
    const response = await axios.get(`${API_URL}/Url/${id}`, { headers: authHeader() });
    return response.data;
};
  
export const createUrl = async (url) => {
    const response = await axios.post(`${API_URL}/Url`, { "originalUrl": url }, { headers: authHeader() });
    return response.data;
};
  
export const deleteUrl = async (id) => {
    await axios.delete(`${API_URL}/Url/${id}`, { headers: authHeader() });
};


export const getAboutContent = async () => {
    const response = await axios.get(`${API_URL}/About`);
    return response.data;
  };
  
  export const updateAboutContent = async (content) => {
    const response = await axios.put(`${API_URL}/About`, { content }, { headers: authHeader() });
    return response.data;
  };