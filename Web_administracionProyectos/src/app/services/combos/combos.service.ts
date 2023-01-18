import { Injectable } from '@angular/core';
 
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';

import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../../environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const HttpUploadOptions = {
  headers: new HttpHeaders({ "Content-Type": "multipart/form-data" })
}


@Injectable({
  providedIn: 'root'
})
export class CombosService {

  URL = environment.URL_API;
  empresas:any[]=[];
  delegaciones:any[]=[];
  locales:any[]=[];
  tiposAlmacen:any[]=[];
  servicios:any[]=[]; 
  tiposObras :any[]=[];   
  estadosSigetrama:any[]=[]; 
  clientes:any[]=[]; 
  cargos:any[]=[];

  constructor(private http:HttpClient) {  }  

  get_empresas(){
    if (this.empresas.length > 0) {
      return of( this.empresas )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '2');
      parametros = parametros.append('filtro', '');
  
      return this.http.get( this.URL + 'tblLocales' , {params: parametros})
                  .pipe(map((res:any)=>{
                        this.empresas = res.data;
                        return res.data;
                  }) );
    }
  }

  
  get_delegaciones(id_usuario:number){
    if (this.delegaciones.length > 0) {
      return of( this.delegaciones )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '2');
      parametros = parametros.append('filtro', String(id_usuario));
  
      return this.http.get( this.URL + 'tblAlm_Almacenes' , {params: parametros})
                  .pipe(map((res:any)=>{
                        this.delegaciones = res.data;
                        return res.data;
                  }) );
    }
  }
   
  get_locales(id_usuario:number){
    if (this.locales.length > 0) {
      return of( this.locales )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '3');
      parametros = parametros.append('filtro', String(id_usuario));
  
      return this.http.get( this.URL + 'tblAlm_Almacenes' , {params: parametros})
                  .pipe(map((res:any)=>{
                        this.locales = res.data;
                        return res.data;
                  }) );
    }
  }

  get_tiposAlmacenes(){
    if (this.tiposAlmacen.length > 0) {
      return of( this.tiposAlmacen )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '4');
      parametros = parametros.append('filtro', '');
  
      return this.http.get( this.URL + 'tblAlm_Almacenes' , {params: parametros})
                  .pipe(map((res:any)=>{
                        this.tiposAlmacen = res.data;
                        return res.data;
                  }) );
    }
  }

  
  get_servicios(){
    if (this.servicios.length > 0) {
      return of( this.servicios )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '4');
      parametros = parametros.append('filtro', '');
  
      return this.http.get( this.URL + 'tblCuentaCorriente' , {params: parametros})
                  .pipe(map((res:any)=>{
                        this.servicios = res.data;
                        return res.data;
                  }) );
    }
  }

  get_tiposObras(){
    if (this.tiposObras.length > 0) {
      return of( this.tiposObras )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '8');
      parametros = parametros.append('filtro', '');
  
      return this.http.get( this.URL + 'tblCuentaCorriente' , {params: parametros})
                  .pipe(map((res:any)=>{
                        this.tiposObras = res.data;
                        return res.data;
                  }) );
    }
  }

  get_estadosSigetramas(id_usuario:number){
    if (this.estadosSigetrama.length > 0) {
      return of( this.estadosSigetrama )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '3');
      parametros = parametros.append('filtro', id_usuario);
  
      return this.http.get( this.URL + 'tblObra_TD' , {params: parametros})
                  .pipe(map((res:any)=>{
                        this.estadosSigetrama = res.data;
                        return res.data;
                  }) );
    }
  }

  get_clientes(id_usuario:number){
    if (this.clientes.length > 0) {
      return of( this.clientes )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '4');
      parametros = parametros.append('filtro', id_usuario);
  
      return this.http.get( this.URL + 'tblObra_TD' , {params: parametros})
                  .pipe(map((res:any)=>{
                        this.clientes = res.data;
                        return res.data;
                  }) );
    }
  }

  get_cargos(){
    if (this.cargos.length > 0) {
      return of( this.cargos )
    }else{
      let parametros = new HttpParams();
      parametros = parametros.append('opcion', '4');
      parametros = parametros.append('filtro', '');
  
      return this.http.get( this.URL + 'tblPersonal' , {params: parametros})
                  .pipe(map((res:any)=>{
                        this.cargos = res.data;
                        return res.data;
                  }) );
    }
  }

  

}
