export interface UserModel {
  userId: string;
  userName: string;
  email: string;
  role: 'admin' | 'user' | 'mods';
  createdAt: Date;
}
