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
export class ObrasService {

  URL = environment.URL_API;
  constructor(private http:HttpClient) { }  

  get_mostrar_informacion({ delegacion, proyecto, area, tipoObra, estado}): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '1');
    parametros = parametros.append('filtro', delegacion +'|'+ proyecto +'|'+ area +'|'+ tipoObra +'|'+ estado  );

    return this.http.get( this.URL + 'tblObra_TD' , {params: parametros});
  }

  set_save_obras(objMantenimiento:any):any{
    return this.http.post(this.URL + 'tblObra_TD', JSON.stringify(objMantenimiento), httpOptions);
  }

  set_edit_obras(objMantenimiento:any, id :number):any{
    return this.http.put(this.URL + 'tblObra_TD/' + id , JSON.stringify(objMantenimiento), httpOptions);
  }

  set_anular_obras(id : number):any{ 
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '2');
    parametros = parametros.append('filtro', id);

    return this.http.get( this.URL + 'tblObra_TD' , {params: parametros});
  }

  get_verificar_nroObra(nroObra:string){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '5');
    parametros = parametros.append('filtro', nroObra);

    return this.http.get( this.URL + 'tblObra_TD' , {params: parametros}).toPromise();
  }

  set_save_obraEmpresa(objMantenimiento:any):any{
    return this.http.post(this.URL + 'tblObra_TD_Empresa', JSON.stringify(objMantenimiento), httpOptions);
  }

  set_eliminarObraEmpresa(id_ObraTD_Empresa:number): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '8');
    parametros = parametros.append('filtro', id_ObraTD_Empresa);

    return this.http.get( this.URL + 'tblObra_TD' , {params: parametros});
  }

  get_obrasId(idObraCab:number): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '6');
    parametros = parametros.append('filtro', idObraCab);

    return this.http.get( this.URL + 'tblObra_TD' , {params: parametros});
  }

  get_obrasEmpresasDet(idObra:number): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '7');
    parametros = parametros.append('filtro', idObra);

    return this.http.get( this.URL + 'tblObra_TD' , {params: parametros});
  }


}
