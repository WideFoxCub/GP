import { Component, HostListener, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, NavigationEnd, RouterLink, RouterLinkActive } from '@angular/router';
import { filter, Subscription } from 'rxjs';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnDestroy {
  isMenuOpen = false;
  isOfferOpen = false;

  private sub: Subscription;

  constructor(private router: Router) {
    this.sub = this.router.events
      .pipe(filter(e => e instanceof NavigationEnd))
      .subscribe(() => {
        this.isOfferOpen = false;
        this.isMenuOpen = false;
      });
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  toggleMenu(): void {
    this.isMenuOpen = !this.isMenuOpen;
    if (!this.isMenuOpen) this.isOfferOpen = false;
  }

  closeMenu(e?: Event): void {
    e?.stopPropagation();
    this.isMenuOpen = false;
    this.isOfferOpen = false;
  }

  toggleOfferDropdown(e: MouseEvent): void {
    e.stopPropagation();
    this.isOfferOpen = !this.isOfferOpen;
  }

  closeMenus(e?: Event): void {
    e?.stopPropagation();
    this.isOfferOpen = false;
    this.isMenuOpen = false;
  }

  @HostListener('document:click')
  onDocumentClick(): void {
    this.isOfferOpen = false;
  }
}
