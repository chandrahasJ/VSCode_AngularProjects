import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt'


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  BaseUrl = 'http://localhost:5000/api/auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any = {};

  constructor(private http: HttpClient) { 
    console.log('AuthService :> '+ this.decodedToken);
  }

  login(model: any){
    return this.http.post(this.BaseUrl + 'login',model).pipe(
        map(
           (Response :any) => {
              const user = Response;
              if(user) {
                localStorage.setItem('token', user.token);
               // this.decodedToken = this.jwtHelper.decodeToken(user.token);
                console.log(this.decodedToken);
              }
           })
    );
  }

  register(model: any){
    return this.http.post(this.BaseUrl+'register', model);
  }

  loggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }
}
