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

  putUser() {
    return this.http.put(`${this.baseUrl}/${this.formData.id}`, this.formData);
  }

  deleteUser(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }

  refreshUsers() {
    this.http.get(this.baseUrl)
    .toPromise()
    .then(response => this.users = response as User[]);
  }
}
