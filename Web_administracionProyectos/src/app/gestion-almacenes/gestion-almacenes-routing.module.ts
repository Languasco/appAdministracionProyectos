import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../guards/auth.guard';
import { HomeComponent } from './pages/home/home.component';

import { DelegacionesComponent } from './pages/mantenimientos/delegaciones/delegaciones.component';
import { PagesComponent } from './pages/pages.component';
import { LocalesComponent } from './pages/mantenimientos/locales/locales.component';
import { CuentaCorrienteComponent } from './pages/mantenimientos/cuenta-corriente/cuenta-corriente.component';
import { PersonalComponent } from './pages/mantenimientos/personal/personal.component';
import { ConfigUbicacionComponent } from './pages/mantenimientos/config-ubicacion/config-ubicacion.component';
import { MovAlmacenComponent } from './pages/mantenimientos/mov-almacen/mov-almacen.component';
import { ImportacionMaterialComponent } from './pages/mantenimientos/importacion-material/importacion-material.component';
import { TipoAlmacenComponent } from './pages/mantenimientos/tipo-almacen/tipo-almacen.component';
import { AlmacenComponent } from './pages/mantenimientos/almacen/almacen.component';
import { FamiliaComponent } from './pages/mantenimientos/familia/familia.component';
import { UnidadMedidaComponent } from './pages/mantenimientos/unidad-medida/unidad-medida.component';
import { AreasComponent } from './pages/mantenimientos/areas/areas.component';
import { ObrasComponent } from './pages/mantenimientos/obras/obras.component';
import { CargoPersonalComponent } from './pages/mantenimientos/cargo-personal/cargo-personal.component';

const routes: Routes = [
  {
    path : '', component : PagesComponent,
    children : [
      { path: 'home', component: HomeComponent, canActivate: [ AuthGuard] },
      { path: 'Delegacion', component: DelegacionesComponent, canActivate: [ AuthGuard] },
      { path: 'Locales', component: LocalesComponent, canActivate: [ AuthGuard] },
      { path: 'MovimientoAlmacen', component: MovAlmacenComponent, canActivate: [ AuthGuard] },
      { path: 'TipoAlmacen', component: TipoAlmacenComponent, canActivate: [ AuthGuard] },
      { path: 'Almacen', component: AlmacenComponent, canActivate: [ AuthGuard] },
      { path: 'Familia', component: FamiliaComponent, canActivate: [ AuthGuard] },
      { path: 'Unidades_Medida', component: UnidadMedidaComponent, canActivate: [ AuthGuard] },     
      { path: 'CuentaCorriente', component: CuentaCorrienteComponent, canActivate: [ AuthGuard] },
      { path: 'Areas', component: AreasComponent, canActivate: [ AuthGuard] },
      { path: 'Obras', component: ObrasComponent, canActivate: [ AuthGuard] },
      { path: 'CargoPersonal', component: CargoPersonalComponent, canActivate: [ AuthGuard] },

      { path: 'Personal', component: PersonalComponent, canActivate: [ AuthGuard] },
      { path: 'ConfiguracionUbicacion', component: ConfigUbicacionComponent, canActivate: [ AuthGuard] },

      { path: 'ImportarMaterial', component: ImportacionMaterialComponent, canActivate: [ AuthGuard] },

      { path: '**', redirectTo: 'home' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class GestionAlmacenesRoutingModule { }
