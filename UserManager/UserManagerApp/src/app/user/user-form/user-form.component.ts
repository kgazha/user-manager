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
    this.service.postUser().subscribe(
      response => {
        this.resetForm(form);
        this.toastr.success('Пользователь добавлен');
      },
      error => {
        console.log(error);
      }
      
    );
  }

  resetForm(form: NgForm) {
    form.form.reset();
    this.service.formData = new User();
  }

}
