export interface UserModel {
    id: string;
    username: string;
    email: string;
    role: string;
    token: string;
    firstName: string;
    lastName: string;
    claim: {Key: string, Value: string}
  }