import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { PasswordValidation } from '../../helpers/passwordvalidator';
import { AuthService } from '../services/auth.service';
import { FirstRegister } from '../registration/Models/Register';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Alert } from 'src/app/helpers/Alert';
import { AppStateService } from 'src/app/state/app-state.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-first-registration',
  templateUrl: './first-registration.component.html',
  styleUrls: ['./first-registration.component.css']
})
export class FirstRegistrationComponent implements OnInit {
  data : FormGroup;
  alert : Alert;
  model : FirstRegister;
  constructor(private fb:FormBuilder,  private _snackBar: MatSnackBar, private service : AuthService, private appState: AppStateService,    private router: Router    ) {
    this.data = this.fb.group({
      name : ['', Validators.required],
      surname : ['', Validators.required],
      email : ['', Validators.required],
      password : ['', Validators.required],
      confirmPassword: ['', Validators.required]
      }, {
      validator: PasswordValidation.MatchPassword
     }
    );
    this.model = new FirstRegister();
    this.alert = Alert.getInstance(_snackBar);

   }

  ngOnInit(): void {
  }
  async submit(){
    if(!this.data.valid){
      return;
    }
    this.model.firstName = this.data.controls.name.value;
    this.model.lastName = this.data.controls.surname.value;
    this.model.email = this.data.controls.email.value;
    this.model.password = this.data.controls.password.value;

    const userState = await this.service.registerUser(this.model);

    if (userState) {
      this.appState.SetUserState(userState);
      this.router.navigateByUrl(`/user/data`);
    } 
  }

}
