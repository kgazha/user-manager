import { Injectable } from '@angular/core';
import { User } from './user.model';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient) { }

  formData: User = new User();
  users: User[];

  readonly baseUrl = 'http://localhost:51067/api/Users'

  postUser() {
    return this.http.post(this.baseUrl, this.formData);
  }

  refreshUsers() {
    this.http.get(this.baseUrl)
    .toPromise()
    .then(response => this.users = response as User[]);
  }
}
