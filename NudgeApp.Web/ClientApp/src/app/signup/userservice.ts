import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs';

@Injectable()
export class userservice {

  constructor(private http: HttpClient) { }

  createuser(username: string, password: string, name: string, email: string, address: string): void {
    console.log(name);

    this.http.get('http://localhost:5000/Api/User/createUser?username=' + username + '&password=' + password + '&name=' + name + '&email=' + email + '&address=' + address, { responseType: 'text' }).subscribe(result => {
      console.log(result);
    }, error => console.error(error));
  }
}
