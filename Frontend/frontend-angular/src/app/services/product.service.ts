import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Product } from '../models/product.model';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  private apiUrl = 'http://localhost:5056/api/products';

  constructor(private http: HttpClient) {}

  getProductById(productId: number): Observable<Product> {
    return this.http
      .get<Product>(`${this.apiUrl}/${productId}`)
      .pipe(catchError(this.handleError));
  }

  getAllProducts(): Observable<Product[]> {
    return this.http
      .get<Product[]>(this.apiUrl)
      .pipe(catchError(this.handleError));
  }

  addProduct(product: Product): Observable<Product> {
    return this.http
      .post<Product>(this.apiUrl, product)
      .pipe(catchError(this.handleError));
  }

  updateProduct(product: Product): Observable<void> {
    return this.http
      .put<void>(`${this.apiUrl}/${product.productId}`, product)
      .pipe(catchError(this.handleError));
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Unknown error!';
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    window.alert(errorMessage);
    return throwError(errorMessage);
  }
}
