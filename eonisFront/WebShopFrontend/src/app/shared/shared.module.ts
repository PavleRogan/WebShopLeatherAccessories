import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagingHeaderComponent } from './components/paging-header/paging-header.component';
import { PagerComponent } from './components/pager/pager.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';


@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent
  ],
  imports: [
    CommonModule,
    PaginationModule,
    FormsModule,
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),

  ], 
  exports:[PagingHeaderComponent,
    PagerComponent,
    ReactiveFormsModule,
    BsDropdownModule
  ]
})
export class SharedModule { }
