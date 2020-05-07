import axios from "axios";

//deployment
const DEV_URL = 'https://localhost:5001/api/';
const PROD_URL = '#{ProdUrlToken}#';

const baseURL = !process.env.NODE_ENV || process.env.NODE_ENV === 'development' ? `${DEV_URL}` : `${PROD_URL}`;


export const api = axios.create({
  baseURL: baseURL,
  headers: {
    "Access-Control-Allow-Origin": "*"
  }
});

api.interceptors.request.use(config => {
  if (localStorage.getItem("authToken")) {
    config.headers.common["Authorization"] =
      "Bearer " + localStorage.getItem("authToken");
  } // todo logout
  return config;
});

api.interceptors.response.use(
  response => response.data,
  error => {
    var errorMessege: string | undefined = undefined;
    if (error.response) {
      if (typeof error.response.data === "string" && error.response.data.length > 0) {
        errorMessege = error.response.data
      } else if (error.response.data.errors !== undefined && error.response.data.errors[0] !== undefined) {
        errorMessege = error.response.data.errors[0].description
      }
      // big errors usually indicate it is not a custom message but C# error, we don't wanna return those 
      if (errorMessege === undefined || errorMessege.length > 100) {
        errorMessege = "There was an issue with your request!";
      }
    }
    return Promise.reject(new Error(errorMessege));
  })

