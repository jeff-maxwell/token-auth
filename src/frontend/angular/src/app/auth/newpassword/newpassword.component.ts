import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-newpassword',
  templateUrl: './newpassword.component.html',
  styleUrls: ['./newpassword.component.css']
})
export class NewPasswordComponent implements OnInit {

  message: string;
  password: string;
  confirmPassword: string;

  constructor() { }

  ngOnInit() {
  }

  submit(): void {
    if (this.password !== this.confirmPassword) {
      this.message = 'Password and Confirm Password must match.';
    }
  }

}
