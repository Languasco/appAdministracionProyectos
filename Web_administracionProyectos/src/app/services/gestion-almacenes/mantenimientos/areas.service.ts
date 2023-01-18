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
export class AreasService {
  URL = environment.URL_API;
  constructor(private http:HttpClient) { }  

  get_mostrar_informacion(estado:number): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '1');
    parametros = parametros.append('filtro', estado);

    return this.http.get( this.URL + 'tblAlm_Tipos_Almacen' , {params: parametros});
  }

  set_save_TiposAlmacen(objMantenimiento:any):any{
    return this.http.post(this.URL + 'tblAlm_Tipos_Almacen', JSON.stringify(objMantenimiento), httpOptions);
  }

  set_edit_TiposAlmacen(objMantenimiento:any, id :number):any{
    return this.http.put(this.URL + 'tblAlm_Tipos_Almacen/' + id , JSON.stringify(objMantenimiento), httpOptions);
  }

  set_anular_TiposAlmacen(id : number):any{ 
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '2');
    parametros = parametros.append('filtro', id);

    return this.http.get( this.URL + 'tblAlm_Tipos_Almacen' , {params: parametros});
  }

}
