import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { UserModel } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class UserStateService {
  private userSubject: BehaviorSubject<UserModel | null> =
    new BehaviorSubject<UserModel | null>(null);
  public user$: Observable<UserModel | null> = this.userSubject.asObservable();

  constructor() {}

  public setUserData(userData: UserModel) {
    localStorage.setItem('user', JSON.stringify(userData));
    this.userSubject.next(userData);
  }
}
