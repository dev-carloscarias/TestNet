import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProductStatusService {
  private apiUrl = 'http://localhost:5056/api/products/statuses';

  constructor(private http: HttpClient) {}

  getProductStatuses(): Observable<any> {
    return this.http.get<any>(this.apiUrl);
  }
}
