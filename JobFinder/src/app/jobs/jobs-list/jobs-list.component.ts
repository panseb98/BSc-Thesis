import { Component, OnInit, Input } from '@angular/core';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { UserService } from 'src/app/authorization/services/user.service';
import { RecommendedJobModel } from '../Models/RecommendedJobModel';


@Component({
  selector: 'app-jobs-list',
  templateUrl: './jobs-list.component.html',
  styleUrls: ['./jobs-list.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class JobsListComponent implements OnInit {
  @Input() userId : string;
  columnsToDisplay = ['jobTitle', 'jobShortDesc', 'jobRaiting'];
  expandedElement: RecommendedJobModel | null;
  dataSource;
  isEmpty =false;
  constructor(private userService : UserService) { 
  }

  async ngOnInit() {
    
    const res = await this.userService.getRecommendedJobs(this.userId);
    if(!res){
      this.isEmpty = true;
    }
    this.dataSource = res;
  }

}
