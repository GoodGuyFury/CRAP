import { AuthComponent } from './features/home/auth/auth.component';
import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { DashboardComponent } from './features/home/dashboard/dashboard.component';
import { SignInComponent } from './features/home/auth/sign-in/sign-in.component';
import { SignUpComponent } from './features/home/auth/sign-up/sign-up.component';

export const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent },
      {
        path: 'auth',
        component: AuthComponent,
        children: [
          { path: '', redirectTo: 'sign-in', pathMatch: 'full' },
          {
            path: 'sign-in',
            component: SignInComponent,
            data: { isExistingUser: true },
          },
          {
            path: 'sign-up',
            component: SignUpComponent,
            data: { isExistingUser: false },
          },
        ],
      },
    ],
  },
  { path: '', redirectTo: '/home', pathMatch: 'full' },
  { path: '**', redirectTo: '/home', pathMatch: 'full' },
];
