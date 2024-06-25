import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
})
export class ProductDetailComponent implements OnInit {
  product: Product | undefined;

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private router: Router
  ) {}

  ngOnInit() {
    const productId = Number(this.route.snapshot.paramMap.get('id'));
    if (productId) {
      this.productService.getProductById(productId).subscribe(
        (data) => (this.product = data),
        (error) => this.handleError(error)
      );
    }
  }

  private handleError(error: any) {
    console.error('An error occurred:', error);
    window.alert(error.message || 'An error occurred');
  }

  goBack() {
    this.router.navigate(['/']);
  }
}
