import { Component } from '@angular/core';
import { SHARED_MODULES } from '../../../../shared/modules/shared.moudle';
import {
  FormBuilder,
  FormControlOptions,
  FormGroup,
  Validators,
} from '@angular/forms';
import { SignUpModel } from '../../../../shared/models/sign-up.model';
import { last } from 'rxjs';
import { AuthService } from '../../../../shared/services/auth.service';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [SHARED_MODULES],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.scss',
})
export class SignUpComponent {
  signUpForm!: FormGroup;
  constructor(private fb: FormBuilder, private authService: AuthService) {}
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

  passwordMatchValidator(data: FormGroup) {}

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
          console.log('Signup successful', response);
        },
        error: (error) => {
          console.error('Signup failed', error);
        },
      });
    }
  }
}
