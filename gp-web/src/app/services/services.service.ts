import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface ServiceDto {
  id: number;
  name: string;
  description: string;
  priceFrom: number;
  category: 'manicure' | 'kosmetologia';
}

export interface CreateServiceDto {
  name: string;
  description: string;
  priceFrom: number;
  category: 'manicure' | 'kosmetologia';
}

export interface UpdateServiceDto {
  name: string;
  description: string;
  priceFrom: number;
  category: 'manicure' | 'kosmetologia';
  isActive: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class ServiceService {
  private apiUrl = 'http://192.168.1.86:5000/api/services';

  constructor(private http: HttpClient) { }

  // GET - wszystkie usługi
  getAllServices(): Observable<ServiceDto[]> {
    return this.http.get<ServiceDto[]>(this.apiUrl);
  }

  // GET - usługa po ID
  getServiceById(id: number): Observable<ServiceDto> {
    return this.http.get<ServiceDto>(`${this.apiUrl}/${id}`);
  }

  // POST - utwórz nową usługę
  createService(service: CreateServiceDto): Observable<ServiceDto> {
    return this.http.post<ServiceDto>(this.apiUrl, service);
  }

  // PUT - edytuj usługę
  updateService(id: number, service: UpdateServiceDto): Observable<ServiceDto> {
    return this.http.put<ServiceDto>(`${this.apiUrl}/${id}`, service);
  }

  // DELETE - usuń usługę
  deleteService(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
