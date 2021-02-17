import { Injectable } from '@angular/core';
import { Register, FirstRegister } from '../registration/Models/Register';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { ResponseModel } from 'src/app/helpers/Models/ResponseModel';
import { UserState } from 'src/app/state/Models/UserState';
import { UserModel } from '../Models/UserModel';
import { Observable } from 'rxjs';
import { Permission } from 'src/app/state/Models/Permission';
import { LoginModel } from '../Models/LoginModel';
import { Router } from '@angular/router';
import { AppStateService } from 'src/app/state/app-state.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http: HttpClient,     private router: Router,     private appState: AppStateService    ) { }

  registerUser(user: FirstRegister): Promise<UserState>{
    return new Promise((res, rej) => {

      return this.http
        .post<ResponseModel<UserModel>>(
          `${environment.api}/Authorization/RegisterNewUser`,
          user
        ).subscribe(
          (x) => {
            if (x.succeeded) {
              const values = Object.values(x.data.claim);
              const perms = Object.getOwnPropertyNames(x.data.claim).map(
                (x, i, a) => new Permission(x, values[i])
              );
              res(
                new UserState(
                  true,
                  perms,
                  x.data.username,
                  x.data.email,
                  x.data.token,
                  x.data.id
                )
              );
            } else {
             // this.notifierService.notify('error', x.message);
              res(null);
            }
          },
          (err: HttpErrorResponse) => {
            res(null);
          }
        );
    });
  }

  loginUser(user: LoginModel): Promise<UserState>{
    return new Promise((res, rej) => {

      return this.http
        .post<ResponseModel<UserModel>>(
          `${environment.api}/Authorization/LoginUser`,
          user
        ).subscribe(
          (x) => {
            if (x.succeeded) {
              const values = Object.values(x.data.claim);
              const perms = Object.getOwnPropertyNames(x.data.claim).map(
                (x, i, a) => new Permission(x, values[i])
              );
              console.log(x);
              res(
                new UserState(
                  true,
                  perms,
                  x.data.username,
                  x.data.email,
                  x.data.token,
                  x.data.id
                )
              );
            } else {
             // this.notifierService.notify('error', x.message);
              res(null);
            }
          },
          (err: HttpErrorResponse) => {
            res(null);
          }
        );
    });
  }
  logout() {
    //this.http.post(`${environment.api}/Authorization/Logout`, {});
    this.router.navigateByUrl('/auth');
    this.appState.SetUserState(null);
  }
}
