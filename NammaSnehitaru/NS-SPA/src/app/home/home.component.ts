import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registeMode = false;
  values: any = {};
  BaseUrl = 'http://localhost:5000/api/values';

  constructor(private http: HttpClient) { }

  ngOnInit() {
    //this.getValues();
  }

  registerToggler() {
    this.registeMode = !this.registeMode;
  }

  getValues() {
    this.http.get(this.BaseUrl).subscribe(Response => {
      this.values = Response;
    },
    error => {
      console.log('Error Occured');
    });
  }

  cancelRegisterMode(registerMode: boolean){
    this.registeMode = registerMode;
  }
}