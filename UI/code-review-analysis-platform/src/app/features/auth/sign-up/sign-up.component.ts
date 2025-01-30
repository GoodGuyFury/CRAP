import { Component } from '@angular/core';
import { SHARED_MODULES } from '../../../shared/modules/shared.moudle';
import {
  FormBuilder,
  FormControlOptions,
  FormGroup,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [SHARED_MODULES],
  templateUrl: './sign-up.component.html',
  styleUrl: './sign-up.component.scss',
})
export class SignUpComponent {
  signUpForm!: FormGroup;
  constructor(private fb: FormBuilder) {}
  ngOnInit(): void {
    this.signUpForm = this.fb.group(
      {
        firstName: ['', [Validators.required]],
        middleName: [''],
        lastName: [''],
        dob: ['', [Validators.required]],
        userId: ['', [Validators.required, Validators.minLength(6)]],
        password: ['', [Validators.required, Validators.minLength(8)]],
        confirmPassword: ['', [Validators.required]],
      },
      { validator: this.passwordMatchValidator } as FormControlOptions
    );
  }

  passwordMatchValidator(data: FormGroup) {}
}
