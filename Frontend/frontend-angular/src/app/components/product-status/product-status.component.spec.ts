import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductStatusComponent } from './product-status.component';

describe('ProductStatusComponent', () => {
  let component: ProductStatusComponent;
  let fixture: ComponentFixture<ProductStatusComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProductStatusComponent]
    });
    fixture = TestBed.createComponent(ProductStatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
