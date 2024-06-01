import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { CheckoutComponent } from './checkout.component';
import { PaymentComponent } from './payment/payment.component';
import { AuthGuard } from '../core/auth.guard';
import { CheckoutSuccessComponent } from './checkout-success/checkout-success.component';

const routes: Routes = [
  {path:'',component: CheckoutComponent},
  {path:'payment', component: PaymentComponent},
  {path:'success', component: CheckoutSuccessComponent}


]


@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(routes)

  ],
  exports : [RouterModule]
})
export class CheckoutRoutingModule { }
