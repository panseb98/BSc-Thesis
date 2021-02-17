import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { UserService } from 'src/app/authorization/services/user.service';

@Component({
  selector: 'app-jobs-title-list',
  templateUrl: './jobs-title-list.component.html',
  styleUrls: ['./jobs-title-list.component.css']
})
export class JobsTitleListComponent implements OnInit {

  constructor(private service : UserService) { }

  async ngOnInit() {
    const res = await this.service.getAllJobsTitles('');
    this.dataSource = new MatTableDataSource(res); 
  }
  displayedColumns = ['position', 'name'];
  dataSource;

  async applyFilter(filterValue: string) {
    const res = await this.service.getAllJobsTitles(filterValue);
    
    this.dataSource = new MatTableDataSource(res);
  }
}
