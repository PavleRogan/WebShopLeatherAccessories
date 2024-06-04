import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from '../shared/shared.module';
import { UserProfileComponent } from './user-profile/user-profile.component';

import { FormsModule } from '@angular/forms';
import { MyOrdersComponent } from './my-orders/my-orders.component';
import { UsersComponent } from './users/users.component';
import { AdminsComponent } from './admins/admins.component'; // Import FormsModule


@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    UserProfileComponent,
    MyOrdersComponent,
    UsersComponent,
    AdminsComponent
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    SharedModule,
    FormsModule
  ]
})
export class AccountModule { }
