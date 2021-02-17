import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Skill } from '../Models/Register';
import { SkillModel } from '../../Models/SkillModel';

@Component({
  selector: 'app-submit',
  templateUrl: './submit.component.html',
  styleUrls: ['./submit.component.css']
})
export class SubmitComponent implements OnInit {
  @Input() fullForm : FormGroup;

  constructor() { 
    console.log(this.fullForm);
  }

  ngOnInit() {
    console.log(this.fullForm);

  }
  getSkills() : string{
   let a = this.fullForm.get('skills').value as Array<SkillModel>;
   return a.map( x => x.skillName).join(', ');
  }

}
