import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportacionMaterialComponent } from './importacion-material.component';

describe('ImportacionMaterialComponent', () => {
  let component: ImportacionMaterialComponent;
  let fixture: ComponentFixture<ImportacionMaterialComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportacionMaterialComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportacionMaterialComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
