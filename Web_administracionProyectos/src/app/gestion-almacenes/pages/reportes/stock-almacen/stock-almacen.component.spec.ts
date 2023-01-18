import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockAlmacenComponent } from './stock-almacen.component';

describe('StockAlmacenComponent', () => {
  let component: StockAlmacenComponent;
  let fixture: ComponentFixture<StockAlmacenComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StockAlmacenComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StockAlmacenComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
