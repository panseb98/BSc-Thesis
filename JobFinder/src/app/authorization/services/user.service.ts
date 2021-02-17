import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { ResponseModel } from 'src/app/helpers/Models/ResponseModel';
import { SkillModel } from '../Models/SkillModel';
import { Experience, Register } from '../registration/Models/Register';
import { RecommendedJobModel } from 'src/app/jobs/Models/RecommendedJobModel';
import { NewKey, NewJob } from '../Models/RequestsModels';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Alert } from 'src/app/helpers/Alert';
import { NotifierService } from 'angular-notifier';
import { AllRequests } from 'src/app/admin/Models/AllRequests';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, private notify : NotifierService) { }

  finSkillsAsync(key: string): Promise<Array<SkillModel>> {
    const params = new HttpParams().append('key', key);
    return new Promise((res, rej) => {
      return this.http
        .get<ResponseModel<Array<SkillModel>>>(
          `${environment.api}/Jobs/GetSkillsByKey`,
          { params }
        )
        .subscribe((x) => {console.log(x); res(x.data)});
    });
  }

  getUserExpendedData(userId : string) : Promise<Register>{
    const params = new HttpParams().append('userId', userId);
    return new Promise((res, rej) => {
      return this.http
        .get<ResponseModel<Register>>(
          `${environment.api}/Jobs/GetExpendedUserData`,
          { params }
        )
        .subscribe((x) => {console.log(x); res(x.data)});
    });  
  }
  getRecommendedJobs(userId : string) : Promise<Array<RecommendedJobModel>>{
    const params = new HttpParams().append('userId', userId);
    return new Promise((res, rej) => {
      return this.http
        .get<ResponseModel<Array<RecommendedJobModel>>>(
          `${environment.api}/Jobs/GetRecommendedJobs`,
          { params }
        )
        .subscribe((x) => {console.log(x); res(x.data)});
    });  
  }
  
  addUserExpendedData(model : Register) : Promise<boolean>{
    console.log(model);
    return new Promise((res, rej) => {

      return this.http
        .post<ResponseModel<boolean>>(
          `${environment.api}/Jobs/AddUserExpendedData`,
          model
        ).subscribe(
          (x) => {
            if (x.succeeded) {
              this.notify.notify('success', 'Poprawnie uaktualniono dane!');
            } else {
              this.notify.notify('error', x.message);
              res(false);
            }
          },
          (err: HttpErrorResponse) => {
            res(false);
          }
        );
    });
  }
  addKeyRequest(model : NewKey) : Promise<boolean>{
    console.log(model);
    return new Promise((res, rej) => {

      return this.http
        .post<ResponseModel<boolean>>(
          `${environment.api}/Jobs/AddKeyRequest`,
          model
        ).subscribe(
          (x) => {
            if (x.succeeded) {
              this.notify.notify('success', 'Poprawnie wysłano formularz!');
              res(x.data)
            } else {
              this.notify.notify('error', x.errors.join(''));
              res(false);
            }
          },
          (err: HttpErrorResponse) => {
            this.notify.notify('error', 'Ups, coś poszło nie tak...');
            res(false);
          }
        );
    });
  }

  addJobRequest(model : NewJob) : Promise<boolean>{
    console.log(model);
    return new Promise((res, rej) => {

      return this.http
        .post<ResponseModel<boolean>>(
          `${environment.api}/Jobs/addJobRequest`,
          model
        ).subscribe(
          (x) => {
            if (x.succeeded) {
              this.notify.notify('success', 'Poprawnie wysłano formularz!');
              res(x.data)
            } else {
              this.notify.notify('error', x.errors.join(''));
              res(false);
            }
          },
          (err: HttpErrorResponse) => {
            this.notify.notify('error', 'Ups, coś poszło nie tak...');
            res(false);
          }
        );
    });
  }
  
  getAllJobsTitles(key : string) : Promise<Array<string>>{
    const params = new HttpParams().append('key', key);
    return new Promise((res, rej) => {
      return this.http
        .get<ResponseModel<Array<string>>>(
          `${environment.api}/Jobs/GetAllJobNames`,{params}
        )
        .subscribe((x) => {console.log(x); res(x.data)});
    });  
  }

  getAllSkills() : Promise<Array<string>>{
    return new Promise((res, rej) => {
      return this.http
        .get<ResponseModel<Array<string>>>(
          `${environment.api}/Jobs/GetAllSkills`
        )
        .subscribe((x) => {console.log(x); res(x.data)});
    });  
  }

  getRequestList() : Promise<AllRequests>{
    return new Promise((res, rej) => {
      return this.http
        .get<ResponseModel<AllRequests>>(
          `${environment.api}/Jobs/GetAllRequests`
        )
        .subscribe((x) => {console.log(x); res(x.data)});
    });  
  }
  addNewKey(id : number) : Promise<boolean>{
    const params = new HttpParams()
      .append('recordId', id.toString());
    return new Promise((res, rej) => {

      return this.http
        .put<ResponseModel<boolean>>(
          `${environment.api}/Jobs/AddNewKey`,null,
          {params}
        ).subscribe(
          (x) => {
            if (x.succeeded) {
              this.notify.notify('success', 'Poprawnie dodano nowy klucz!');
              res(x.data)
            } else {
              this.notify.notify('error', x.message);
              res(false);
            }
          },
          (err: HttpErrorResponse) => {
            this.notify.notify('error', 'Ups, coś poszło nie tak...');
            res(false);
          }
        );
    });
  }
  removeNewKey(id : number) : Promise<boolean>{
    console.log(id);
    const params = new HttpParams()
                                .append('recordId', id.toString());
    return new Promise((res, rej) => {

      return this.http
        .put<ResponseModel<boolean>>(
          `${environment.api}/Jobs/RemoveNewKey`,null,
          {params}
        ).subscribe(
          (x) => {
            if (x.succeeded) {
              this.notify.notify('success', 'Poprawnie usunięto klucz!');
              res(x.data)
            } else {
              this.notify.notify('error', x.message);
              res(false);
            }
          },
          (err: HttpErrorResponse) => {
            this.notify.notify('error', 'Ups, coś poszło nie tak...');
            res(false);
          }
        );
    });
  }

  addNewJobOffers(id : number){
    this.notify.getConfig().behaviour.autoHide = false;

    this.notify.notify('info', 'Czekaj, trwa dodawanie nowych ofert...');

    const params = new HttpParams()
      .append('recordId', id.toString())
    return new Promise((res, rej) => {
      return this.http
        .put<ResponseModel<boolean>>(
          `${environment.api}/Jobs/AddNewJobs`,null,
          {params}
        ).subscribe(
          (x) => {
            if (x.succeeded) {
              this.notify.getConfig().behaviour.autoHide = 2000;
              this.notify.hideAll();
              this.notify.notify('success', 'Poprawnie dodano nowe oferty pracy!');
              res(x.data)
            } else {
              this.notify.getConfig().behaviour.autoHide = 2000;
              this.notify.hideAll();
              this.notify.notify('error', x.message);
              res(false);
            }
          },
          (err: HttpErrorResponse) => {
            this.notify.getConfig().behaviour.autoHide = 2000;
            this.notify.hideAll();
            this.notify.notify('error', 'Ups, coś poszło nie tak...');
            res(false);
          }
        );
    });
  }

  removeNewJob(id : number): Promise<boolean>{
    const params = new HttpParams()
                                .append('recordId', id.toString());
    return new Promise((res, rej) => {

      return this.http
        .put<ResponseModel<boolean>>(
          `${environment.api}/Jobs/RemoveNewJob`,null,
          {params}
        ).subscribe(
          (x) => {
            if (x.succeeded) {
              this.notify.notify('success', 'Poprawnie usunięto ofertę!');
              res(x.data)
            } else {
              this.notify.notify('error', x.message);
              res(false);
            }
          },
          (err: HttpErrorResponse) => {
            this.notify.notify('error', 'Ups, coś poszło nie tak...');
            res(false);
          }
        );
    });
  }

  updateJobSkills(){
    return new Promise((res, rej) => {
      return this.http
        .get<ResponseModel<boolean>>(
          `${environment.api}/Jobs/UpdateJobSkills`
        )
        .subscribe(
          (x) => {
            if (x.succeeded) {
              this.notify.notify('success', 'Poprawnie zaktualizowano klucze!');
              res(x.data)
            } else {
              this.notify.notify('error', x.message);
              res(false);
            }
          },
          (err: HttpErrorResponse) => {
            this.notify.notify('error', 'Ups, coś poszło nie tak...');
            res(false);
          }
        );
    });  
  }

  getJobsFromCV(userId : string) : Promise<Array<Experience>>{
    const params = new HttpParams().append('userId', userId);
    return new Promise((res, rej) => {
      return this.http
        .get<ResponseModel<Array<Experience>>>(
          `${environment.api}/Jobs/GetJobsFromCV`,
          { params }
        )
        .subscribe((x) => {console.log(x); res(x.data)});
    });  
  }
}
