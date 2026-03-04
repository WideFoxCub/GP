import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ServiceService, ServiceDto } from '../../services/services.service';

@Component({
  selector: 'app-services-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './services-list.component.html',
  styleUrls: ['./services-list.component.css']
})
export class ServicesListComponent implements OnInit {
  services: ServiceDto[] = [];
  loading = true;
  error: string | null = null;

  constructor(
    private serviceService: ServiceService,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    this.loadServices();
  }

  loadServices(): void {
    this.loading = true;
    this.error = null;

    console.log('🚀 Ładuję usługi z: http://localhost:5202/api/services');

    this.serviceService.getAllServices().subscribe({
      next: (data: ServiceDto[]) => {
        console.log('✅ Dane otrzymane:', data);
        this.services = data;
        this.loading = false;
        this.cdr.detectChanges(); // ← WYMUŚ CHANGE DETECTION!
      },
      error: (err: any) => {
        console.error('❌ Błąd:', err);
        this.error = 'Błąd przy ładowaniu usług: ' + err.message;
        this.loading = false;
        this.cdr.detectChanges(); // ← WYMUŚ CHANGE DETECTION!
      },
      complete: () => {
        console.log('✅ Request completed');
      }
    });
  }

  deleteService(id: number): void {
    if (confirm('Na pewno chcesz usunąć tę usługę?')) {
      this.serviceService.deleteService(id).subscribe({
        next: () => {
          this.loadServices();
        },
        error: (err: any) => {
          alert('Błąd przy usuwaniu: ' + err.message);
        }
      });
    }
  }
}
