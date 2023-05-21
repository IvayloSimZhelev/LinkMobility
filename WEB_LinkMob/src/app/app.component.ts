import { Component } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { AuthService } from './services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  items: MenuItem[];
  isLoggedIn: boolean = false;   
  title = 'Link Mod';

  constructor(private authService: AuthService,  private router: Router) {
    this.isLoggedIn = authService.isLoggedIn;

     this.authService.loginStatusChanged.subscribe((status: boolean) => {
      this.isLoggedIn = status;
    });
  }

  toggleLoginStatus() {
    const currentUrl = this.router.url;
    if (currentUrl==='/datagrid' || currentUrl==='/login'){
      this.authService.setIsLoggedIn(false);  
    }
    this.isLoggedIn = this.authService.getIsLoggedIn();
  }
}
