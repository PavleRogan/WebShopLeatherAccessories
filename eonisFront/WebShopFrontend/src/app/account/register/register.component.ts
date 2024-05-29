import { Component } from '@angular/core';
import { AccountService } from '../account.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  registerForm!: FormGroup;

  constructor(private fb: FormBuilder, private accountService: AccountService, private snackBar:MatSnackBar, private router:Router) { }

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      city: [''],
      streetAndNumber: [''],
      postalCode: [''],
      contactNumber: ['']
    });
  }

  onSubmit(): void {
    if (this.registerForm.valid) {
      this.accountService.register(this.registerForm.value).subscribe(
        response => {
          if (response) {
            console.log('Registration successful');
            this.snackBar.open('Profile created. Welcome!', 'Close', {
              duration: 3000,
            });
            this.router.navigateByUrl('/account/login');
          } else {
            console.error('Registration failed');
           
            this.snackBar.open('Invalid data. Please try again.', 'Close', {
              duration: 3000,
            });

          }
        },
        error => {
          console.error('Error occurred during registration:', error);
        }
      );
    }
  }
}

