export interface IUser {
  id: number;
  userName: string;
  password: string;
  email: string;
  role: 'Administrator' | 'Doctor' | 'Patient';
  isBanned: boolean;
  isDeleted: boolean;
}