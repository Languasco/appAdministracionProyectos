import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { HomeComponent } from './home/home.component';

const router:Routes = [
  {
     path : '', component : HomeComponent , canActivate: [ AuthGuard]
  },
  {
    path : 'Autentificacion',
    loadChildren :()=> import('./autentificacion/autentificacion.module').then(m => m.AutentificacionModule )
  },
  {
    path : 'GestionAlmacenes',
    loadChildren :()=> import('./gestion-almacenes/gestion-almacenes.module').then(m => m.GestionAlmacenesModule )
  },
  {
    path : 'GestionProyectos',
    loadChildren :()=> import('./gestion-proyectos/gestion-proyectos.module').then(m => m.GestionProyectosModule )
  },
  { 
    path: '**', redirectTo: '' 
  }

];

@NgModule({
  imports: [
    RouterModule.forRoot(router,{useHash:true})
 ],
 exports : [
   RouterModule
 ]
})
export class AppRoutingModule { }

