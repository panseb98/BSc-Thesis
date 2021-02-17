import { Component, OnInit, HostListener } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { RegistrationComponent } from '../../registration/registration.component';
import { AppStateService } from 'src/app/state/app-state.service';
import {JobsListComponent} from '../../../jobs/jobs-list/jobs-list.component';
import { JobSkillAddComponent } from 'src/app/jobs/job-skill-add/job-skill-add.component';
import { JobTitleAddComponent } from 'src/app/jobs/job-title-add/job-title-add.component';
import { NewKey, NewJob } from '../../Models/RequestsModels';
import { UserService } from '../../services/user.service';
@Component({
  selector: 'app-user-data',
  templateUrl: './user-data.component.html',
  styleUrls: ['./user-data.component.css']
})
export class UserDataComponent implements OnInit {
  expansionMode = false;
  findButtonShow = true;
  @HostListener('window:resize', ['$event'])
    onResize(event) {
        if (window.innerWidth > 790) {
            this.expansionMode = true;
        } else {
            this.expansionMode = false;
        }
    }
  constructor(private dialog: MatDialog, private appState : AppStateService, private service : UserService) {
   }

  ngOnInit() {
  }
  endRegistration(){
    const dialogRef = this.dialog.open(RegistrationComponent, {
      width: this.expansionMode ? '40vw' : '55vw',
      height: '90%'
    });
  }
  async addSkill(){
    let model = new NewKey();
    const dialogRef = this.dialog.open(JobSkillAddComponent);
    dialogRef.afterClosed().subscribe(async res => {
      if ((res == null)) {
      }
      else{
        model.userId = this.appState.UserState.id;
        model.keyName = res;
        const res2 = await this.service.addKeyRequest(model);
        console.log(res2);
      }
    });
  }
  findJobs(){
    this.findButtonShow = false;
  }
  addTitle(){
    let model = new NewJob();

    const dialogRef = this.dialog.open(JobTitleAddComponent);
    dialogRef.afterClosed().subscribe(async res => {
      if ((res == null)) {
        console.log('dupa');
      }
      else{
        model.userId = this.appState.UserState.id;
        model.keyName = res.name;
        model.location = res.location;
        const res2 = await this.service.addJobRequest(model);
        console.log(res2);
      }
    });
  }
}


