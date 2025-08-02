import axios from '../lib/axios';
const RouteName = "users";

export interface RegisterUserDataDto {
  userName: string;
  password: string;
  email: string;
  role: 'Administrator' | 'Doctor' | 'Patient';
}

export interface UpdateUserDataDto {
  userName: string;
  password: string;
  email: string;
  role: 'Administrator' | 'Doctor' | 'Patient';
  isBanned: boolean;
}

export const registerUser = async (data: RegisterUserDataDto) => {
  return await axios.post(`/${RouteName}/register`, data);
}

export const deleteUser = async (userId: number) => {
  return await axios.post(`/${RouteName}/register`);
}

export const updateUser = async (userId: number, data: UpdateUserDataDto) => {
  return await axios.post(`/${RouteName}/register`, data);
}

export const listAllUsers = async () => {
  return await axios.get(`/${RouteName}/list`);
}