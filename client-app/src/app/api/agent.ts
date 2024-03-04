import axios, { AxiosResponse } from "axios";
import { Activity } from "../interfaces/activity";

axios.defaults.baseURL = "http://localhost/5000/api";

const sleep = (delay: number) => {
  return new Promise((resolve) => {
    setTimeout(resolve, delay);
  });
};

axios.interceptors.response.use(async (response) => {
  try {
    await sleep(1000);
    return response;
  } catch (error) {
    console.log(error);
    return await Promise.reject(error);
  }
});

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const request = {
  get: <T>(url: string) => axios.get<T>(url).then(responseBody),
  post: <T>(url: string, values: object) =>
    axios.post<T>(url, values).then(responseBody),
  put: (url: string, values: object) =>
    axios.put(url, values).then(responseBody),
  delete: <T>(url: string) => axios.delete<T>(url).then(responseBody),
};

const Activities = {
  list: () => request.get<Activity[]>("/activities"),
  details: (id: string) => request.get<Activity>(`/activities/${id}`),
  create: (activity: Activity) => request.post<void>("/activities", activity),
  update: (activity: Activity) =>
    request.put(`/activities/${activity.id}`, activity),
  delete: (id: string) => request.delete<void>(`/activities/${id}`),
};

const agent = {
  Activities,
};

export default agent;
