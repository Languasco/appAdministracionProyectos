import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const HttpUploadOptions = {
  headers: new HttpHeaders({ "Content-Type": "multipart/form-data" })
}

@Injectable({
  providedIn: 'root'
})
export class PersonalService {

  URL = environment.URL_API;
  constructor(private http:HttpClient) { }  

  get_mostrar_informacion({ delegacion, proyecto, personal, estado}, id_usuario: number): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '1');
    parametros = parametros.append('filtro', delegacion +'|'+ proyecto +'|'+ personal +'|'+ estado +'|'+ id_usuario   );

    return this.http.get( this.URL + 'tblPersonal' , {params: parametros});
  }

  set_save_personal(objMantenimiento:any):any{
    return this.http.post(this.URL + 'tblPersonal', JSON.stringify(objMantenimiento), httpOptions);
  }

  set_edit_personal(objMantenimiento:any, id :number):any{
    return this.http.put(this.URL + 'tblPersonal/' + id , JSON.stringify(objMantenimiento), httpOptions);
  }

  set_anular_personal(id : number):any{ 
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '2');
    parametros = parametros.append('filtro', id);

    return this.http.get( this.URL + 'tblPersonal' , {params: parametros});
  }

  get_verificar_nroDoc(nroDoc:string){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '3');
    parametros = parametros.append('filtro', nroDoc);

    return this.http.get( this.URL + 'tblPersonal' , {params: parametros}).toPromise();
  }

  set_save_obraEmpresa(objMantenimiento:any):any{
    return this.http.post(this.URL + 'tblObra_TD_Empresa', JSON.stringify(objMantenimiento), httpOptions);
  }

  set_eliminarObraEmpresa(id_ObraTD_Empresa:number): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '8');
    parametros = parametros.append('filtro', id_ObraTD_Empresa);

    return this.http.get( this.URL + 'tblPersonal' , {params: parametros});
  }

  get_personalId(idPersonal:number): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '5');
    parametros = parametros.append('filtro', idPersonal);

    return this.http.get( this.URL + 'tblPersonal' , {params: parametros});
  }

  get_obrasEmpresasDet(idObra:number): any{
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '7');
    parametros = parametros.append('filtro', idObra);

    return this.http.get( this.URL + 'tblPersonal' , {params: parametros});
  }


}
