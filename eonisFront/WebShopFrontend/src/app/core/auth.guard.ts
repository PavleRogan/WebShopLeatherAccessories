import { Injectable } from "@angular/core";
import { MatSnackBar } from "@angular/material/snack-bar";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable, map, of, tap } from "rxjs";
import { AccountService } from "src/app/account/account.service";

@Injectable({providedIn:'root'})
export class AuthGuard implements CanActivate {


  constructor(private accService:AccountService, private router : Router, private snackBar: MatSnackBar){}
  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    // Check if user is logged in
    const isLoggedIn = localStorage.getItem('loggedIn') === 'true';
    if (isLoggedIn) {
      return of(true);
    } else {
      // Display a message or handle redirection
      this.snackBar.open('You must be logged in for this!', 'Close', {
        duration: 3000,
      });
      return of(false);
    }
  }

}