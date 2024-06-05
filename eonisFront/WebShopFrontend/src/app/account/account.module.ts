import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AccountRoutingModule } from './account-routing.module';
import { SharedModule } from '../shared/shared.module';
import { UserProfileComponent } from './user-profile/user-profile.component';
import {MatTableModule} from '@angular/material/table';
import { FormsModule } from '@angular/forms';
import { MyOrdersComponent } from './my-orders/my-orders.component';
import { UsersComponent } from './users/users.component';
import { AdminsComponent } from './admins/admins.component'; // Import FormsModule
import { MatSortModule } from '@angular/material/sort';
import {MatPaginatorModule} from '@angular/material/paginator';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatIconModule } from '@angular/material/icon';
import { AdminDialogComponent } from './admins/admin-dialog/admin-dialog.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';


@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    UserProfileComponent,
    MyOrdersComponent,
    UsersComponent,
    AdminsComponent,
    AdminDialogComponent
  ],
  imports: [
    CommonModule,
    AccountRoutingModule,
    SharedModule,
    FormsModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDialogModule,
    MatIconModule,
    MatRadioModule
  ]
})
export class AccountModule { }
