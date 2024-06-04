import { Component } from '@angular/core';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';
import { ProductAddEditComponent } from '../product-add-edit/product-add-edit.component';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styleUrls: ['./product-edit.component.scss']
})
export class ProductEditComponent {

  product!: IProduct;
  
  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private snackBar: MatSnackBar,
    private dialog:MatDialog
  ) {}

  ngOnInit(): void {
    this.loadProduct();
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

  openUpdateProduct(data: any): void {
    this.dialog.open(ProductAddEditComponent,{
      data
    });

  }

  deleteProduct(): void {
    if (confirm('Are you sure you want to delete this product?')) {
      const productId = this.activatedRoute.snapshot.paramMap.get('id');
      if(productId){

        this.shopService.deleteProduct(productId).subscribe(data => {
          this.snackBar.open('Product deleted!', 'ok', { duration: 1500 });
          this.router.navigate(['/shop']);
          console.log(data);
       }, error =>{
        console.log(error);
       });
      }
    }
  }
}
