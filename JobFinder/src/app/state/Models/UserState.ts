import { Permission } from './Permission';

export class UserState {
    id: string;
    username: string;
    isLoggedIn: boolean;
    email: string;
    token: string;
    permissions: Array<Permission>;

    constructor(
        isLoggedIn?: boolean,
        permissions?: Array<Permission>,
        username?: string,
        email?: string,
        token?: string,
        id?: string
      ) {
        this.isLoggedIn = isLoggedIn ? isLoggedIn : false;
        this.permissions = permissions ? permissions : new Array<Permission>();
        this.username = username ? username : null;
        this.email = email ? email : null;
        this.token = token ? token : null;
        this.id = id ? id : null
      }
    
}