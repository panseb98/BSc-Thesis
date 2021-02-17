import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { AppStateService } from '../state/app-state.service';
import { MatSidenav } from '@angular/material/sidenav';
import { Router, NavigationEnd } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { RegistrationComponent } from '../authorization/registration/registration.component';
import { AuthService } from '../authorization/services/auth.service';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {
  @ViewChild('sidenav', { static: true }) sidenav: MatSidenav;
  expansionMode = false;
  @HostListener('window:resize', ['$event'])
  onResize(event) {
      if (window.innerWidth > 790) {
          this.expansionMode = true;
      } else {
          this.expansionMode = false;
      }
  }
  activated ="";
  constructor(private dialog: MatDialog, public appState: AppStateService, 
    private router: Router, private service : AuthService
   ) {
    console.log(appState.UserState);
    this.router.events.subscribe(nav => {
      if(nav instanceof NavigationEnd) {
        this.activated = nav.url.split('/')[1]
      }
    })
    }
  logout() {
    this.service.logout();
  }

  endRegistration(){
    const dialogRef = this.dialog.open(RegistrationComponent, {
      width: this.expansionMode ? '40vw' : '55vw',
      height: '90%'
    });
  }

  ngOnInit() {
  }

}
