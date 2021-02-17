import { NewKey, NewJob } from 'src/app/authorization/Models/RequestsModels';

export class AllRequests{
    keyRequests : Array<NewKey>;
    jobRequests : Array<NewJob>;
}
