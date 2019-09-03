import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { EmployeeService } from '../employee.service';
import { ActivatedRoute } from '@angular/router';
import { IEmployee } from '../IEmployee';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})
export class EmployeeComponent implements OnInit {

  empForm: FormGroup;
  pageTitle: string;
  action: string;
  empid: number;
  employee: IEmployee = {
    Id: null,
    Name: '',
    Gender: '',
    Email: ''
  };
  constructor(private fb: FormBuilder,
              private empservice: EmployeeService,
              private route: ActivatedRoute) {

  }

  ngOnInit() {
    this.empForm = this.fb.group({
      Name: ['', Validators.required],
      Gender: ['male'],
      Email: ['', Validators.required],
      Department: ['', Validators.required]
    });

    this.route.paramMap.subscribe(
      params => {
        this.empid = +params.get('id');
        if (this.empid) {
          this.action = 'Update';
          this.pageTitle = 'Edit Employee';
          this.getEmployee(this.empid);
        } else {
          this.action = 'Save';
          this.pageTitle = 'Create Employee';
        }
      }
    );
  }

  getEmployee(id: number) {
    this.empservice.getEmployee(id).subscribe(
      (employee: IEmployee) => {
        this.empForm.patchValue({
          Name: employee.Name,
          Gender: employee.Gender,
          Email: employee.Email
        });
      }
    );
  }

  onSubmit() {
    if (this.empid) {
      this.mapFormValuesToEmployeeModel();
      this.employee.Id = this.empid;
      this.empservice.updateEmployee(this.employee).subscribe(
        data => {
          this.empForm.reset();
        },
        err => {
          console.log(err.error);
        }
      );
    } else {
      this.mapFormValuesToEmployeeModel();
      this.employee.Id = 0;
      this.empservice.addEmployee(this.employee).subscribe(
        data => {
          this.empForm.reset();
        },
        err => {
          console.log(err.error);
        }
      );
    }
  }

  mapFormValuesToEmployeeModel() {
    this.employee.Name = this.empForm.controls.Name.value;
    this.employee.Gender = this.empForm.controls.Gender.value;
    this.employee.Email = this.empForm.controls.Email.value;
  }
}
