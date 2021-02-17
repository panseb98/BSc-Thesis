import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { UserService } from 'src/app/authorization/services/user.service';

@Component({
  selector: 'app-jobs-skills-list',
  templateUrl: './jobs-skills-list.component.html',
  styleUrls: ['./jobs-skills-list.component.css']
})
export class JobsSkillsListComponent implements OnInit {

  constructor(private service : UserService) { }

  async ngOnInit() {
    const res = await (await this.service.finSkillsAsync(''));
    this.dataSource = new MatTableDataSource(res); 
  }
  displayedColumns = ['position', 'name'];
  dataSource;

  async applyFilter(filterValue: string) {

    const res = await this.service.finSkillsAsync(filterValue);
    
    this.dataSource = new MatTableDataSource(res);
  }
  

}

