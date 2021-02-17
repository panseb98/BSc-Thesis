import { Component, OnInit, Input, ElementRef, ViewChild } from '@angular/core';
import {COMMA, ENTER} from '@angular/cdk/keycodes';
import { Skill } from '../Models/Register';
import { FormGroup, FormControl, FormArray, FormBuilder } from '@angular/forms';
import {startWith, map, debounceTime, switchMap} from 'rxjs/operators';
import { Observable } from 'rxjs';
import { UserService } from '../../services/user.service';
import { SkillModel } from '../../Models/SkillModel';

@Component({
  selector: 'app-skills',
  templateUrl: './skills.component.html',
  styleUrls: ['./skills.component.css']
})
export class SkillsComponent implements OnInit {
  @Input() data : FormArray;
  form: FormControl;
  value: string;
  selectable = true;
  removable = true;
  @ViewChild('inspectorInput', { static: true }) inspectorInput: ElementRef<HTMLInputElement>;
  isFocussed: boolean;
  public filteredOptions;
  separatorKeysCodes: number[] = [ENTER, COMMA];
  filteredList : Array<SkillModel> = new Array<SkillModel>();

  constructor(private formBuilder: FormBuilder, private userService : UserService) {

    this.form = new FormControl('');

    this.filteredOptions = this.form.valueChanges
      .pipe(
        debounceTime(100),
        startWith(''),
        switchMap((v) =>
                typeof v === 'string'
                    ? v !== ''
                        ? this.userService.finSkillsAsync(v)
                        : this.userService.finSkillsAsync('')
                    : this.userService.finSkillsAsync('')
            )
      );

  }
  ngOnInit(){
  
     
  }
  getList() : Observable<SkillModel[]>
  {
    console.log('du[a');
    return Observable.create((ob) => {ob.next(this.filteredList)});
  }
  add(event/*: MatChipInputEvent*/){
    const input = event.input;
    const value = event.value;  
    this.form.setValue(null);
  }

  remove(inspectorId: number): void {
    console.log(inspectorId);
    console.log(this.data);
    const index = this.data.value.findIndex(x => x.skillId === inspectorId);
    if (index >= 0) {
      this.data.removeAt(index);
    }
  }
  selected(event/*: MatAutocompleteSelectedEvent*/){
    const conData = this.data.value as Array<SkillModel>;
    const normData = event.option.value;
    if(conData.findIndex(x => x.skillId === event.option.value.skillId) === -1) {
      this.data.push(this.initSkillItem(normData as SkillModel));
    } 
    console.log('bbbbb');

    this.form.setValue(null);
    console.log('aaaaa');
  }
  private initSkillItem(item : SkillModel) : FormGroup{
    return this.formBuilder.group({
      skillId: item.skillId,
      skillName: item.skillName,
    });
  }
  private _filter2(value : string): Observable<SkillModel[]> {
    if(typeof value === 'string'){
      const filterValue = value.toLowerCase();
      const da =  this.filteredList.filter(item => item.skillName.toLowerCase().includes(value.toLowerCase()));
      return Observable.create((ob) => {ob.next(da)});
    }
    
    return this.getList();
  }

}
