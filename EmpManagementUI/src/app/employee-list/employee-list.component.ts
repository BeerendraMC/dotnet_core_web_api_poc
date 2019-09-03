import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../employee.service';
import { IEmployee } from '../IEmployee';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employees: IEmployee[];
  constructor(private empService: EmployeeService, private roter: Router) { }

  ngOnInit() {
    this.getEmps();
  }

  getEmps() {
    this.empService.getEmployees().subscribe(
      data => {
        this.employees = data;
      },
      err => {
        console.log(err.error);
      }
    );
  }

  editButtonClick(empid: number) {
    this.roter.navigate(['employee', empid]);
  }

}
