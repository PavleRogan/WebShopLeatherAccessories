import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IPagination } from '../shared/models/pagination';
import {map} from 'rxjs/operators'

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = "https://localhost:7010/api/"

  constructor(private http: HttpClient) { }

  getProducts(pageSize:number,pageNumber: number,searchPhrase?:string, gender?:string, category?:string){

    let params = new HttpParams()
    .set("pageSize", pageSize.toString())
    .set("pageNumber", pageNumber.toString());

  if (searchPhrase) {
    params = params.set("searchPhrase", searchPhrase);
  }
  if (gender) {
    params = params.set("gender", gender);
  }
  if (category) {
    params = params.set("category", category);
  }

    return this.http.get<IPagination>(this.baseUrl + 'products', {observe: 'response',params}).pipe(
      map(response =>{
        return response.body;
      })
    );
  }
}
