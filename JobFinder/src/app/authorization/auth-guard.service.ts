import { Injectable } from '@angular/core';
import { AppStateService } from '../state/app-state.service';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private appState: AppStateService, private router: Router) { }
  path: ActivatedRouteSnapshot[];
  route: ActivatedRouteSnapshot;
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if(this.appState.UserState && (route.routeConfig.path === '' || route.routeConfig.path === 'auth')){
      this.router.navigateByUrl('user/data');
      console.log("dupa");
      return false;
    }
    if (!this.appState.UserState) {
      this.router.navigateByUrl('/auth');
      return true;
    }
    return true;
   
  }
}
