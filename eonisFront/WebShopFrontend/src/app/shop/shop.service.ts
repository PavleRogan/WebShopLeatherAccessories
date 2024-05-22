import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import {map} from 'rxjs/operators'
import { ShopParams } from '../shared/models/shopParams';
import { IProduct } from '../shared/models/product';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = "https://localhost:7010/api/"

  constructor(private http: HttpClient) { }

  getProducts(shopParams : ShopParams){

    let params = new HttpParams()
    .set("pageSize", shopParams.pageSize.toString())
    .set("pageNumber", shopParams.pageNumber.toString());

  if (shopParams.searchPhrase) {
    params = params.set("searchPhrase",shopParams.searchPhrase);
  }
  if (shopParams.gender) {
    params = params.set("gender", shopParams.gender);
  }
  if (shopParams.category) {
    params = params.set("category", shopParams.category);
  }
  if (shopParams.sortBy) {
    params = params.set("sortBy", shopParams.sortBy);
  }
  if (shopParams.sortDirection) {
    params = params.set("sortDirection", shopParams.sortDirection);
  }

    return this.http.get<IPagination>(this.baseUrl + 'products', {observe: 'response',params}).pipe(
      map(response =>{
        return response.body;
      })
    );
  }

  getProduct(id: string){
    return this.http.get<IProduct>(this.baseUrl + 'products/' + id);
  
  }
}
