import { Injectable } from '@angular/core';
declare let alertify: any;

@Injectable({
  providedIn: 'root'
})
export class AlertifyService {
  constructor() {}

  confirm(message: string , okCallback: () => {}){
    alertify.confirm(message, (e) => {
      if(e){
        okCallback();
      }
    });
  }

  success(message: string){
    alertify.success(message);
  }

  warning(message: string){
    alertify.warning(message);
  }

  error(message: string){
    alertify.error(message);
  }

  message(message: string){
    alertify.message(message);
  }
} 