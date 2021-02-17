import { Element } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { Experience } from 'src/app/authorization/registration/Models/Register';
import { UserService } from 'src/app/authorization/services/user.service';
import { AppStateService } from 'src/app/state/app-state.service';
import { JobSkillAddComponent } from '../job-skill-add/job-skill-add.component';

@Component({
  selector: 'app-job-title-add',
  templateUrl: './job-title-add.component.html',
  styleUrls: ['./job-title-add.component.css']
})
export class JobTitleAddComponent implements OnInit {

  displayedColumns = ['position', 'name', 'location', 'symbol'];
  dataSource = new MatTableDataSource<Experience>();
  isEmptyList = false;

  constructor(private fb:FormBuilder, private _mdr: MatDialogRef<JobSkillAddComponent>, private service :  UserService, private appState : AppStateService) {

   }

  async ngOnInit() {
    const userId = this.appState.UserState.id
    const res = await this.service.getJobsFromCV(userId);
    if(res.length == 0){
      this.isEmptyList = true;
    }
    this.dataSource.data = res;
  }
  submit(res : Experience){
    let element = {name: res.positionName, location : res.description};
    this._mdr.close(element);
  }

}

