import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AlertasService } from '../../../../services/alertas/alertas.service';
 
import { FuncionesglobalesService } from '../../../../services/funciones/funcionesglobales.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoginService } from '../../../../services/login/login.service';
import Swal from 'sweetalert2'; 
  
import { RespuestaServer } from '../../../../models/respuestaServer.models';
import { combineLatest } from 'rxjs';
import { CombosService } from 'src/app/services/combos/combos.service';
import { TiposAlmacenService } from '../../../../services/gestion-almacenes/mantenimientos/tipos-almacen.service';
declare var $:any;

@Component({
  selector: 'app-areas',
  templateUrl: './areas.component.html',
  styleUrls: ['./areas.component.css']
})
export class AreasComponent implements OnInit {

  formParamsFiltro : FormGroup;
  formParams: FormGroup;

  idUserGlobal :number = 0;
  flag_modoEdicion :boolean =false;

  areas  :any[]=[]; 
  filtrarMantenimiento = "";
  empresas:any[]=[]; 
 
  constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService, private funcionGlobalServices : FuncionesglobalesService, 
    private tiposAlmacenService : TiposAlmacenService, private combosService : CombosService ) {         
    this.idUserGlobal = this.loginService.get_idUsuario();
  }
 
 ngOnInit(): void {
   this.inicializarFormulario_filtro(); 
   this.inicializarFormulario(); 
   this.mostrarInformacion();
 
 }

 inicializarFormulario(){ 
    this.formParams= new FormGroup({
        id_Area: new FormControl('0'), 
        nombre_area: new FormControl(''), 
        estado: new FormControl('1'), 
        usuario_creacion: new FormControl(this.idUserGlobal), 
    }) 
 }

 inicializarFormulario_filtro(){ 
    this.formParamsFiltro = new FormGroup({
      estado : new FormControl('1'),   
    }) 
  }

 mostrarInformacion(){ 
    this.spinner.show();
    this.tiposAlmacenService.get_mostrar_informacion( this.formParamsFiltro.value.estado  )
    .subscribe((res:RespuestaServer)=>{  
      this.spinner.hide();         
      if (res.ok==true) {         
          this.areas  = res.data;
      }else{
        this.spinner.hide();
        this.alertasService.Swal_alert('error', JSON.stringify(res.data));
        alert(JSON.stringify(res.data));
      }
    })
  }
  
 cerrarModal(){
    setTimeout(()=>{ // 
      $('#modal_mantenimiento').modal('hide');  
    },0); 
 }

 nuevo(){
    this.flag_modoEdicion = false;
    this.inicializarFormulario();  
    setTimeout(()=>{ // 
      $('#modal_mantenimiento').modal('show');  
    },0); 
 } 

 async saveUpdate(){ 

  if (this.formParams.value.nombre_area == '' || this.formParams.value.nombre_area == null) {
    this.alertasService.Swal_alert('error','Por favor ingrese el nombre');
    return 
  } 

   this.formParams.patchValue({ "usuario_creacion" : this.idUserGlobal });
 
  if ( this.flag_modoEdicion==false) { //// nuevo  
 
     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
     Swal.showLoading(); 
    this.tiposAlmacenService.set_save_TiposAlmacen (this.formParams.value)
      .subscribe((res:any)=>{  
        Swal.close();    
        if (res.ok ==true) {     
          this.flag_modoEdicion = true;
          this.mostrarInformacion();
          this.alertasService.Swal_Success('Se agrego correctamente..');
          this.cerrarModal();
        }else{
          this.alertasService.Swal_alert('error', JSON.stringify(res.data));
          alert(JSON.stringify(res.data));
        }
      })
     
   }else{ /// editar

     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Actualizando, espere por favor'  })
     Swal.showLoading();    
    this.tiposAlmacenService.set_edit_TiposAlmacen (this.formParams.value , this.formParams.value.id_Area)
      .subscribe((res:any)=>{  
        Swal.close();    
        if (res.ok ==true) {     
          this.flag_modoEdicion = true;
          this.mostrarInformacion();
          this.alertasService.Swal_Success('Se actualizo correctamente..');
          this.cerrarModal();
        }else{
          this.alertasService.Swal_alert('error', JSON.stringify(res.data));
          alert(JSON.stringify(res.data));
        }
      })
   }
 } 

 editar({id_Area, nombre_area, estado   }){
   this.flag_modoEdicion=true;
    this.formParams= new FormGroup({
      id_Area: new FormControl(id_Area), 
      nombre_area: new FormControl(nombre_area), 
      estado: new FormControl(estado), 
      usuario_creacion: new FormControl(this.idUserGlobal), 
  })
   setTimeout(()=>{ // 
    $('#modal_mantenimiento').modal('show');  
  },0);  
 } 

 anular(objBD:any){
   if (objBD.estado ===2 || objBD.estado =='2') {      
     return;      
   }

   this.alertasService.Swal_Question('Sistemas', 'Esta seguro de anular ?')
   .then((result)=>{
     if(result.value){
      Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Anulando, espere por favor'  })
      Swal.showLoading();    
     this.tiposAlmacenService.set_anular_TiposAlmacen(objBD.id_Area).subscribe((res:RespuestaServer)=>{
         Swal.close();        
         if (res.ok ==true) { 
           
           for (const user of this.areas ) {
             if (user.id_Area == objBD.id_Area ) {
                 user.estado = 2;
                 break;
             }
           }
           this.alertasService.Swal_Success('Se anulo correctamente..')  
    
         }else{
           this.alertasService.Swal_alert('error', JSON.stringify(res.data));
           alert(JSON.stringify(res.data));
         }
       })
        
     }
    }) 

 }


}