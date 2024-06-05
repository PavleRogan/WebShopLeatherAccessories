import { HttpClient, HttpHandler, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, catchError, map, of, switchMap, throwError } from 'rxjs';
import { IUpdateUserCommand, IUser } from '../shared/models/user';
import { Router } from '@angular/router';
import { stringify } from 'uuid';
import { jwtDecode } from 'jwt-decode'; 
import { IOrder } from '../shared/models/order';


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
  updateUser(currentUser: IUser | null): Observable<void | null> {
    if (!currentUser || !currentUser.userId) {
      throw new Error('Invalid user data');
    }

    const updateUserCommand: IUpdateUserCommand = {
      userId: currentUser.userId,
      name: currentUser.name || '',
      contactNumber: currentUser.contactNumber,
      city: currentUser.city,
      streetAndNumber: currentUser.streetAndNumber,
      postalCode: currentUser.postalCode
    };

    const token = localStorage.getItem('token');
    if (!token) {
      throw new Error('No token found');
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.patch<void>(`${this.baseUrl}users/${currentUser.userId}`, updateUserCommand, { headers }).pipe(
      switchMap(() => this.loadCurrentUser(token, currentUser.email)), // Make a subsequent call to loadCurrentUser
      catchError((error) => {
        console.error('Error occurred while updating user:', error);
        return of(null); 
      })
    );
  }
  
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('email');
    localStorage.removeItem('loggedIn');
    localStorage.removeItem('orderId');
    this.currentUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  register(user: any): Observable<boolean> {
    return this.http.post<any>(this.baseUrl + 'users', user, { observe: 'response' }).pipe(
      map((response: HttpResponse<any>) => {
        return response.status === 201;
      }),
      catchError((error) => {
        console.error('Error occurred during registration:', error);
        return throwError(error); // Throw error in case of an error
      })
    );
  }

  getUserOrders(userId: string): Observable<IOrder[]> {
    const token = localStorage.getItem('token');
    if (!token) {
      throw new Error('No token available.');
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.get<IOrder[]>(this.baseUrl + 'users/' + userId +'/orders', { headers });
  }

  getAllUsers():Observable<any>{
    const token = localStorage.getItem('token');
    if (!token) {
      throw new Error('No token available.');
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.get<IUser[]>(this.baseUrl + 'users',{headers});
  }

  deleteUser(id:string): Observable<any>{
    const token = localStorage.getItem('token');
    if (!token) {
      throw new Error('No token available.');
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.delete(this.baseUrl + 'users/' + id,{headers});
  }

  getAllAdmins():Observable<any>{
    const token = localStorage.getItem('token');
    if (!token) {
      throw new Error('No token available.');
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    
    return this.http.get(this.baseUrl + 'admins',{headers});
  }

  deleteAdmin(id:string): Observable<any>{
    const token = localStorage.getItem('token');
    if (!token) {
      throw new Error('No token available.');
    }

    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.delete(this.baseUrl + 'admins/' + id,{headers});
  }

  updateAdmin(id:string, data:any){
    const token = localStorage.getItem('token');
    if (!token) {
      throw new Error('No token available.');
    }
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);
    return this.http.patch(this.baseUrl + 'admins/' + id,{data},{headers});
  }

  createAdmin(){
    const token = localStorage.getItem('token');
    if (!token) {
      throw new Error('No token available.');
    }
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

  }
}
