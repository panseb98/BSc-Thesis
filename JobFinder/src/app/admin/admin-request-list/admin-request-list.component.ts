import { Component, OnInit } from '@angular/core';
import { AllRequests } from '../Models/AllRequests';
import { UserService } from 'src/app/authorization/services/user.service';

@Component({
  selector: 'app-admin-request-list',
  templateUrl: './admin-request-list.component.html',
  styleUrls: ['./admin-request-list.component.css']
})
export class AdminRequestListComponent implements OnInit {
  dataLoad : boolean;
  mainData : AllRequests;
  constructor(private service : UserService) { }
  displayedColumnsKey = ['position', 'name', 'date', 'cancel', 'accept'];
  displayedColumnsJob = ['position', 'name', 'location', 'date', 'cancel', 'accept'];

  async ngOnInit(){
    this.mainData = await this.service.getRequestList();
    console.log(this.mainData);
    this.dataLoad = true;
  }
  async add(id : number){
    console.log(id);
    const res = await this.service.addNewKey(id);
    this.mainData = await this.service.getRequestList();
  }
  async remove(id : number){
    console.log(id);
    const res = await this.service.removeNewKey(id);
    this.mainData = await this.service.getRequestList();
  }
  async addJob(id : number){
    console.log(id);
    const res = await this.service.addNewJobOffers(id);
    this.mainData = await this.service.getRequestList();
  }
  async removeJob(id : number){
    console.log(id);
    const res = await this.service.removeNewJob(id);
    this.mainData = await this.service.getRequestList();
  }
  async updateSkills(){
    const res = await this.service.updateJobSkills();
  }

}


