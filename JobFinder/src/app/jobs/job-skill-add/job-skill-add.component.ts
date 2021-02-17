import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-job-skill-add',
  templateUrl: './job-skill-add.component.html',
  styleUrls: ['./job-skill-add.component.css']
})
export class JobSkillAddComponent implements OnInit {
  data : FormGroup;

  constructor(private fb:FormBuilder, private _mdr: MatDialogRef<JobSkillAddComponent>) {
    this.data = this.fb.group({
      name : ['', Validators.required]
      });
   }

  ngOnInit(): void {
  }
  submit(){
    this._mdr.close(this.data.controls.name.value)
  }

}
