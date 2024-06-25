import { Component, EventEmitter, Output, Input } from '@angular/core';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css'],
})
export class ProductFormComponent {
  @Input() product: Product = {
    productId: 0,
    name: '',
    status: 0,
    stock: 0,
    description: '',
    price: 0,
    statusName: '',
  };
  @Output() save = new EventEmitter<Product>();
  @Output() close = new EventEmitter<boolean>();

  onSave() {
    this.save.emit(this.product);
  }

  onClose() {
    this.close.emit(true);
  }
}
