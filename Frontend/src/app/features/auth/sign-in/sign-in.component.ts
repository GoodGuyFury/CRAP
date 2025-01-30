import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SHARED_MODULES } from './../../../shared/modules/shared.moudle';
import { Component } from '@angular/core';

@Component({
  selector: 'app-sign-in',
  standalone: true,
  imports: [SHARED_MODULES],
  templateUrl: './sign-in.component.html',
  styleUrl: './sign-in.component.scss'
})
export class SignInComponent {
signInForm !: FormGroup

constructor(private fb: FormBuilder){

}
ngOnInit(): void {
  this.signInForm = this.fb.group({
  userEmail:[''],
  userId:[''],
  password:['', (Validators.required)]
})
}
onSubmit(){

}
}
