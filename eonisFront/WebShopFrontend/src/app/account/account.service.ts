import { HttpClient, HttpHandler, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, catchError, map } from 'rxjs';
import { IUser } from '../shared/models/user';
import { Router } from '@angular/router';
import { stringify } from 'uuid';
import { jwtDecode } from 'jwt-decode'; 


@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = "https://localhost:7010/api/";

  private currentUserSource = new BehaviorSubject<IUser | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http:HttpClient, private router: Router) { }

  login(values: any) {
    return this.http.post<IUser>(this.baseUrl + 'Auth/authenticate', values).pipe(
      map((user: IUser | null) => {
        if(user){
          localStorage.setItem('token', user.token);
          localStorage.setItem('email', user.email);
        this.currentUserSource.next(user);
        

        const decodedToken: any = jwtDecode(user.token!);
        const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        if (role == 'User') {
          this.loadCurrentUser(user.token, user.email).subscribe(() => {
            console.log('loaded user' + user.email);
            console.log(role);
          });
        } else if (role == 'Admin') {
          this.loadCurrentAdmin(user.token, user.email).subscribe(() => {
            console.log('loaded admin' + user.email);
            console.log(role);
          });
        }
        }
        
      })
    )
  }

  getCurrentUserValue(){
    return this.currentUserSource.value;
  }

  loadCurrentUser(token:string, email: string){
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get<any>(this.baseUrl + 'users/email/' + email,{headers}).pipe(
      map((user: IUser | null) => {
        if(user){
          user.role = "User";
          this.currentUserSource.next(user);
          console.log("from load curr "+ JSON.stringify(user, null, 2));
        }
      })
    );
  }
  loadCurrentAdmin(token:string, email: string){
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);
    return this.http.get<any>(this.baseUrl + 'admins/email/' + email,{headers}).pipe(
      map((admin: IUser | null) => {
        if(admin){
          const usr = {
            token,
            email,
            role : "Admin"
          }
          this.currentUserSource.next(usr);
          console.log("from load curr "+ JSON.stringify(usr, null, 2));

        }
      })
    );
  }

  
 

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('email');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  register(values: any): Observable<boolean> {
    return this.http.post<any>(this.baseUrl + 'users', values, { observe: 'response' }).pipe(
      map((response: HttpResponse<any>) => {
        return response.status === 201;
      }),
      catchError((error) => {
        // Handle error response here if needed
        console.error('Error occurred:', error);
        return [false];  // Return false in case of an error
      })
    );
  }

}
