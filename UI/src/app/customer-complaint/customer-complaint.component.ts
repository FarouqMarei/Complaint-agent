import { ComplaintStatus } from './../models/models';
import { ComplaintService } from './../services/complaint.service';
import { LoginService } from './../services/login.service';
import { Component, OnInit } from '@angular/core';
import { StatusType, Complaint } from '../models/models';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-customer-complaint',
  templateUrl: './customer-complaint.component.html',
  styleUrls: ['./customer-complaint.component.css']
})
export class CustomerComplaintComponent implements OnInit {

  userId: any;
  username: string;
  usertype: string;

  showForm = false;
  loadTable = false;
  disableSubmit = false;

  complaintList: any[];
  complaint: Complaint = new Complaint();

  constructor(public loginService: LoginService,
    private complaintService: ComplaintService,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.userId = window.localStorage.getItem("id");
    this.username = window.localStorage.getItem("username");
    this.usertype = window.localStorage.getItem("usertype");
    if (this.usertype == "1") {
      this.usertype = 'Customer';
    } else {
      this.usertype = 'Admin';
    }

    this.FillLists();
  }

  FillLists() {
    this.complaintService.GetComplaintsByUserId(parseInt(this.userId)).subscribe(
      item => {
        if (item && item.status == StatusType.Success) {
          this.complaintList = item.data;
          this.loadTable = true;
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

  trClick(item) {
    this.disableSubmit = false;
    this.complaint.id = item.id;
    this.complaint.description = item.description;
    this.complaint.type = item.type;
    this.complaint.userId = this.userId;
    if (item.status != ComplaintStatus.Pending) {
      this.disableSubmit = true;
    }
    this.showForm = true;
  }

  NewForm() {
    this.complaint = new Complaint();
    this.showForm = true;
  }

  closeForm() {
    this.complaint = new Complaint();
    this.showForm = false;
  }

  submitForm() {
    if (this.complaint && (this.complaint.type == 0)) {
      this.toastr.warning('Warning', 'Select type');
    } else {
      let body = {
        id: this.complaint.id,
        description: this.complaint.description,
        type: parseInt(this.complaint.type.toString()),
        userId: this.userId
      };
      this.complaintService.SubmitComplaint(body).subscribe(
        item => {
          if (item && item.status == StatusType.Success) {
            this.toastr.success('Success', this.complaint.id > 0 ? 'Complaint updated' : 'Complaint submitted');
            this.FillLists();
            this.showForm = false;
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
