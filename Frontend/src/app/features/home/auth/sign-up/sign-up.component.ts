import { Router } from '@angular/router';
import { DialogService } from './../../../../shared/services/dialog.service';
import { Component } from '@angular/core';
import { SHARED_MODULES } from '../../../../shared/modules/shared.moudle';
import {
  FormBuilder,
  FormControlOptions,
  FormGroup,
  Validators,
} from '@angular/forms';
import { SignUpModel } from '../../../../shared/models/sign-up.model';
import { last, Subject, takeUntil } from 'rxjs';
import { AuthService } from '../../../../shared/services/auth.service';
import { StatusEnum } from '../../../../shared/enums/status.enum';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [SHARED_MODULES],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.scss',
})
export class SignUpComponent {
  signUpForm!: FormGroup;
  private destroy$ = new Subject<void>();

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private dialogService: DialogService,
    private router: Router
  ) {}
  ngOnInit(): void {
    this.signUpForm = this.fb.group(
      {
        firstName: ['', [Validators.required]],
        middleName: [''],
        lastName: [''],
        dob: ['', [Validators.required]],
        userId: ['', [Validators.required, Validators.minLength(6)]],
        userEmail: ['', [Validators.required]],
        phone: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
        password: ['', [Validators.required, Validators.minLength(8)]],
        confirmPassword: ['', [Validators.required]],
      },
      { validator: this.passwordMatchValidator } as FormControlOptions
    );
  }

  passwordMatchValidator(group: FormGroup): { [key: string]: boolean } | null {
    const password = group.get('password')?.value;
    const confirmPassword = group.get('confirmPassword')?.value;

    return password && confirmPassword && password !== confirmPassword
      ? { passwordMismatch: true }
      : null;
  }

  onSubmit() {
    if (this.signUpForm.valid) {
      const formValues = this.signUpForm.value;
      const signUpForm: SignUpModel = {
        firstName: formValues.firstName,
        lastName: formValues.lastName,
        middleName: formValues.middleName,
        password: formValues.password,
        dateOfBirth: new Date(formValues.dob),
        userEmail: formValues.userEmail,
        userId: formValues.userId,
        phone: String(formValues.phone),
      };
      console.log(signUpForm);
      this.authService.signup(signUpForm).subscribe({
        next: (response) => {
          this.dialogService
            .showMessage(response.message)
            .pipe(takeUntil(this.destroy$))
            .subscribe(() => {
              if (response.status === StatusEnum.Success) {
                this.router.navigate(['/home/auth/sign-in']);
              } else if (response.status === StatusEnum.Error) {
                return;
              }
            });
        },
        error: (error) => {
          console.error('Signup failed', error);
        },
      });
    }
  }
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}
