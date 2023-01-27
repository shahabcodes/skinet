import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/products';
import { IType } from '../shared/models/productType';
import { ShopParams } from '../shared/models/shopParams';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  // Set it to false if "search" div has some directives for eg: ngIf, set it to true when there is no dynamic activity
  @ViewChild("search", { static: false }) searchTerm: ElementRef 
  products: IProduct[];
  brands: IBrand[];
  types: IType[];
  totalCount = 0;
  shopParams = new ShopParams();
  sortOptions = [
    { name: "A - Z", value: "nameAsc" },
    { name: "Z - A", value: "nameDesc" },
    { name: "Price: Low to High", value: "priceAsc" },
    { name: "Price: High to Low", value: "priceDesc" },
  ];

  constructor(private shopService: ShopService) { }

  ngOnInit(): void {
    this.getBrands(); 
    this.getTypes();
    this.getProducts();
  }

  getProducts() {
    if (this.shopParams.PageId > 0) { this.shopParams.PageId = this.shopParams.PageId - 1; }
    this.shopService.getProducts(this.shopParams)
      .subscribe    // always unsubscribe, note in case of http request angular is smart enough to unsubscribe autumatically
      (response => {
        if (response.length === 0) { this.totalCount = 0; }
        this.products = response;
        if (response.length > 0) {
          this.shopParams.PageId = this.products[0].pageNum + 1;
          this.shopParams.rows = this.products[0].rows;
          debugger;
          this.totalCount = this.products[0].totalRows;
        }

        console.log(response);
      },
        error => {
          console.log(error);
        })
  }

  getBrands() {
    this.shopService.getBrands().subscribe
      (response => {
        this.brands = [{ id: 0, name: 'All' }, ...response];
        console.log(response);
      },
        error => {
          console.log(error)
        }
      );
  }

  getTypes() {
    this.shopService.getTypes().subscribe
      (response => {
        this.types = [{ id: 0, name: 'All' }, ...response];
        console.log(response);
      },
        error => {
          console.log(error)
        }
      );
  }

  onBrandSelected(brandId: number) {
    this.shopParams.PageId = 0;
    this.shopParams.brandId = brandId;
    this.getProducts();
  }

  onTypeSelected(typeId: number) {
    this.shopParams.PageId = 0;
    this.shopParams.typeId = typeId;
    this.getProducts();
  }


  onSortSelected(sort: string) {
    this.shopParams.PageId = 0;
    this.shopParams.sort = sort;
    this.getProducts();
  }

  onPageChanged(event: any)
  {
    if (this.shopParams.PageId !== event){
      this.shopParams.PageId = event;
      this.getProducts();
    }
  }

  onSearch(){
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.getProducts();
  }

  onReset(){
    this.searchTerm.nativeElement.value = '';
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
