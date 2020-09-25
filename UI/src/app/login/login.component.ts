import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { LoginService } from '../services/login.service';
import { StatusType, UserType } from '../models/models';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  name;
  password;

  constructor(private toastr: ToastrService,
    private loginService: LoginService,
    private router: Router) { }

  ngOnInit() {
    window.localStorage.clear();
  }

  login() {
    if (this.name == null || this.name == '') {
      this.toastr.warning('Warning', 'Name is required');
    } else if (this.password == null || this.password == '') {
      this.toastr.warning('Warning', 'Password is required');
    } else {
      let body = {
        name: this.name,
        password: this.password,
      };
      this.loginService.Login(body).subscribe(
        item => {
          if (item && item.status == StatusType.Success) {
            this.loginService.setStorage(item);
            this.toastr.success('Success', 'Logged in successfully');
            if (item.data.type == UserType.Admin) {
              this.router.navigate(['/admin']);
            } else {
              this.router.navigate(['/customer']);
            }
          } else {
            if (item && item.errors && item.errors.length > 0) {
              for (let i = 0 ; i < item.errors.length ; i++) {
                this.toastr.error('Error', item.errors[i]);
              }
            }
          }
        }, err => {
          this.toastr.error('Error', 'Something went wrong, contact Admin');
        }
      )
    }
  }



}
