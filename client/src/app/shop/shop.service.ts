import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/products';
import { IType } from '../shared/models/productType';
import { map} from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7034/api/';

  constructor(private http:HttpClient) {}

  getProducts(shopParams: ShopParams)
    // rows?: number, PageId?:number, SearchText?:string)
  {
    let params = new HttpParams();
    if (shopParams.brandId != 0) { params = params.append('brandId', shopParams.brandId.toString());}
    if (shopParams.typeId != 0) { params = params.append('typeId', shopParams.typeId.toString());}
    if (shopParams.search) { params = params.append('SearchText', shopParams.search);}

    params = params.append('sort', shopParams.sort);
    params = params.append('PageId', shopParams.PageId.toString());
    params = params.append('rows', shopParams.rows.toString());

    return this.http.get<IProduct[]>(this.baseUrl + 'products',
    {observe: 'response', params})
    .pipe(        //pipe is a wrapper around any rxjs operators
      map(response => {
        return response.body
      })
     );
  }

  getBrands()
  {
    return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
  }

  getTypes()
  {
    return this.http.get<IType[]>(this.baseUrl + 'products/types');
  }
}
