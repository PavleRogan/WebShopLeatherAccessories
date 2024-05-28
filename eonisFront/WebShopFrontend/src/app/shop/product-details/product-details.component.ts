import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit{

  product!: IProduct;

  constructor(private shopService: ShopService,private activatedRoute: ActivatedRoute,private snackBar: MatSnackBar, private basketService: BasketService){}
  
  ngOnInit(): void {
   this.loadProduct();
  }

  addItemToBasket(){
    this.basketService.addItemToOrder(this.product);
    this.snackBar.open(`${this.product.name} was added to your basket`, 'OK', {
      duration: 3000,
      panelClass: ['success-snackbar']
    });
  }
  


  loadProduct(){
    const productId = this.activatedRoute.snapshot.paramMap.get('id');
    if (productId) {
      this.shopService.getProduct(productId).subscribe(product => {
        this.product = product;
      }, error => {
        console.log(error);
      });
    } else {
      console.log('Product ID is null');
    }
    ;
  }

}
