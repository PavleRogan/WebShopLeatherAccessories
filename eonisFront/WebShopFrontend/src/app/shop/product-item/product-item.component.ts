import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/product';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AccountService } from 'src/app/account/account.service';
import { Observable } from 'rxjs';
import { IUser } from 'src/app/shared/models/user';


@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit{

  @Input() product!: IProduct;
  currentUser$!: Observable<IUser | null>;  
  currentUser!: IUser | null;

  constructor(private basketService: BasketService,private snackBar: MatSnackBar, private accService:AccountService){}

  ngOnInit(): void {
    this.currentUser$ = this.accService.currentUser$;
   this.currentUser$.subscribe(user => {
    this.currentUser = user;
  });
  }

  addItemToBasket(){
    this.basketService.addItemToOrder(this.product);
    console.log('Adding item to basket:', this.product); 
    this.snackBar.open(`${this.product.name} was added to your basket`, 'OK', {
      duration: 3000,
      panelClass: ['success-snackbar']
    });
  }

  isAdmin(user: any): boolean { 
    return user && user.role === 'Admin'; 
  }
}
