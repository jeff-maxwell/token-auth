import { Component, Input, OnInit } from '@angular/core';
import { md5 } from '../../shared/md5';
import { UserService } from '../../services/user.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  title = 'Login';
  user: any = { 'email': '', 'password': '' };
  gravatar: string;
  returnUrl: string;

  constructor(
    private userService: UserService,
    private route: ActivatedRoute,
    private router: Router) { }

  ngOnInit(): void {
    // this.getGravatarImage();
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

  }

  getGravatarImage(): void {
    this.gravatar = 'http://www.gravatar.com/avatar/' + md5(this.user.email) + '?s=96';
  }

  login(): void {
    this.userService.login(this.user)
      .subscribe(
          data => {
              this.router.navigate([this.returnUrl]);
          },
          error => {
            console.log('Username or password is incorrect');
          });
  }
}
