import { StatusEnum } from '../enums/status.enum';

export interface ApiResponse<T> {
  status: StatusEnum;
  message: string;
  data: T | null;
}
