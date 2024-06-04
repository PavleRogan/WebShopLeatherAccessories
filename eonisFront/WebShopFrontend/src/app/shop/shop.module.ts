import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './product-item/product-item.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { SharedModule } from '../shared/shared.module';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { RouterModule } from '@angular/router';
import { ShopRoutingModule } from './shop-routing.module';
import { CoreModule } from '../core/core.module';
import { ProductEditComponent } from './product-edit/product-edit.component';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field'; // Import MatFormFieldModule
import { MatInputModule } from '@angular/material/input'; // Import MatInputModule
import { FormsModule } from '@angular/forms';
import { ProductAddEditComponent } from './product-add-edit/product-add-edit.component';
import { MatRadioModule } from '@angular/material/radio';
import {MatSelectModule} from '@angular/material/select';


@NgModule({
  declarations: [
    ShopComponent,
    ProductItemComponent,
    ProductDetailsComponent,
    ProductEditComponent,
    ProductAddEditComponent,
  ],
  imports: [
    CommonModule,
    SharedModule,
    PaginationModule,
    ShopRoutingModule,
    CoreModule,
    MatInputModule,
    FormsModule,
    MatButtonModule,
    MatSelectModule,
    MatDialogModule,
    MatFormFieldModule,
    MatIconModule,
    MatRadioModule
  ],
  exports : [
    
  ]
})
export class SHOPModule { }
