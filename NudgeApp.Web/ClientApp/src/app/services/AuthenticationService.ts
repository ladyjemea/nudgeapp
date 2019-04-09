import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { GoogleAuthService } from 'ng-gapi';
import { Request } from '@angular/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthenticationService {

  public static loggedIn: boolean = false;

  constructor(private http: HttpClient, private googleAuth: GoogleAuthService) { }

  login(username: string, password: string) {
    var params = new HttpParams();
    params = params.append('username', username);
    params = params.append('password', password);

    return this.http.get('User/authenticate', { params: params }).pipe(map(user => {
      this.saveToken(user);
      return user;
    }));
  }

  logout() {
    AuthenticationService.loggedIn = false;
    localStorage.removeItem('currentUser');
  }

  GoogleLogin(callback: any = () => { }) {
    this.googleAuth.getAuth()
      .subscribe((auth) => {
        auth.signIn().then(res => {
          this.ServerGoogleAuth(res.w3.Eea, res.Zi.id_token, res.w3.U3).subscribe(() => {
            if (callback !== null || callback !== undefined)
              callback();
          }
          );
        });
      });
  }

  private ServerGoogleAuth(id: string, tokenId: string, email: string) {
    var userData = {
      id, tokenId, email
    };
    return this.http.post('User/GoogleSignIn', userData)
      .pipe(map(user => {
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
