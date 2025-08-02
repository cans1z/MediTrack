import { IMedication } from "./medication.interface";
import { IUser } from "./user.interface";

export interface IPrescription {
  id: number;
  dosage: number;
  frequency: number;
  startDate: Date | string;
  period: number;
  isFlexible: boolean;
  comment: string;

  doctorId: number;
  doctor: IUser;

  patientId: number;
  patient: IUser;

  medicationId: number;
  medication: IMedication;
}