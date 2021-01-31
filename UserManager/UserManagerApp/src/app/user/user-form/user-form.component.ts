import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/shared/user.model';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styles: [
  ]
})
export class UserFormComponent implements OnInit {

  constructor(public service: UserService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }

  onSubmit(form: NgForm) {
    if (this.service.formData.id == 0) {
      this.insertRecord(form);
    }
    else {
      this.updateRecord(form);
    }
  }

  insertRecord(form: NgForm) {
    this.service.postUser().subscribe(
      response => {
        this.resetForm(form);
        this.toastr.success('Пользователь добавлен');
        this.service.refreshUsers();
      },
      errorResponse => {
        console.log(errorResponse);
        if ('error' in errorResponse) {
          if ('errors' in errorResponse.error) {
            for (const message of errorResponse.error.errors) {
              this.toastr.error(message);
            }
          }
        }
      }
    );
  }

  updateRecord(form: NgForm) {
    this.service.putUser().subscribe(
      response => {
        this.resetForm(form);
        this.toastr.success('Пользователь успешно обновлён');
        this.service.refreshUsers();
      },
      errorResponse => {
        console.log(errorResponse);
        if ('error' in errorResponse) {
          if ('errors' in errorResponse.error) {
            for (const message of errorResponse.error.errors) {
              this.toastr.error(message);
            }
          }
        }
      }
    );
  }

  resetForm(form: NgForm) {
    form.form.reset();
    this.service.formData = new User();
  }

}
