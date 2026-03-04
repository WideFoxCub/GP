import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ServiceService, ServiceDto, ServiceCategory } from '../../services/services.service';

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

  category: ServiceCategory | null = null;

  constructor(
    private serviceService: ServiceService,
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {
    this.route.queryParamMap.subscribe(qp => {
      const cat = qp.get('cat');
      this.category = (cat === 'nails' || cat === 'cosmetology') ? cat : null;
      this.load();
    });
  }

  private load(): void {
    this.loading = true;
    this.error = null;
    this.cdr.detectChanges();

    this.serviceService.getServices(this.category ?? undefined).subscribe({
      next: (data) => {
        this.services = data;
        this.loading = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.error = 'Błąd przy pobieraniu usług.';
        this.loading = false;
        this.cdr.detectChanges();
      }
    });
  }
}
