import { NgModule } from '@angular/core';
import { HomeComponent } from './home/home/home.component';
import { RouterModule, Routes } from '@angular/router';
import { ShopComponent } from './shop/shop.component';
import { ProductDetailsComponent } from './shop/product-details/product-details.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'shop', loadChildren: ()=> import('./shop/shop.module').then(mod => mod.SHOPModule)},
  {path: 'basket', loadChildren: ()=> import('./basket/basket.module').then(mod => mod.BasketModule)},
  {path: 'checkout', loadChildren: ()=> import('./checkout/checkout.module').then(mod => mod.CheckoutModule)},
  {path: 'account', loadChildren: ()=> import('./account/account.module').then(mod => mod.AccountModule)},
  {path: '**', redirectTo: '', pathMatch: 'full'},
]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
