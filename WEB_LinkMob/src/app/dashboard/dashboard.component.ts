import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  isLoggedIn :boolean;

constructor(private authService: AuthService){
    this.isLoggedIn = authService.getIsLoggedIn();

    this.authService.loginStatusChanged.subscribe((status: boolean) => {
     this.isLoggedIn = status;
     this.authService.setIsLoggedIn(status);
   });  
  }

  ngOnInit() {
    this.authService.setIsLoggedIn(false);    
  }
}
