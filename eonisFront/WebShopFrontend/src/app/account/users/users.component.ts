import { Component, OnInit } from '@angular/core';
import { AccountService } from '../account.service';
import { SelectionModel } from '@angular/cdk/collections';
import {AfterViewInit, ViewChild} from '@angular/core';
import {MatPaginator, MatPaginatorModule} from '@angular/material/paginator';
import {MatTableDataSource, MatTableModule} from '@angular/material/table';
import { IUser } from 'src/app/shared/models/user';
import {MatSort, Sort, MatSortModule} from '@angular/material/sort';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MatSnackBar } from '@angular/material/snack-bar';



@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {


  displayedColumns: string[] = ['userId','email', 'name', 'contactNumber', 'city', 'orders','action'];
  dataSource = new MatTableDataSource<any>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;


  constructor(private accService: AccountService, private snackBar: MatSnackBar){

  }

  ngOnInit(): void {
    this.getAllUsers();
  }

  getAllUsers(){
    this.accService.getAllUsers().subscribe({
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

  onDeleteUser(id: string): void {
    const confirmDelete = confirm('Are you sure you want to delete this user?');
    if (confirmDelete) {
      this.accService.deleteUser(id).subscribe({
        next: (res) => {
          console.log(res);
          this.getAllUsers();
          this.snackBar.open('User deleted!', 'Close', {
            duration: 3000,
          });
        },
        error: (err) => {
          console.log(err);
        }
      });
    }
  }
}
