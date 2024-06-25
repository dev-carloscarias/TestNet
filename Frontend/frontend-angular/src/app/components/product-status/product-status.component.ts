import { Component, OnInit } from '@angular/core';
import { ProductStatusService } from '../../services/product-status.service';

@Component({
  selector: 'app-product-status',
  templateUrl: './product-status.component.html',
  styleUrls: ['./product-status.component.css'],
})
export class ProductStatusComponent implements OnInit {
  statuses: { key: number; value: string }[] = [];

  constructor(private productStatusService: ProductStatusService) {}

  ngOnInit() {
    this.productStatusService.getProductStatuses().subscribe((data) => {
      this.statuses = Object.entries(data).map(([key, value]) => ({
        key: Number(key),
        value: value as string,
      }));
    });
  }

  goBack() {
    window.history.back();
  }
}
