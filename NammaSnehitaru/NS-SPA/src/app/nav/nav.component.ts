import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/Alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any  = {};

  constructor(public authService: AuthService, private alertify: AlertifyService,
             private routeMeService: Router) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(
      next => {
        this.alertify.success('Logged In Successfully');
      },
      error => {
        this.alertify.error(error);
      },
      () => {
        this.routeMeService.navigate(['/members']);
      }
    );
  }

  isLoggedIn() {
    // const token = localStorage.getItem('token');
    // return !!token;
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    this.alertify.success('Logout Successfully');
    this.routeMeService.navigate(['/home']);
  }

}
