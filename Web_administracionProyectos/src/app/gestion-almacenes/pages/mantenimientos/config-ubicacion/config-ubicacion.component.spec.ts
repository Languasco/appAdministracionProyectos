import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigUbicacionComponent } from './config-ubicacion.component';

describe('ConfigUbicacionComponent', () => {
  let component: ConfigUbicacionComponent;
  let fixture: ComponentFixture<ConfigUbicacionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfigUbicacionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigUbicacionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
