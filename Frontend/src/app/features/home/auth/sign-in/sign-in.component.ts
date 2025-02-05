import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { SHARED_MODULES } from './../../../../shared/modules/shared.moudle';
import { Component } from '@angular/core';
import { SignInModel } from '../../../../shared/models/sign-in.model';
import { AuthService } from '../../../../shared/services/auth.service';
import { DialogService } from '../../../../shared/services/dialog.service';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [SHARED_MODULES],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.scss',
})
export class SignInComponent {
  private destroy$ = new Subject<void>();
  signInForm!: FormGroup;
  isUseEmail: boolean = true;
  buttonLabel: string = 'Use User ID';

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private dialogService: DialogService
  ) {}
  ngOnInit(): void {
    this.signInForm = this.fb.group(
      {
        userEmail: [''],
        userId: [''],
        password: ['', Validators.required],
      },
      { validators: this.oneFieldRequiredValidator() }
    );
  }
  oneFieldRequiredValidator() {
    return (group: AbstractControl): ValidationErrors | null => {
      const userEmail = group.get('userEmail');
      const userId = group.get('userId');

      const userEmailFilled = userEmail && userEmail.value;
      const userIdFilled = userId && userId.value;

      if (
        (userEmailFilled && userIdFilled) ||
        (!userEmailFilled && !userIdFilled)
      ) {
        return { oneFieldRequired: true }; // Error if both or neither are filled
      }

      return null; // Valid if one of the fields is filled
    };
  }

  setSignInMethod() {
    this.isUseEmail = !this.isUseEmail;
    this.signInForm.reset();
    this.buttonLabel = this.isUseEmail ? 'Use UserID' : 'User Email';
  }
  onSubmit() {
    if (this.signInForm.valid) {
      const formValues = this.signInForm.value;
      const signInRequest: SignInModel = {
        userId: formValues.userId,
        userEmail: formValues.userEmail,
        password: formValues.password,
      };

      this.authService.signin(signInRequest).subscribe({
        next: (response) => {
          this.dialogService
            .showMessage(response.message)
            .pipe(takeUntil(this.destroy$))
            .subscribe(() => {});
        },
      });
    }
  }
}
