import { Role } from '../enums/role.enum';

export interface UserModel {
  firstName: string;
  middleName?: string;
  lastName?: string;
  dateOfBirth: string; // Date is usually represented as a string in JSON
  userId: string;
  userEmail?: string;
  role: Role;
  accessibleRoutes: string[];
}
