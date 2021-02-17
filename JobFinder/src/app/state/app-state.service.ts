import { Injectable } from '@angular/core';
import { UserState } from './Models/UserState';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AppStateService {
  private userLogged = new BehaviorSubject<UserState>(null);

  constructor() {
    this.extractUserData();
   }

  isLogged(): boolean{
      
    if(this.userLogged.value)
      return true;

    return false;
  }

  extractUserData() {
    const userState = JSON.parse(localStorage.getItem('userState')) as UserState;
    const stateDate = new Date(localStorage.getItem('stateCreation'));
    if (!userState) { return; }
    if (!stateDate) { return; }
    const hourDiff = Math.abs(new Date().valueOf() - new Date(localStorage.getItem('stateCreation')).valueOf()) / 3600000;
    if(userState && hourDiff < 12) {
      this.SetUserState(userState);
    }
  }

  public get UserState(): UserState {
    return this.userLogged.value;
  }

  public SetUserState(state: UserState) {
    console.log(state);
    if (!state) {
      console.log(state);
      localStorage.removeItem('userState');
      localStorage.removeItem('stateCreation');
      this.userLogged.next(state);

      //return;
    }
    localStorage.setItem('userState', JSON.stringify(state));
    localStorage.setItem('stateCreation', new Date().toUTCString());
    this.userLogged.next(state);
  }
  canDisplay(permission: string[]): boolean {
    if (!this.userLogged.value) {
      return false;
    }
    const exists = this.userLogged.value
                        .permissions
                        .some(r => permission.indexOf(r.key) >= 0 ||
                                   permission.indexOf(r.value) >= 0);
    if(exists) {
      return true;
    } else {
      return false;
    }
  }
}
