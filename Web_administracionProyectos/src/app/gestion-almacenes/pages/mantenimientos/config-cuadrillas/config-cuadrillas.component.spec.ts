import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfigCuadrillasComponent } from './config-cuadrillas.component';

describe('ConfigCuadrillasComponent', () => {
  let component: ConfigCuadrillasComponent;
  let fixture: ComponentFixture<ConfigCuadrillasComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConfigCuadrillasComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfigCuadrillasComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
