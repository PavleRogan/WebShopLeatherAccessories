import { Component, OnInit } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit{

  pageSize: number = 5;
  pageNumber: number = 1;
  products!: IProduct[] | undefined;
  selectedGender: string | undefined;
  selectedCategory: string | undefined;
  searchPhrase: string | undefined = undefined;

  constructor(private shopService: ShopService){}

  ngOnInit(): void {
   this.getProducts();   
  }

  getProducts(){
    this.shopService.getProducts(this.pageSize, this.pageNumber,this.searchPhrase,this.selectedGender,this.selectedCategory).subscribe(
      response => {
        this.products = response?.items;
        console.log(this.products);
      }, (error) => {
            console.log(error);
          }
      );
  }


  onGenderSelected(gender:string){
    this.selectedGender = gender;
    this.getProducts();
  }
  onCategorySelected(category:string){
    this.selectedCategory = category;
    this.getProducts();
  }
  onGenderAllSelected(){
    this.selectedGender = undefined;
    this.getProducts();
  }
  onCategoryAllSelected(){
    this.selectedCategory = undefined;
    this.getProducts();
  }
}
