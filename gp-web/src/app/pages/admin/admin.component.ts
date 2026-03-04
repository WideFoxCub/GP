import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-admin',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent {
  isLoggedIn = false;
  password = '';
  adminPassword = 'admin123'; // TODO: Zmień hasło!

  login(): void {
    if (this.password === this.adminPassword) {
      this.isLoggedIn = true;
      this.password = '';
    } else {
      alert('Błędne hasło!');
      this.password = '';
    }
  }

  logout(): void {
    this.isLoggedIn = false;
  }
}
