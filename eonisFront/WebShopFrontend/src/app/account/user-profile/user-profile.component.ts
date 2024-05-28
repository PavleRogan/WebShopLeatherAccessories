import { Component } from '@angular/core';
import { Observable} from 'rxjs';
import { IUser } from 'src/app/shared/models/user';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-user-profile',
  templateUrl: './user-profile.component.html',
  styleUrls: ['./user-profile.component.scss']
})
export class UserProfileComponent {


  currentUser$!: Observable<IUser | null>;  
  currentUser!: IUser | null;

 constructor(private accService:AccountService){}

 ngOnInit(): void {
   this.currentUser$ = this.accService.currentUser$;
   this.currentUser$.subscribe(user => {
    this.currentUser = user;
    console.log("curr user = " + JSON.stringify(this.currentUser));
  });
 }

 onEdit() {
  //editing logic i metoda u accService patch user
 }
   


  }
  


