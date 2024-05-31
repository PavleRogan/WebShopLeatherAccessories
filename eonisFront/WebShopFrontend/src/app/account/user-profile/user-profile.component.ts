import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/shared/models/user';
import { AccountService } from '../account.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent implements OnInit {

  currentUser$!: Observable<IUser | null>;  
  currentUser!: IUser | null;

  constructor(private accService: AccountService, private snackBar: MatSnackBar) {}

  ngOnInit(): void {
    this.currentUser$ = this.accService.currentUser$;
    this.currentUser$.subscribe(user => {
      this.currentUser = user;
      console.log("curr user = " + JSON.stringify(this.currentUser));
    });
  }

  onEdit() {
    if (this.currentUser) {
      const { name, contactNumber, city, streetAndNumber, postalCode } = this.currentUser;

      if (!name || !contactNumber || !city || !streetAndNumber || !postalCode) {
        this.snackBar.open('All fields are required.', 'Close', {
          duration: 3000,
        });
        return;
      }

      this.accService.updateUser(this.currentUser).subscribe(
        updatedUser => {
          this.snackBar.open('Info updated.', 'Close', {
            duration: 3000,
          });
        },
        error => {
          console.error('Error updating user:', error);
        }
      );
    }
  }
}
