import { LoginService } from './../services/login.service';
import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { StatusType } from '../models/models';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  name;
  password;
  userType = '1';

  constructor(private toastr: ToastrService,
    private loginService: LoginService,
    private router: Router) { }

  ngOnInit() {
  }

  register(event) {
    if (this.name == null || this.name == '') {
      this.toastr.warning('Warning', 'Name is required');
    } else if (this.password == null || this.password == '') {
      this.toastr.warning('Warning', 'Password is required');
    } else {
      let body = {
        name: this.name,
        password: this.password,
        type: parseInt(this.userType)
      };
      this.loginService.Register(body).subscribe(
        item => {
          if (item && item.status == StatusType.Success) {
            this.toastr.success('Success', 'Navigating to login page');
            this.router.navigate(['/login']);
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
