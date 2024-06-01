import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { UserProfileComponent } from './user-profile/user-profile.component';
import { AuthGuard } from '../core/auth.guard';
import { MyOrdersComponent } from './my-orders/my-orders.component';

const routes:Routes =
[
  {path:'login',component:LoginComponent},
  {path:'register',component:RegisterComponent},
  {path:'profile',canActivate:[AuthGuard], component: UserProfileComponent},
  {path:'my-orders',canActivate:[AuthGuard], component: MyOrdersComponent}
]


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports:[RouterModule]
})
export class AccountRoutingModule { }
