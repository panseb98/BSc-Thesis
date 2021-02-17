import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { Education, Experience } from '../Models/Register';
import { FormGroup, FormArray, FormBuilder } from '@angular/forms';
import { MatTableDataSource, MatTable } from '@angular/material/table';
import { AppStateService } from 'src/app/state/app-state.service';

@Component({
  selector: 'app-expericence',
  templateUrl: './expericence.component.html',
  styleUrls: ['./expericence.component.css']
})
export class ExpericenceComponent implements OnInit {

  @ViewChild(MatTable, { static: true }) table: MatTable<any>;
  @Input() expList : FormArray;
  userId : string;
  form: FormGroup;
  columns = ['university', 'fieldOfStudy', 'years', 'remove'];
  dataSource: MatTableDataSource<Education>;
  newEmployeeClicked = false;
  constructor(private formBuilder: FormBuilder, private appState : AppStateService) {
    this.userId = appState.UserState.id;
    this.dataSource = new MatTableDataSource();
   }

  ngOnInit() {
    this.form = this.formBuilder.group({
      id: null,
      companyName: '',
      positionName: '',
      description: '',
      startDate: '',
      endDate: '',
      isNow : false
    });
  }
  private initExpItem(item : Experience) : FormGroup{
    return this.formBuilder.group({
      id: null,
      companyName: item.companyName,
      positionName: item.positionName,
      description: item.description,
      startDate: item.startDate,
      endDate: item.isNow ? null : item.endDate,
      isNow : item.isNow
    });
  }
  
  addNewEmployeeBtn() {
    this.newEmployeeClicked = !this.newEmployeeClicked;
  }
  addNewEdu(){

    this.expList.push(this.initExpItem(this.form.value as Experience));
    this.form.reset();
  }

  
  removeAll() {
    this.dataSource.data = [];
  }

  removeAt(index: number) {
    this.expList.removeAt(index);    
  }

}
