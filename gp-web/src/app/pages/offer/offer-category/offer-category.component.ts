import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { ServiceService, ServiceDto, ServiceCategory } from '../../../services/services.service';
import { catchError, distinctUntilChanged, map, of, switchMap, tap } from 'rxjs';

@Component({
  selector: 'app-offer-category',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './offer-category.component.html',
  styleUrls: ['./offer-category.component.css']
})
export class OfferCategoryComponent implements OnInit {
  services: ServiceDto[] = [];
  loading = true;

  title = 'Oferta';
  error: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private serviceService: ServiceService
  ) { }

  ngOnInit(): void {
    this.route.queryParamMap.pipe(
      map(qp => qp.get('cat')),
      map((cat): ServiceCategory | undefined =>
        cat === 'nails' || cat === 'cosmetology' ? cat : undefined
      ),
      distinctUntilChanged(), // nie rób requestu jeśli kategoria ta sama
      tap(category => {
        this.title =
          category === 'nails' ? 'Stylizacja paznokci' :
            category === 'cosmetology' ? 'Kosmetologia' :
              'Oferta';

        this.loading = true;
        this.error = null;
      }),
      switchMap(category =>
        this.serviceService.getServices(category).pipe(
          catchError(() => {
            this.error = 'Błąd przy pobieraniu usług.';
            return of([] as ServiceDto[]);
          })
        )
      )
    ).subscribe(data => {
      this.services = data;
      this.loading = false;
    });
  }
}
