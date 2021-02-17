import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { NotifierService } from 'angular-notifier';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { AppStateService } from 'src/app/state/app-state.service';
import { LoginModel } from '../Models/LoginModel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form: FormGroup;
  model: LoginModel;
  constructor(private fb:FormBuilder,  private notifier: NotifierService, private _snackBar: MatSnackBar, private service : AuthService,  private appState: AppStateService,    private router: Router) {
    this.form = this.fb.group({
      username : ['', [Validators.required, Validators.email]],
      password : ['', Validators.required]
    });
    this.model = new LoginModel();
   }

  ngOnInit() {
  }

  async submit() {

    if(!this.form.valid){
      return;
    }

    this.service.loginUser(this.model);
    this.model.email = this.form.controls.username.value;
    this.model.password = this.form.controls.password.value;
    const userState = await this.service.loginUser(this.model);

    if (userState) {
      this.appState.SetUserState(userState);
      this.router.navigateByUrl(`/user/data`);
    } 

  }

}
