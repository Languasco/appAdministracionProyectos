import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';

import { of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const HttpUploadOptions = {
  headers: new HttpHeaders({ "Content-Type": "multipart/form-data" })
}


@Injectable({
  providedIn: 'root'
})
export class CuentaCorrienteService {


  URL = environment.URL_API;
  constructor(private http:HttpClient) { }  

  get_mostrar_informacion(estado:number): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '1');
    parametros = parametros.append('filtro', estado);

    return this.http.get( this.URL + 'tblCuentaCorriente' , {params: parametros});
  }

  set_save_cuentasCorrientes(objMantenimiento:any):any{
    return this.http.post(this.URL + 'tblCuentaCorriente', JSON.stringify(objMantenimiento), httpOptions);
  }

  set_edit_cuentasCorrientes(objMantenimiento:any, id :number):any{
    return this.http.put(this.URL + 'tblCuentaCorriente/' + id , JSON.stringify(objMantenimiento), httpOptions);
  }

  set_anular_cuentasCorrientes(id : number):any{ 
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '2');
    parametros = parametros.append('filtro', id);

    return this.http.get( this.URL + 'tblCuentaCorriente' , {params: parametros});
  }

  get_verificar_ruc(ruc:string){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '3');
    parametros = parametros.append('filtro', ruc);

    return this.http.get( this.URL + 'tblCuentaCorriente' , {params: parametros}).toPromise();
  }

  set_save_serviciosCorrientes(objMantenimiento:any):any{
    return this.http.post(this.URL + 'tblCuentaCorriente_Servicio', JSON.stringify(objMantenimiento), httpOptions);
  }

  get_cuentasCorrientes_servicios(idCuentaCorriente:number): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '6');
    parametros = parametros.append('filtro', idCuentaCorriente);

    return this.http.get( this.URL + 'tblCuentaCorriente' , {params: parametros});
  }

  set_eliminarCuentasCorrientes_servicios(id_CtaCte_Servicio:number): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '7');
    parametros = parametros.append('filtro', id_CtaCte_Servicio);

    return this.http.get( this.URL + 'tblCuentaCorriente' , {params: parametros});
  }

}
