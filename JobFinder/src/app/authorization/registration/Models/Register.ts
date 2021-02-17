import { SkillModel } from '../../Models/SkillModel';

export class Register{
   // person : PersonalData;
    userId : number;
    educations : Array<Education>;
    experiences : Array<Experience>;
    skills : Array<SkillModel>;
}
export class FirstRegister{
    firstName : string;
    lastName: string;
    email: string;
    password : string;
}
export class Education{
    id : number;
    universityName : string;
    fieldOfStudy : string;
    studyLevel : string;
    startDate : Date;
    endDate : Date;
    isNow : boolean;
}
export class Experience{
    id : number;
    companyName : string;
    positionName : string;
    description: string;
    startDate : Date;
    endDate : Date;
    isNow : boolean;
}
export class Skill{
    id : number;
    key: string;
}