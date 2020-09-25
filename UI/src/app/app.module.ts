import { AuthService } from './services/auth.service';
import { ComplaintService } from './services/complaint.service';
import { HttpClientModule} from '@angular/common/http';
import { LoginService } from './services/login.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AdminComplaintComponent } from './admin-complaint/admin-complaint.component';
import { CustomerComplaintComponent } from './customer-complaint/customer-complaint.component';
import { FormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule, ToastrService, Overlay, OverlayContainer } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    AdminComplaintComponent,
    CustomerComplaintComponent
   ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    HttpClientModule
  ],
  exports: [
  ],
  providers: [
    AuthService,
    LoginService,
    ComplaintService,
    ToastrService,
    Overlay,
    OverlayContainer
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
