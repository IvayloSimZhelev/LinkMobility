import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { ConfirmationService } from 'primeng/api';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  carDialog: boolean;
  headers: string;
  submitted: boolean;
  errorMessage: string;
  dialogVisible: boolean = false;
  isLoggedIn : boolean;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService    
  ) {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
    this.isLoggedIn = authService.getIsLoggedIn();

    this.authService.loginStatusChanged.subscribe((status: boolean) => {
     this.isLoggedIn = status;
     this.authService.setIsLoggedIn(status);
   });
  }

  ngOnInit() {
    this.carDialog = true;
    this.headers = 'Login Details';
    this.isLoggedIn = this.authService.getIsLoggedIn();
  }

  hideDialog() {
    this.isLoggedIn = this.authService.getIsLoggedIn();
    this.router.navigateByUrl('/dashboard');
    this.carDialog = false;
  }

  showDialog(message: string) {
    this.errorMessage = message;
    this.dialogVisible = true;
  }
  
  closeDialog() {
    this.dialogVisible = false;
  }

  onSubmit() {
    this.submitted = true;

    if (this.loginForm.invalid) {
      return;
    }

    const email = this.loginForm.value.email;
    const password = this.loginForm.value.password;

    this.authService.loginUser(email, password).subscribe(
      (response) => {
        console.log('Login successful');
        console.log('Response:', response);
  
        if (response !== "Unauthorized" && response !== null) {
          this.login();
          this.router.navigateByUrl('/datagrid');
        } else {
          this.errorMessage = 'Invalid authentication credentials.';
        }
      },
      (error) => {
        console.error('Login error:', error);
        this.errorMessage = 'Invalid authentication credentials.';
      },
      () => {
        console.info('Login complete');
      }
    );   
  }

  login(): void {
    this.authService.login();
  }
}
