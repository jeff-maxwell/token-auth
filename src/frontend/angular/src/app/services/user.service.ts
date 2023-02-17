import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { AuthUser } from '../models/authUser';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class UserService {
    private subject = new Subject<any>();
    env: any = environment;

    constructor(private http: HttpClient) { }

    currentUser() {
        const text = localStorage.getItem('currentUser');
        if (text === null || typeof text === 'undefined' || text === 'undefined') {
            return null;
        } else {
            return JSON.parse(text);
        }
    }

    login(userInfo: any) {
        return this.http.post<AuthUser>(this.env.baseAPIUrl + 'users/login',
            userInfo,
            { headers: new HttpHeaders().set('Content-Type', 'application/json')})
            .map(user => {
                if (user && user.token) {
                    this.subject.next(user);
                    localStorage.setItem('currentUser', JSON.stringify(user));
                }
            });
    }

    logout() {
        localStorage.removeItem('currentUser');
    }

    signup(signupInfo: any) {
        return this.http.post<any>(this.env.baseAPIUrl + 'users/signup', signupInfo);
    }

    getUser(): Observable<any> {
        return this.subject.asObservable();
    }

    public getToken(): string {
        return localStorage.getItem('token');
    }

    public isAuthenticated(): boolean {
        const token = this.getToken();
        return true;
    }
}
