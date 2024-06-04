import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';
import { ShopParams } from '../shared/models/shopParams';
import { AccountService } from '../account/account.service';
import { Observable } from 'rxjs';
import { IUser } from '../shared/models/user';
import { MatDialog } from '@angular/material/dialog';
import { ProductAddEditComponent } from './product-add-edit/product-add-edit.component';


@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit{


  @ViewChild('search',{static: false}) searchTerm! : ElementRef;
  products!: IProduct[] | undefined;
  shopParams = new ShopParams();
  totalCount! : number;
  currentUser$!: Observable<IUser | null>;  
  currentUser!: IUser | null;

  constructor(private shopService: ShopService, private accService: AccountService, private dialog:MatDialog){}

  ngOnInit(): void {
    this.getProducts();   
    this.currentUser$ = this.accService.currentUser$;
   this.currentUser$.subscribe(user => {
    this.currentUser = user;
  });
  }

  isAdmin(user: any): boolean { 
    return user && user.role === 'Admin'; 
  }

  getProducts(){
    this.shopService.getProducts(this.shopParams).subscribe(
      response => {
        this.products = response?.items;
        this.shopParams.pageNumber = response?.pageNumber ?? this.shopParams.pageNumber;
        this.shopParams.pageSize = response?.pageSize ?? this.shopParams.pageSize;
        this.totalCount = response?.totalItemsCount ?? 0;
      }, (error) => {
            console.log(error);
          }
      );
  }

 addProductOpen(){
  this.dialog.open(ProductAddEditComponent);
 }

  onGenderSelected(gender:string){
    this.shopParams.gender = gender;
    this.getProducts();
  }
  onCategorySelected(category:string){
    this.shopParams.category = category;
    this.getProducts();
  }
  onGenderAllSelected(){
    this.shopParams.gender = undefined;
    this.getProducts();
  }
  onCategoryAllSelected(){
    this.shopParams.category = undefined;
    this.getProducts();
  }
  onSortSelected(event:Event){
    const target = event.target as HTMLSelectElement;
    const value = target.value.split(" ");
    var sortBy = value[0];
    var  sortDirection =value[1];
    this.shopParams.sortBy= sortBy;
    this.shopParams.sortDirection = sortDirection;
    this.getProducts();
  }

  
  onPageChanged(event: any) {
    if(this.shopParams.pageNumber !== event){
      this.shopParams.pageNumber = event ;
          this.getProducts();
    }
    
  }

  onSearch(){
    this.shopParams.searchPhrase = this.searchTerm.nativeElement.value;
    this.getProducts();
  }

  onReset(){
    this.shopParams.searchPhrase = undefined;
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
