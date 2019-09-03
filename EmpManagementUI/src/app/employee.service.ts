import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IEmployee } from './IEmployee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private httpClient: HttpClient) { }

  baseUrl = 'http://localhost:51007/employee/';

  getEmployees(): Observable<IEmployee[]> {
    return this.httpClient.get<IEmployee[]>(this.baseUrl + 'getAllemp');
  }

  getEmployee(id: number): Observable<IEmployee> {
    return this.httpClient.get<IEmployee>(this.baseUrl + 'getEmp/' + id);
  }

  addEmployee(employee: IEmployee): Observable<void> {
    return this.httpClient.post<void>(this.baseUrl + 'addEmp', employee);
  }

  updateEmployee(employee: IEmployee): Observable<void> {
    return this.httpClient.put<void>(this.baseUrl + 'updateEmp', employee);
  }

  deleteEmployee(id: number): Observable<void> {
    return this.httpClient.delete<void>(this.baseUrl + 'deleteEmp/' + id);
  }
}
