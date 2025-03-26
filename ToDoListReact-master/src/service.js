import axios from 'axios';
import api from './axiosInterpector.js';

// const apiUrl = "http://localhost:5198"
axios.defaults.baseURL = api;
export default {
  
  getTasks: async () => {
    
    //  axios.interceptors.response.use(async function (response) {
    const result = await axios.get(`${api}/items`)    
    return result.data;
    // return response;

  },
  // function (error) {
  //   // Any status codes that falls outside the range of 2xx cause this function to trigger
  //   // Do something with response error
  //   return Promise.reject(error);
  // });
  // },
  addTask: async(name)=>{
    console.log('addTask:',name)
   await axios.post(`${api}/items`,{name});
    return {};
  },

  setCompleted: async(id, isComplete)=>{
    
    console.log('setCompleted', {id, isComplete})
    await axios.put(`${api}/items/${id}`,{isComplete}) 
    return{};
  },

  deleteTask:async(id)=>{
    console.log('deleteTask')
   await axios.delete(`${api}/items/${id}`);
  }
};
