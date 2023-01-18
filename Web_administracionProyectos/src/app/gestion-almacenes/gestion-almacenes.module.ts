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

import { GestionAlmacenesRoutingModule } from './gestion-almacenes-routing.module';
import { PagesComponent } from './pages/pages.component';
import { LocalesComponent } from './pages/mantenimientos/locales/locales.component';
import { DelegacionesComponent } from './pages/mantenimientos/delegaciones/delegaciones.component';
import { GuiasComponent } from './pages/procesos/guias/guias.component';
import { TransferenciasComponent } from './pages/procesos/transferencias/transferencias.component';
import { StockAlmacenComponent } from './pages/reportes/stock-almacen/stock-almacen.component';
import { StockEmpresaComponent } from './pages/reportes/stock-empresa/stock-empresa.component';
import { StockObraMaterialComponent } from './pages/reportes/stock-obra-material/stock-obra-material.component';
import { HomeComponent } from './pages/home/home.component';
import { CuentaCorrienteComponent } from './pages/mantenimientos/cuenta-corriente/cuenta-corriente.component';
import { PersonalComponent } from './pages/mantenimientos/personal/personal.component';
import { ConfigUbicacionComponent } from './pages/mantenimientos/config-ubicacion/config-ubicacion.component';
import { MovAlmacenComponent } from './pages/mantenimientos/mov-almacen/mov-almacen.component';
import { ImportacionMaterialComponent } from './pages/mantenimientos/importacion-material/importacion-material.component';
import { TipoAlmacenComponent } from './pages/mantenimientos/tipo-almacen/tipo-almacen.component';
import { AlmacenComponent } from './pages/mantenimientos/almacen/almacen.component';
import { MaterialesComponent } from './pages/mantenimientos/materiales/materiales.component';
import { FamiliaComponent } from './pages/mantenimientos/familia/familia.component';
import { UnidadMedidaComponent } from './pages/mantenimientos/unidad-medida/unidad-medida.component';
import { AreasComponent } from './pages/mantenimientos/areas/areas.component';
import { ObrasComponent } from './pages/mantenimientos/obras/obras.component';
import { CargoPersonalComponent } from './pages/mantenimientos/cargo-personal/cargo-personal.component';
import { ConfigCuadrillasComponent } from './pages/mantenimientos/config-cuadrillas/config-cuadrillas.component';

@NgModule({
  declarations: [
    PagesComponent,
    LocalesComponent,
    DelegacionesComponent,
    GuiasComponent,
    TransferenciasComponent,
    StockAlmacenComponent,
    StockEmpresaComponent,
    StockObraMaterialComponent,
    HomeComponent,
    CuentaCorrienteComponent,
    PersonalComponent,
    ConfigUbicacionComponent,
    MovAlmacenComponent,
    ImportacionMaterialComponent,
    TipoAlmacenComponent,
    AlmacenComponent,
    MaterialesComponent,
    FamiliaComponent,
    UnidadMedidaComponent,
    AreasComponent,
    ObrasComponent,
    CargoPersonalComponent,
    ConfigCuadrillasComponent
  ],
  imports: [
    CommonModule,
    GestionAlmacenesRoutingModule,
    ComponentsModule,
    FormsModule,
    ReactiveFormsModule,
    Ng2SearchPipeModule,
    BsDatepickerModule.forRoot(),
    TabsModule.forRoot(),
  ]
})
export class GestionAlmacenesModule { 
  constructor( private bsLocaleService: BsLocaleService){
    this.bsLocaleService.use('es');//fecha en español, datepicker
  }
}
