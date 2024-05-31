import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;

  constructor(private fb: FormBuilder, private accountService: AccountService, private snackBar: MatSnackBar, private router: Router) { }

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      city: ['',Validators.required],
      streetAndNumber: ['',Validators.required],
      postalCode: ['',Validators.required],
      contactNumber: ['',Validators.required]
    });
  }

  isFieldInvalid(field: string): boolean {
    const control = this.registerForm.get(field);
    return (control?.invalid && (control.dirty || control.touched)) ?? false;
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
          this.snackBar.open('Email exists. Please try again.', 'Close', {
            duration: 3000,
          });
        }
      );
    } else {
      this.snackBar.open('All fields are required.', 'Close', {
        duration: 3000,
      });
      this.registerForm.markAllAsTouched();
    }
  }
}
