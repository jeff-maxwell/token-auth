import { Component, OnInit } from '@angular/core';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css']
})
export class SignupComponent implements OnInit {

user: any = {};
message: string;

  constructor(private userService: UserService, private router: Router) {
  }

  ngOnInit() {
  }

  signup(): void {
    if (this.user.password !== this.user.confirmPassword) {
      this.message = 'Password and Confirm Password must match';
    } else {
      this.message = '';
      this.userService.signup(this.user)
        .subscribe(
          data => {
              this.router.navigate(['/home']);
          },
          error => {
            console.log('Username or password is incorrect');
          });
    }
  }

}
