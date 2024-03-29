export class Credentials {

    constructor(
        public userName?: string,
        public password?: string
    ) {
    }

}

export class ChangePasswordModel {

    constructor(
        public currentPassword?: string,
        public newPassword?: string,
        public confirmNewPassword?: string
    ) {

    }

    toJSON() {
        return {
            currentPassword: this.currentPassword,
            newPassword: this.newPassword
        };
    }

}

export class User {

    constructor(
        readonly id: number,
        readonly userName: string,
        readonly enabled: boolean,
        readonly role: Role,
        readonly emailAddress: string,
        readonly name: string,
        readonly surname: string,
        readonly colorHex: string,
        readonly accessToken: string
        
    ) {
    }

    static build(obj: User) {
        return new User(obj.id, obj.userName, obj.enabled, obj.role, obj.emailAddress,obj.name,obj.surname,obj.colorHex,
            obj.accessToken);
    }

}

export enum Role {

    Administrator,
    Operator

}

export class UpdateUserRequest {

    constructor(
        public userName: string,
        public enabled: boolean,
        public role: Role,
        public emailAddress: string,
        public password: string,
        readonly name: string,
        readonly surname: string,
        readonly colorHex: string,
    ) {
    }

    static empty() {
        return new UpdateUserRequest(null, true, Role.Operator, null, null,null,null,null);
    }

    static fromUser(user: User) {
        return new UpdateUserRequest(user.userName, user.enabled, user.role, user.emailAddress, null, user.name,user.surname,user.colorHex);
    }

}

export interface IUser {

    readonly id: number;
    readonly userName: string;
    readonly enabled: boolean;
    readonly role: Role;

}
