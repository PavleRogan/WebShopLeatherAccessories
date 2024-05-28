import { Component } from '@angular/core';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {

  constructor(private accountService: AccountService){}

  registerUser(userValues: any): void {
    this.accountService.register(userValues).subscribe(
      (result: boolean) => {
        if (result) {
          console.log('User created successfully');
          // You can add more logic here to handle successful user creation
        } else {
          console.log('User creation failed');
          // You can add more logic here to handle user creation failure
        }
      },
      (error: any) => {
        console.error('An error occurred:', error);
        // Handle any additional errors if necessary
      }
    );
  }
}
