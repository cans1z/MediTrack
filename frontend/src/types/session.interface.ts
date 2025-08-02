import { IUser } from "./user.interface";

export interface ISession {
  id: number;
  token: string;
  isActive: boolean;
  authDate: Date | string;

  userId: number;
  user: IUser;
}