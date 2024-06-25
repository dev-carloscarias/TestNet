import { Component, OnInit } from '@angular/core';
import { Product } from '../../models/product.model';
import { ProductService } from '../../services/product.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  selectedProduct?: Product;
  showForm = false;

  constructor(private productService: ProductService, private router: Router) {}

  ngOnInit() {
    this.loadProducts();
  }

  loadProducts() {
    this.productService.getAllProducts().subscribe(
      (data) => (this.products = data),
      (error) => this.handleError(error)
    );
  }

  addProduct() {
    this.selectedProduct = {
      productId: 0,
      name: '',
      status: 0,
      stock: 0,
      description: '',
      price: 0,
      statusName: '',
    };
    this.showForm = true;
  }
  viewStatus() {
    this.router.navigate(['/statuses']);
  }
  editProduct(product: Product) {
    this.selectedProduct = product;
    this.showForm = true;
  }

  saveProduct(product: Product) {
    if (product.productId === 0) {
      this.productService.addProduct(product).subscribe(
        () => {
          this.loadProducts();
          window.alert('Producto agregado correctamente!');
        },
        (error) => this.handleError(error)
      );
    } else {
      this.productService.updateProduct(product).subscribe(
        () => {
          this.loadProducts();
          window.alert('Producto actualizado correctamente!');
        },
        (error) => this.handleError(error)
      );
    }
    this.showForm = false;
  }

  closeForm() {
    this.showForm = false;
  }

  viewProduct(productId: number) {
    this.router.navigate(['/product', productId]);
  }

  private handleError(error: any) {
    console.error('An error occurred:', error);
    window.alert(error.message || 'An error occurred');
  }
}
