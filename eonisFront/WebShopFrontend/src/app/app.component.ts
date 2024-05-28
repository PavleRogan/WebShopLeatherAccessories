import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IProduct } from './shared/models/product';
import { IPagination } from './shared/models/pagination';
import { AccountService } from './account/account.service';
import { jwtDecode } from 'jwt-decode'; // Use named import syntax


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'WebShopFrontend';
  products : IProduct[] | undefined;

  constructor(private accService: AccountService){}

  ngOnInit(): void {
    this.loadCurrUser();
  }

  loadCurrUser() {
    const token = localStorage.getItem('token');
    const email = localStorage.getItem('email');

    const decodedToken: any = jwtDecode(token!);
    const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];

    if(token){
      if(role =='User'){
        this.accService.loadCurrentUser(token, email!).subscribe(()=>{
          console.log('loaded user' + email);
          console.log(role);
        });
      }else{
        this.accService.loadCurrentAdmin(token, email!).subscribe(()=>{
          console.log('loaded admin' + email);
          console.log(role);
        });
      }
      
    }
  }

}


