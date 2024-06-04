import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ShopService } from '../shop.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { DialogRef } from '@angular/cdk/dialog';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-product-add-edit',
  templateUrl: './product-add-edit.component.html',
  styleUrls: ['./product-add-edit.component.scss']
})
export class ProductAddEditComponent implements OnInit {

  categories = ['Wallets','Purses','Accessories','Belts'];
  prodForm: FormGroup;

  constructor(private fb : FormBuilder, private shopService:ShopService, private snackBar: MatSnackBar
    ,private dialogRef: DialogRef<ProductAddEditComponent>, @Inject(MAT_DIALOG_DATA) public data: any,
    private router: Router,

  ){ 
    this.prodForm = this.fb.group({
      name: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      category: new FormControl('', Validators.required),
      gender: new FormControl('', Validators.required),
      price: new FormControl('', [Validators.required ,Validators.min(1)]),
      stockQuantity: new FormControl('', Validators.required),
      imageUrl: new FormControl('', Validators.required)

    })
   }
  
   ngOnInit(): void {
     this.prodForm.patchValue(this.data);
     console.log(this.data);
   }

   onFormSubmit() {
      if(this.prodForm.valid){
        if(this.data){
          const productId = this.data.productId;
          
          if(productId){
            this.shopService.updateProduct(productId,this.prodForm.value).subscribe({
              next:(val:any)=>{
                this.dialogRef.close();
                this.router.navigateByUrl('/shop');
                this.snackBar.open('Product updated succcessfuly!', 'Close', {
                  duration: 3000,
                });
              }, error:(err)=>{
                console.error(err)
                
              }
            });
          }
         
        }else{
          this.shopService.createProduct(this.prodForm.value).subscribe({
            next:(val:any)=>{
              this.dialogRef.close();
              this.snackBar.open('Product added succcessfuly!', 'Close', {
                duration: 3000,
              });
            }, error:(err)=>{
              console.error(err)
              
            }
          });
        }
        
      }else{
        this.snackBar.open('Data is invalid!', 'Close', {
          duration: 3000,
        });
      }
    }
    public cancel(): void {
      this.dialogRef.close();
      this.snackBar.open('No changes!', 'ok', { duration: 1500 });
    }
}
