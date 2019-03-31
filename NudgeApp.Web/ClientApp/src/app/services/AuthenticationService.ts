import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class AuthenticationService {

  public static loggedIn: boolean = false;

  constructor(private http: HttpClient, private router: Router) { }

  login(username: string, password: string): void {
    var params = new HttpParams();
    params = params.append('username', username);
    params = params.append('password', password);

    this.http.get('http://localhost:5000/User/authenticate', { params: params }).pipe(map(user => {
      this.saveToken(user);
    }));

  }

  logout() {
    AuthenticationService.loggedIn = false;
    localStorage.removeItem('currentUser');
  }

  GoogleLogin(id: string, tokenId: string, email: string) {
    var userData = {
      id, tokenId, email
    };
    return this.http.post('User/GoogleSignIn', userData)
      .pipe(map(user => {
        console.log('got something');
        this.saveToken(user);
        return user;
      }));
  }

  checkToken() {
    if (localStorage.getItem('currentUser') !== null)
      return this.http.get('User/CheckToken').subscribe(result => { AuthenticationService.loggedIn = true; }, error => { this.logout(); });
  }

  private saveToken(user) {
    if (user && user['token']) {
      localStorage.setItem('currentUser', JSON.stringify(user))
      AuthenticationService.loggedIn = true;
    }
  }
}
