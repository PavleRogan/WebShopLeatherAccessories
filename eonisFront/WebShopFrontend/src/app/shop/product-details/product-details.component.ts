import { Component, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { IUser } from 'src/app/shared/models/user';
import { Observable } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit{

  product!: IProduct;
  currentUser$!: Observable<IUser | null>;  
  currentUser!: IUser | null;

  constructor(private shopService: ShopService,private activatedRoute: ActivatedRoute,private snackBar: MatSnackBar, 
    private basketService: BasketService, private accService: AccountService){}
  
  ngOnInit(): void {
   this.loadProduct();
   this.currentUser$ = this.accService.currentUser$;
   this.currentUser$.subscribe(user => {
    this.currentUser = user;
  });
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
  isAdmin(user: any): boolean { 
    return user && user.role === 'Admin'; 
  }

}
