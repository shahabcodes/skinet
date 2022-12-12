import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { IProduct } from './models/products';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export default class AppComponent implements OnInit {
  title = 'SkiNet';
  products: IProduct[];  //Normal JavaScript to store arra

  constructor(private http: HttpClient) {  }

  ngOnInit(): void {
    this.http.get('https://localhost:7034/api/products?rows=20').subscribe(
      (response: IProduct[]) => {
      this.products = response;
      console.log(response);
    }, error => {
      console.log(error);
    } )
  }

}
