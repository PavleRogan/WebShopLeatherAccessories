import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { CheckoutComponent } from './checkout.component';
import { AuthGuard } from '../core/auth.guard';
import { CheckoutSuccessComponent } from './checkout-success/checkout-success.component';
import { CheckoutFailComponent } from './checkout-fail/checkout-fail.component';

const routes: Routes = [
  {path:'',component: CheckoutComponent},
  {path:'success', component: CheckoutSuccessComponent},
  {path:'fail', component: CheckoutFailComponent}


]


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)

  ],
  exports : [RouterModule]
})
export class CheckoutRoutingModule { }
