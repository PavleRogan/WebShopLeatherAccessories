import { DialogRef } from '@angular/cdk/dialog';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-dialog',
  templateUrl: './admin-dialog.component.html',
  styleUrls: ['./admin-dialog.component.scss']
})
export class AdminDialogComponent implements OnInit {

  adminForm: FormGroup;
  isUpdate: boolean;

  constructor(private fb : FormBuilder, private snackBar: MatSnackBar
    ,private dialogRef: DialogRef<AdminDialogComponent>, @Inject(MAT_DIALOG_DATA) public data: any,
    private router: Router){

      this.adminForm= this.fb.group({
        name: new FormControl('', Validators.required),
        surname: new FormControl('', Validators.required),
        contact: new FormControl('', Validators.required),
        username: new FormControl('', Validators.required),
        password: new FormControl('', Validators.required),
      });
      this.isUpdate = data.isUpdate;
      console.log(data.isUpdate);
  }

  ngOnInit(): void {
    
  }

  cancel() {
    throw new Error('Method not implemented.');
    }
    
    onFormSubmit() {
    throw new Error('Method not implemented.');
    }
}
