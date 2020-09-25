export class ResponseResult<T> {
    data: T;
    status: StatusType;
    errors: string[];
}

export class LoginResonse {
    id: number;
    name: string;
    type: UserType;
    token: string;
}

export class Complaint {
    id: number;
    description: string;
    status: ComplaintStatus;
    type: ComplaintType;
    userId: number;
    statusStr: number;
    typeStr: number;
    username: string;
}

export enum StatusType {
    Failed = 0,
    Success = 1
}

export enum UserType {
    Customer = 1,
    Admin = 2
}

export enum ComplaintStatus {
    Pending = 1,
    Resolved = 2,
    Dismissed = 3
}

export enum ComplaintType {
    Complaint = 1,
    GeneralQuery = 2
}
