import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/products';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product: IProduct;

  constructor(private shopService: ShopService, private activateRoute: ActivatedRoute, 
    private bcService: BreadcrumbService) {
      this.bcService.set('@productDetails','');   // keeping it empty before loading
     }

  ngOnInit(): void {
    this.loadProduct();
  }

  loadProduct(){
    this.shopService.getProduct(+this.activateRoute.snapshot.paramMap.get('id'))   //+ because we are passing a number here
    .subscribe(product => {
      console.log(product);
      this.product = product;
      this.bcService.set('@productDetails', product.name);
    }, error => {
      console.log(error)
    });
  }

}
