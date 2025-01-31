export interface SignUpModel {
  firstName: string;
  lastName: string | null;
  middleName: string | null;
  password: string;
  dob: Date;
  email: string;
  userId: string;
}
