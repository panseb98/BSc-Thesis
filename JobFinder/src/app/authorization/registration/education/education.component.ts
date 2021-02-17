import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { FormGroup, FormArray, FormBuilder, FormControl } from '@angular/forms';
import { Skill, Education } from '../Models/Register';
import { DatePipes } from 'src/app/helpers/date.pipe';

@Component({
  selector: 'app-education',
  templateUrl: './education.component.html',
  styleUrls: ['./education.component.css']
})
export class EducationComponent implements OnInit {
  @ViewChild(MatTable, { static: true }) table: MatTable<any>;
  @Input() eduList : FormArray;
  form: FormGroup;
  columns = ['university', 'fieldOfStudy', 'years', 'remove'];
  dataSource: MatTableDataSource<Education>;
  newEmployeeClicked = false;
  constructor(private formBuilder: FormBuilder) {
    this.dataSource = new MatTableDataSource();
   }

  ngOnInit() {
    this.form = this.formBuilder.group({
      id: null,
      universityName: '',
      studyLevel: '',
      fieldOfStudy: '',
      startDate: '',
      endDate: '',
      isNow : false
    });
  }
  private initEduItem(item : Education) : FormGroup{
    return this.formBuilder.group({
      id: null,
      universityName: item.universityName,
      studyLevel: item.studyLevel,
      fieldOfStudy: item.fieldOfStudy,
      startDate: item.startDate,
      endDate: item.endDate,
      isNow : item.isNow
    });
  }
  
  addNewEmployeeBtn() {
    this.newEmployeeClicked = !this.newEmployeeClicked;
  }
  addNewEdu(){

    this.eduList.push(this.initEduItem(this.form.value as Education));
    this.form.reset();
  }

  
  removeAll() {
    this.dataSource.data = [];
  }

  removeAt(index: number) {
    this.eduList.removeAt(index);    
  }

}
