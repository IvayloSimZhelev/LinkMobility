import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from '../models/cutomer';
import { map } from 'rxjs/operators';
import { CustomerResponse } from '../models/customerResponce';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {
  httpOptions: any;
  private baseUrl = 'https://localhost:44346';

  constructor(private http: HttpClient) {
    this.http = http;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      withCredentials: true,
    };
  }

  // https://localhost:44346/api/Customers?page=1&pageSize=10
  getCustomers(companyName: string, page: number, pageSize: number): Observable<CustomerResponse> {
    const url = `${this.baseUrl}/api/Customers`;
  
    const params = new HttpParams()
      .set('companyName', companyName)
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());
  
    return this.http.get<any>(url, { params }).pipe(
      map(response => ({
        data: response.data,
        total: response.totalRecords
      }))
    );
  }

  // https://localhost:44346/api/Customers/22
  updateCustomer(id: number, customer: Customer): Observable<any> {
    const url = `${this.baseUrl}/api/customers/${id}`;
    return this.http.put<any>(url, customer);
  }

  // https://localhost:44346/api/Customers
  createCustomer(customer: Customer): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/api/customers`, customer);
  }

  // https://localhost:44346/api/Customers/2
  deleteCustomer(id: number) {
    const url = `${this.baseUrl}/api/customers/${id}`;
    return this.http.delete(url);
  }
}
