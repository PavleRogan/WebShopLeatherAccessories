import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IProduct } from './models/product';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'WebShopFrontend';
  products : IProduct[] | undefined;

  constructor(private http: HttpClient){}

  ngOnInit(): void {}

}
