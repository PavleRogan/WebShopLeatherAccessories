import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/product';
import { MatSnackBar } from '@angular/material/snack-bar';


@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit{

  @Input() product!: IProduct;

  constructor(private basketService: BasketService,private snackBar: MatSnackBar){}

  ngOnInit(): void {
    
  }

  addItemToBasket(){
    this.basketService.addItemToOrder(this.product);
    console.log('Adding item to basket:', this.product); 
    this.snackBar.open(`${this.product.name} was added to your basket`, 'OK', {
      duration: 3000,
      panelClass: ['success-snackbar']
    });
  }
}
