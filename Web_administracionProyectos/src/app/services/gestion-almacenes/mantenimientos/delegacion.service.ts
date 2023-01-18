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

export class DelegacionService {

  URL = environment.URL_API;
  constructor(private http:HttpClient) { }  

  get_mostrar_informacion(estado:number): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '1');
    parametros = parametros.append('filtro', estado);

    return this.http.get( this.URL + 'tblDelegacion' , {params: parametros});
  }
 
  get_verificar_codigo(codigo_delegacion:string){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '2');
    parametros = parametros.append('filtro', codigo_delegacion);

    return this.http.get( this.URL + 'tblDelegacion' , {params: parametros}).toPromise();
  }

  set_save_delegaciones(objMantenimiento:any):any{
    return this.http.post(this.URL + 'tblDelegacion', JSON.stringify(objMantenimiento), httpOptions);
  }

  set_edit_delegaciones(objMantenimiento:any, id :number):any{
    return this.http.put(this.URL + 'tblDelegacion/' + id , JSON.stringify(objMantenimiento), httpOptions);
  }

  set_anular_delegacion(id : number):any{ 
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '3');
    parametros = parametros.append('filtro', id);

    return this.http.get( this.URL + 'tblDelegacion' , {params: parametros});
  }


}
