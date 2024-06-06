import { DialogRef } from '@angular/cdk/dialog';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AccountService } from '../../account.service';

@Component({
  selector: 'app-admin-dialog',
  templateUrl: './admin-dialog.component.html',
  styleUrls: ['./admin-dialog.component.scss']
})
export class AdminDialogComponent implements OnInit {

  adminForm: FormGroup;

  constructor(private fb : FormBuilder, private snackBar: MatSnackBar, private accService:AccountService,
    private dialogRef: DialogRef<AdminDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: any,
    private router: Router){

      this.adminForm= this.fb.group({
        name: new FormControl('', Validators.required),
        surname: new FormControl('', Validators.required),
        contact: new FormControl('', Validators.required),
        username: new FormControl('', Validators.required),
        password: new FormControl('', Validators.required),
      });
  }

  ngOnInit(): void {
    this.adminForm.patchValue(this.data);
  }

  cancel() {
    this.dialogRef.close();
    this.snackBar.open('No changes!', 'ok', { duration: 1500 });
    }
    
    onFormSubmit() {
      if(this.adminForm.valid){
        if(this.data){

          const adminId = this.data.adminId;
          if(adminId){
            
            this.accService.updateAdmin(adminId,this.adminForm.value).subscribe({
              next:(val:any)=>{
                this.dialogRef.close();
                this.router.navigateByUrl('/account/admins');
                this.snackBar.open('Admin updated succcessfuly!', 'Close', {
                  duration: 3000,
                });
              }, error:(err)=>{
                console.error(err)
                
              }
            });
          }
         
        }else{
          this.accService.createAdmin(this.adminForm.value).subscribe({
            next:(val:any)=>{
              this.dialogRef.close();
              this.snackBar.open('Admin added succcessfuly!', 'Close', {
                duration: 1500,
              });
            }, error:(err)=>{
              console.error(err)
              this.snackBar.open('Admin already exists!', 'Close', {
                duration: 1500,
              });
            }
          });
        }
        
      }else{
        this.snackBar.open('Data is invalid!', 'Close', {
          duration: 3000,
        });
      }
    }
}
