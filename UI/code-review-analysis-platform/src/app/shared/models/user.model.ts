export interface UserModel {
  id: string;
  username: string;
  email: string;
  role: 'admin' | 'user' | 'mods';
  createdAt: Date;
}
