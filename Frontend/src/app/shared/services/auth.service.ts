import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SignUpModel } from '../models/sign-up.model';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from '../../environments/environment';
import { ApiResponse } from '../models/api-response.model';
import { SignInModel } from '../models/sign-in.model';
import { UserModel } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  apiUrl: string = environment.apiUrl;
  constructor(private http: HttpClient) {}

  // Method to handle signup
  signup(request: SignUpModel): Observable<ApiResponse<SignUpModel>> {
    return this.http
      .post<ApiResponse<SignUpModel>>(this.apiUrl + '/Auth/signup', request, {
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
        }),
      })
      .pipe(catchError(this.handleError));
  }

  signin(request: SignInModel): Observable<ApiResponse<UserModel>> {
    return this.http
      .post<ApiResponse<UserModel>>(this.apiUrl + '/Auth/signin', request, {
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
        }),
      })
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    const errorMessage = error.error?.message || 'An unknown error occurred';
    return throwError(() => errorMessage);
  }
}
