import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl, FormArray, AbstractControl } from '@angular/forms';
import { Sort } from '@angular/material/sort';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { Register, Experience, Education, Skill } from './Models/Register';
import { UserService } from '../services/user.service';
import { AppStateService } from 'src/app/state/app-state.service';
import { SkillModel } from '../Models/SkillModel';


@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  isLoaded =false;
  registerForm : FormGroup;
  model : Register;
  constructor(private fb:FormBuilder, private userService : UserService, private appState : AppStateService) { 


  }

  async ngOnInit() {
    this.model = await this.userService.getUserExpendedData(this.appState.UserState.id);
    this.createForm(this.model);
  }
  createForm(createModel : Register){
    console.log(createModel);
    if(createModel){
      this.registerForm = this.fb.group({
        educations :this.initEducation(createModel.educations),
        experiences : this.initExperience(createModel.experiences),
        skills : this.initSkill(createModel.skills)
      });
      this.isLoaded = true;
    }
    else{
      this.registerForm = this.fb.group({
        experienceList : new FormArray([]),
        eduList :new FormArray([]),
        skills : new FormArray([])
      });
      this.isLoaded = true;
    }
  }

  private initSkill(skills : Array<SkillModel>){
    let skillForm = new FormArray([]);
    console.log(skillForm);
    if(skills){
      console.log(skillForm);
      skills.forEach(x => skillForm.push(this.initSkillItem(x)))
    }

    return skillForm;
  }
  private initSkillItem(item : SkillModel) : FormGroup{
    return this.fb.group({
      skillId: item.skillId,
      skillName: item.skillName,
    });
  }

  private initEduItem(item : Education) : FormGroup{
    return this.fb.group({
      id: null,
      universityName: item.universityName,
      eduLevel: item.studyLevel,
      fieldOfStudy: item.fieldOfStudy,
      startDate: item.startDate,
      endDate: item.endDate,
      isCurrent : item.isNow
    });
  }
  private initEducation(education : Array<Education>){
    let eduForm = new FormArray([])
    if(education){
      console.log(education);
      education.forEach(x => eduForm.push(this.initEduItem(x)))
    }

    return eduForm;
  }

  private initExperience(experience: Array<Experience>){
    let expForm = new FormArray([])
    if(experience){
      console.log(experience);
      experience.forEach(x => expForm.push(this.initExpItem(x)))
    }

    return expForm;
  }
  private initExpItem(item : Experience) : FormGroup{
    return this.fb.group({
      id: null,
      companyName: item.companyName,
      positionName: item.positionName,
      description: item.description,
      startDate: item.startDate,
      endDate: item.endDate,
      isNow : item.isNow
    });
  }


  register(){
    let finalModel = this.registerForm.value as Register;
    finalModel.userId = Number(this.appState.UserState.id);
    console.log(finalModel);
    this.userService.addUserExpendedData(finalModel);
  }
}