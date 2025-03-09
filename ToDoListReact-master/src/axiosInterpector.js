
import axios from "axios";

const api = axios.create({
    baseURL: "https://localhost:5198",
   
});

api.interceptors.response.use(
    (response) => {
        console.log("work");
        
        return response;
    },
    (err) => {
        console.error("שגיאה!!!");

        
        return Promise.reject(err);
    }
);

export default api;