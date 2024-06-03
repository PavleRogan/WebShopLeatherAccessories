import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutComponent } from './checkout.component';
import { CheckoutRoutingModule } from './checkout-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { CheckoutSuccessComponent } from './checkout-success/checkout-success.component';

import { FormsModule } from '@angular/forms';
import { CheckoutFailComponent } from './checkout-fail/checkout-fail.component';


@NgModule({
  declarations: [
    CheckoutComponent,
    CheckoutSuccessComponent,
    CheckoutFailComponent
  ],
  imports: [
    CommonModule,
    CheckoutRoutingModule,
    ReactiveFormsModule,
    FormsModule
  ]
})
export class CheckoutModule { }
