import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { LoginComponent } from './auth/login/login.component';
import { HomeComponent } from './home/home.component';
import { TopbarComponent } from './topbar/topbar.component';
import { FormsModule } from '@angular/forms';
import { ForgotComponent } from './auth/forgot/forgot.component';
import { LogoutComponent } from './auth/logout/logout.component';
import { QuestionsComponent } from './auth/questions/questions.component';
import { SignupComponent } from './auth/signup/signup.component';
import { UserService } from './services/user.service';
import { HttpClientModule } from '@angular/common/http';
import { AuthGuardService } from './services/authguard.service';
import { SecureComponent } from './secure/secure.component';
import { NewPasswordComponent } from './auth/newpassword/newpassword.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    TopbarComponent,
    ForgotComponent,
    LogoutComponent,
    QuestionsComponent,
    SignupComponent,
    SecureComponent,
    NewPasswordComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    AuthGuardService,
    UserService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
