import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockObraMaterialComponent } from './stock-obra-material.component';

describe('StockObraMaterialComponent', () => {
  let component: StockObraMaterialComponent;
  let fixture: ComponentFixture<StockObraMaterialComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StockObraMaterialComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StockObraMaterialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
