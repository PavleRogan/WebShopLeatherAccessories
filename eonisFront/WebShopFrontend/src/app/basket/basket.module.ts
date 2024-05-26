import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BasketComponent } from './basket.component';
import { BasketRoutingModule } from './basket-routing/basket-routing.module';
import { FormsModule } from '@angular/forms'; // Import FormsModule



@NgModule({
  declarations: [
    BasketComponent
  ],
  imports: [
    CommonModule,
    BasketRoutingModule,
    FormsModule
  ]
})
export class BasketModule { }
