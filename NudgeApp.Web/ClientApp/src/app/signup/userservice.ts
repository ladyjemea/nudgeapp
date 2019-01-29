import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs';

@Injectable()
export class userservice {

  constructor(private http: HttpClient) { }

  callGet(username: string, password: string): void {
    console.log(username);

    this.http.get('http://localhost:5000/Api/User/createUser?username=' + username + '&password=' + password, { responseType: 'text' }).subscribe(result => {
      console.log(result);
    }, error => console.error(error));
  }
}
