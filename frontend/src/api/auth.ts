import axios from '../lib/axios';
const RouteName = "auth";

export interface LoginDataDto {
  userName: string;
  password: string;
  email: string;
}

export const login = async (data: LoginDataDto) => {
  return await axios.post(`/${RouteName}/login`, data);
}

export const fetchProfile = async () => {
  return await axios.get(`/${RouteName}/fetch`);
}