import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent  implements OnInit{

  loginForm! : FormGroup;

  constructor(private accService: AccountService, private router: Router,private snackBar: MatSnackBar){
  }
  
  ngOnInit(): void {
    this.createLoginForm();
  }

  createLoginForm(){
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', Validators.required)
    })
  }


  onSubmit() {
    this.accService.login(this.loginForm.value).subscribe(()=>{
      console.log("user logged in");
      this.router.navigateByUrl('/shop');
    }, error =>{
      console.log(error);
      this.snackBar.open('Invalid credentials. Please try again.', 'Close', {
        duration: 3000,
      });
    });
    }
}
