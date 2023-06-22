import { EventEmitter, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { v4 as uuidv4 } from 'uuid';

@Injectable()
export class AuthService {
  private baseUrl = 'https://localhost:44346';
    isLoggedIn: boolean = false;
    loginStatusChanged: EventEmitter<boolean> = new EventEmitter<boolean>()

  constructor(private http: HttpClient) {  
    this.isLoggedIn = localStorage.getItem('isLoggedIn') === 'true';
  }
  

  getIsLoggedIn(): boolean {
    return this.isLoggedIn;
  }

  setIsLoggedIn(value: boolean): void {
    this.isLoggedIn = value;
    localStorage.setItem('isLoggedIn', value.toString());
  }

  loginUser(email: string, password: string): Observable<any> {
    const payload = {
      id: uuidv4(),
      name: "",
      email: email,
      password: password
    };
    return this.http.post<any>(`${this.baseUrl}/api/Users/login`, payload);
  }

  login(): void {
    this.isLoggedIn = true    
    this.loginStatusChanged.emit(this.isLoggedIn);
  }

  logout(): void {
    this.isLoggedIn = false;
    this.loginStatusChanged.emit(this.isLoggedIn);
  }
}

