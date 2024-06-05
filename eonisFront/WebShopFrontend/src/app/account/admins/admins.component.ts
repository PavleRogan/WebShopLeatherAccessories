import { Component, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { AccountService } from '../account.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { AdminDialogComponent } from './admin-dialog/admin-dialog.component';

@Component({
  selector: 'app-admins',
  templateUrl: './admins.component.html',
  styleUrls: ['./admins.component.scss']
})
export class AdminsComponent {

  displayedColumns: string[] = ['adminId','username','name','surname','contact','action'];
  dataSource = new MatTableDataSource<any>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;


  constructor(private accService: AccountService, private snackBar: MatSnackBar, private dialog:MatDialog){

  }

  ngOnInit(): void {
    this.getAllAdmins();
  }

  getAllAdmins(){
    this.accService.getAllAdmins().subscribe({
      next:(res)=>{
          this.dataSource = new MatTableDataSource(res);
          this.dataSource.sort = this.sort;
          this.dataSource.paginator = this.paginator;
      }, error: (err)=>{ 
          console.log(err);
      }
    });
  }

  applyFilter(event: Event): void {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  onDeleteAdmin(id: string, username:string): void {
    const curAdmin = localStorage.getItem('email');
    if(curAdmin != username){
      const confirmDelete = confirm('Are you sure you want to delete this admin?');
      if (confirmDelete) {
        this.accService.deleteAdmin(id).subscribe({
          next: (res:any) => {
            console.log(res);
            this.getAllAdmins();
            this.snackBar.open('Admin deleted!', 'Close', {
              duration: 3000,
            });
          },
          error: (err:any) => {
            console.log(err);
          }
        });
      }else{
        console.log("Cant delete current!")
        this.snackBar.open('Cant delete yourself!', 'Close', {
          duration: 3000,
        });
      }
    }
  }

  openAdminDialog(isUpdate:boolean){
    const dialogConfig = new MatDialogConfig();
    dialogConfig.data = { isUpdate }; // Pass the flag to the dialog
    this.dialog.open(AdminDialogComponent, dialogConfig);
    
  }
}
