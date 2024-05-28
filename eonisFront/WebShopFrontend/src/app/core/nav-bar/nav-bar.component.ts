import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { IUser } from 'src/app/shared/models/user';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss']
})
export class NavBarComponent implements OnInit {


  currentUser$!: Observable<IUser | null>;  
  currentUser!: IUser | null;

 constructor(private accService:AccountService){}

 ngOnInit(): void {
   this.currentUser$ = this.accService.currentUser$;
   this.currentUser$.subscribe(user => {
    this.currentUser = user;
  });
 }

 isAdmin(user: any): boolean { // Assuming currentUser$ emits any type of object
  return user && user.role === 'Admin'; // Check if the user's role is admin
}

 onLogOut() {
  this.accService.logout();
  }
}
