import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ResponseResult, LoginResonse, Complaint } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class ComplaintService {

  loggedInUser: LoginResonse = new LoginResonse();
  apiUrl = 'https://localhost:44334/api/Complaint';

  constructor(private http: HttpClient) { }

  GetComplaintsByUserId(userId): Observable<ResponseResult<Complaint[]>> {
    const url = this.apiUrl + '/GetComplaintsByUserId/' + userId;
    return this.http.get<ResponseResult<Complaint[]>>(url);
  }

  SubmitComplaint(body): Observable<ResponseResult<Complaint[]>> {
    const url = this.apiUrl + '/SubmitComplaint';
    return this.http.post<ResponseResult<Complaint[]>>(url, body);
  }

  GetAll(): Observable<ResponseResult<Complaint[]>> {
    const url = this.apiUrl + '/GetAll';
    return this.http.get<ResponseResult<Complaint[]>>(url);
  }

  UpdateStatus(id, status): Observable<ResponseResult<Complaint[]>> {
    const url = this.apiUrl + '/UpdateStatus/' + id + '/' + status;
    return this.http.get<ResponseResult<Complaint[]>>(url);
  }

}
