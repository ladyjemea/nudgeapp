import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs';

@Injectable()
export class userservice {

  constructor(private http: HttpClient) { }

  callGet(): void {
    console.log('example http get request sent');

    this.http.get('http://localhost:5000/Api/User/createUser?username=lae&password=lae', { responseType: 'text' }).subscribe(result => {
      console.log(result);
    }, error => console.error(error));
  }
}
