import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import LoginForm from 'src/app/shared/models/LoginForm';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  // public loginData: LoginForm = new LoginForm();
  public loginForm: any;

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit() {
    // this.loginForm = new LoginForm();

    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }

  submitForm(): void {
    const emailField = this.loginForm.get("email");
    if(emailField.hasError("required")){
      emailField.setErrors({ invalid: true, message: "Email is required." });
    }
    
    if (this.loginForm.valid) {
      console.log('Form data:', this.loginForm.value);
    }
  }
}
