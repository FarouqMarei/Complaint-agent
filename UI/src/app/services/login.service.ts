import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ResponseResult, LoginResonse } from '../models/models';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  loggedInUser: LoginResonse = new LoginResonse();
  apiUrl = 'https://localhost:44334/api/login';

  constructor(private http: HttpClient, private router: Router) { }

  Register(body): Observable<ResponseResult<LoginResonse>> {
    const url = this.apiUrl + '/register';
    return this.http.post<ResponseResult<LoginResonse>>(url, body);
  }

  Login(body): Observable<ResponseResult<LoginResonse>> {
    const url = this.apiUrl + '/login';
    return this.http.post<ResponseResult<LoginResonse>>(url, body);
  }

  logout() {
    window.localStorage.clear();
    this.router.navigate(['/login']);
  }

  setStorage(item) {
    window.localStorage.setItem("id", item.data.id.toString());
    window.localStorage.setItem("username", item.data.name);
    window.localStorage.setItem("usertype", item.data.type.toString());
    window.localStorage.setItem("token", item.data.token);
  }

  getToken(): string {
    return window.localStorage.getItem("token");
  }

  getUserType(): string {
    return window.localStorage.getItem("usertype");
  }

}
