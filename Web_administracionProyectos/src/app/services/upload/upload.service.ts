import { Injectable } from '@angular/core';
 
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';


const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const HttpUploadOptions = {
  headers: new HttpHeaders({ "Content-Type": "multipart/form-data" })
}
 
@Injectable({
  providedIn: 'root'
})
export class UploadService {

  URL = environment.URL_API;
  constructor(private http:HttpClient) { }

  upload_imagen_personal(file:any, idPersonal:number, idusuarioLogin : any):any {   
    const formData = new FormData();   
    formData.append('file', file);
    const filtro =  idPersonal + '|' + idusuarioLogin;
    return this.http.post(this.URL + 'Uploads/post_imagenPersonal?filtros=' + filtro, formData);    
  }

}
