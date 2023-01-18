import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StockEmpresaComponent } from './stock-empresa.component';

describe('StockEmpresaComponent', () => {
  let component: StockEmpresaComponent;
  let fixture: ComponentFixture<StockEmpresaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StockEmpresaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StockEmpresaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
