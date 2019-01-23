import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HTTP_INTERCEPTORS,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(error => {
        //   Check Error Type of HttpErrorResponse
        if (error instanceof HttpErrorResponse) {
          //    Check 401 Bad Request
            if (error.status === 401) {
                return throwError('You are Unauthorized.');
            }
            //    Check application Error such as 500 Iternal server error
            const applicationError = error.headers.get('Application-Message');
            if (applicationError) {
                // console.error(applicationError);
                return throwError(applicationError);
            }
            //     Model State not valid Errors
            const serverError = error.error;
            let modelStateErrors = '';
            if (serverError && typeof serverError === 'object') {
                for (const key in serverError) {
                if (serverError[key]) {
                    modelStateErrors += serverError[key] + '\n';
                }
                }
            }
            return throwError(modelStateErrors || serverError || 'Server Error');
        }
      })
    );
  }
}

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true
};
