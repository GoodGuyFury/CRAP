import {
  HttpClient,
  HttpErrorResponse,
  HttpHeaders,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SignUpModel } from '../models/sign-up.model';
import { catchError, Observable, throwError } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  apiUrl: string = environment.apiUrl;
  constructor(private http: HttpClient) {}

  // Method to handle signup
  signup(request: SignUpModel): Observable<SignUpModel> {
    return this.http
      .post<SignUpModel>(this.apiUrl + '/Auth/signup', request, {
        headers: new HttpHeaders({
          'Content-Type': 'application/json',
        }),
      })
      .pipe(
        catchError(this.handleError) // Error handling (optional)
      );
  }

  // Optional error handling method
  private handleError(error: HttpErrorResponse) {
    const errorMessage = error.error?.message || 'An unknown error occurred';
    return throwError(() => errorMessage);
  }
}
