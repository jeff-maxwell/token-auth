import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-forgot',
  templateUrl: './forgot.component.html',
  styleUrls: ['./forgot.component.css']
})
export class ForgotComponent implements OnInit {

email: string;
message: string;

  constructor() { }

  ngOnInit() {
    this.message = '';
  }

  submit(): void {
    this.message = 'An email with further instructions have been sent.';
  }

}
