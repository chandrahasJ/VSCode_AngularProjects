import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthService } from '../_services/auth.service';
import { AlertifyService } from '../_services/Alertify.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model: any = {};
  @Input() RegisterValue: any = {};
  @Output() cancelRegisterMode = new EventEmitter();
  constructor(private authService: AuthService,
              private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register() {
    this.authService.register(this.model).subscribe(() => {
        this.alertify.success('Registered Successfully');
    },
    error => {
      this.alertify.error(error);
    })
  }

  cancel() {
      this.cancelRegisterMode.emit(false);
    console.log('Cancelled');
  }
}
