import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-test-error',
  templateUrl: './test-error.component.html',
  styleUrls: ['./test-error.component.scss']
})
export class TestErrorComponent implements OnInit {
  baseUrl = environment.apiUrl;
  validationErrors: any; // To display server errors 
  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }
  
  get404Error(){
    this.http.get(this.baseUrl + 'products/45').subscribe
    (response =>{
      console.log(response);
    },error =>{
      console.log(error);
    });
  }

  get500Error(){
    this.http.get(this.baseUrl + 'products/servererror').subscribe
    (response =>{
      console.log(response);
    },error =>{
      console.log(error);
    });
  }

  get400Error(){
    this.http.get(this.baseUrl + 'products/400Error').subscribe
    (response =>{
      console.log(response);
      debugger;
    },error =>{
      console.log(error);
    });
  }

  get400ValidationError(){
    this.http.get(this.baseUrl + 'products/fortyfive').subscribe
    (response =>{
      console.log(response);
    },error =>{
      console.log(error);
      this.validationErrors = error.errors;
    });
  }

}
