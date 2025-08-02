import { IPrescription } from "./prescription.interface";

export interface IIntakeRecord {
  id: number;
  dateTaken: Date | string;
  status: 'Taken' | 'Missed';
  comment: string;

  prescriptionId: number;
  prescription: IPrescription;
}