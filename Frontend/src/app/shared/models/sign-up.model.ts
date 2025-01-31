export interface SignUpModel {
  firstName: string;
  lastName: string | null;
  middleName: string | null;
  password: string;
  dateOfBirth: Date;
  userEmail: string;
  userId: string;
}
