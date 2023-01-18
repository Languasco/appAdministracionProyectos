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
export class LocalesService {

  URL = environment.URL_API;
  constructor(private http:HttpClient) { }  

  get_mostrar_informacion(estado:number): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '1');
    parametros = parametros.append('filtro', estado);

    return this.http.get( this.URL + 'tblLocales' , {params: parametros});
  }
 
  get_verificar_nombreTurno(descTurno:string){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '1');
    parametros = parametros.append('filtro', descTurno);

    return this.http.get( this.URL + 'Mantenimientos' , {params: parametros}).toPromise();
  }

  set_save_locales(objMantenimiento:any):any{
    return this.http.post(this.URL + 'tblLocales', JSON.stringify(objMantenimiento), httpOptions);
  }

  set_edit_locales(objMantenimiento:any, id :number):any{
    return this.http.put(this.URL + 'tblLocales/' + id , JSON.stringify(objMantenimiento), httpOptions);
  }

  set_anular_local(id : number):any{ 
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '3');
    parametros = parametros.append('filtro', id);

    return this.http.get( this.URL + 'tblLocales' , {params: parametros});
  }

  get_empesas(descTurno:string){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '1');
    parametros = parametros.append('filtro', descTurno);

    return this.http.get( this.URL + 'Mantenimientos' , {params: parametros}).toPromise();
  }

}
