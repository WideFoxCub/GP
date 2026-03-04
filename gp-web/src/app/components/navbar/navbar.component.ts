import { Component, HostListener } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, CommonModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {
  isMenuOpen = false;
  isOfferOpen = false;

  toggleMenu(): void {
    this.isMenuOpen = !this.isMenuOpen;

    // jak zamykasz hamburgera to zamknij też submenu
    if (!this.isMenuOpen) {
      this.isOfferOpen = false;
    }
  }

  closeMenu(): void {
    this.isMenuOpen = false;
    this.isOfferOpen = false;
  }

  toggleOfferDropdown(event: Event): void {
    // nie klikaj “po drodze” w inne elementy (np. li)
    event.stopPropagation();
    this.isOfferOpen = !this.isOfferOpen;
  }

  closeMenuAndDropdown(): void {
    this.isMenuOpen = false;
    this.isOfferOpen = false;
  }

  // Klik poza menu = zamknij rozwijane (opcjonalnie)
  @HostListener('document:click')
  onDocumentClick(): void {
    this.isOfferOpen = false;
  }
}
