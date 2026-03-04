import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ServiceService, ServiceDto } from '../../services/services.service';
import { SeoService } from '../../services/seo.service';

@Component({
  selector: 'app-offer',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './offer.component.html',
  styleUrls: ['./offer.component.css']
})
export class OfferComponent implements OnInit {

  services: ServiceDto[] = [];
  loading = true;
  error: string | null = null;

  constructor(
    private serviceService: ServiceService,
    private cdr: ChangeDetectorRef,
    private seo: SeoService
  ) { }

  ngOnInit(): void {
    this.seo.setServicePageSeo();
    this.loadServices();
  }

  loadServices(): void {
    this.loading = true;
    this.error = null;

    this.serviceService.getAllServices().subscribe({
      next: (data: ServiceDto[]) => {
        this.services = data;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.error = 'Błąd przy ładowaniu usług';
        this.loading = false;
        this.cdr.detectChanges();
      }
    });
  }
}
