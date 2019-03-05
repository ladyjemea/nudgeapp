import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class AuthenticationService {

  public static loggedIn: boolean;

  constructor(private http: HttpClient, private router: Router) { }

  checkpassword(username: string, password: string): void {

    var params = new HttpParams();
    params = params.append('username', username);
    params = params.append('password', password);

    this.http.get('http://localhost:5000/User/authenticate', { params: params }).pipe(map(user => {
      if (user && user['token']) {
        localStorage.setItem('currentUser', JSON.stringify(user))
        AuthenticationService.loggedIn = true;
        this.router.navigateByUrl('mainaccess');
      }
    })).subscribe();

  }

  logout() {
    AuthenticationService.loggedIn = false;
    localStorage.removeItem('currentUser');
  }

  checkToken() {
    if (localStorage.getItem('currentUser') !== null)
      return this.http.get('User/CheckToken').subscribe(result => { AuthenticationService.loggedIn = true; }, error => { this.logout(); });
  }
}
