import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { HomeComponent } from './home/home.component';
import { ForgotComponent } from './auth/forgot/forgot.component';
import { LogoutComponent } from './auth/logout/logout.component';
import { QuestionsComponent } from './auth/questions/questions.component';
import { SignupComponent } from './auth/signup/signup.component';
import { SecureComponent } from './secure/secure.component';
import { AuthGuardService } from './services/authguard.service';

const routes: Routes = [
      { path: '', component: HomeComponent },
      { path: 'login', component: LoginComponent },
      { path: 'auth/forgot', component: ForgotComponent },
      { path: 'auth/logout', component: LogoutComponent },
      { path: 'auth/signup', component: SignupComponent },
      { path: 'auth/questions', component: QuestionsComponent },
      { path: 'secure', component: SecureComponent, canActivate: [AuthGuardService] },
      { path: '**', redirectTo: 'login' }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
