import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Ng2SearchPipeModule } from 'ng2-search-filter';
import { ComponentsModule } from '../components/components.module';

//--- tabs
import { TabsModule } from 'ngx-bootstrap/tabs'; 

//----- fechas datetimePicker ---
import { BsDatepickerModule, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { esLocale } from 'ngx-bootstrap/locale';
defineLocale('es', esLocale);

import { GestionProyectosRoutingModule } from './gestion-proyectos-routing.module';
import { PagesComponent } from './pages/pages.component';
import { HomeComponent } from './pages/home/home.component';


@NgModule({
  declarations: [
    PagesComponent,
    HomeComponent
  ],
  imports: [
    CommonModule,
    GestionProyectosRoutingModule,
    ComponentsModule,
    FormsModule,
    ReactiveFormsModule,
    Ng2SearchPipeModule,
    BsDatepickerModule.forRoot(),
    TabsModule.forRoot(),
  ]
})
export class GestionProyectosModule {
  constructor( private bsLocaleService: BsLocaleService){
    this.bsLocaleService.use('es');//fecha en español, datepicker
  }
 }
