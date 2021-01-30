import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/user.service';
import { User } from "../shared/user.model";
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styles: [
  ]
})
export class UserComponent implements OnInit {

  constructor(public service: UserService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.service.refreshUsers();
  }

  populateForm(selectedRecord: User) {
    this.service.formData = Object.assign({}, selectedRecord);
  }

  onDelete(id: number) {
    if (confirm('Вы уверены, что хотите удалить запись?'))
    {
      this.service.deleteUser(id).subscribe(
        () => {
          this.service.refreshUsers();
          this.toastr.success('Пользователь удалён');
          this.service.refreshUsers();
        },
        error => {
          console.log(error);
        }
      );
    }
  }

}
