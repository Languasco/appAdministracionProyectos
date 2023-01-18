import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AlertasService } from '../../../../services/alertas/alertas.service';
 
import { FuncionesglobalesService } from '../../../../services/funciones/funcionesglobales.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoginService } from '../../../../services/login/login.service';
import Swal from 'sweetalert2'; 
 
import { LocalesService } from 'src/app/services/gestion-almacenes/mantenimientos/locales.service';
import { RespuestaServer } from '../../../../models/respuestaServer.models';
import { combineLatest } from 'rxjs';
import { CombosService } from 'src/app/services/combos/combos.service';
declare var $:any;

@Component({
  selector: 'app-locales',
  templateUrl: './locales.component.html',
  styleUrls: ['./locales.component.css']
})
 
export class LocalesComponent implements OnInit {

  formParamsFiltro : FormGroup;
  formParams: FormGroup;

  idUserGlobal :number = 0;
  flag_modoEdicion :boolean =false;

  locales :any[]=[]; 
  filtrarMantenimiento = "";
  empresas:any[]=[]; 
 
  constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService, private funcionGlobalServices : FuncionesglobalesService, 
    private localesService : LocalesService, private combosService : CombosService ) {         
    this.idUserGlobal = this.loginService.get_idUsuario();
  }
 
 ngOnInit(): void {
   this.inicializarFormulario_filtro(); 
   this.inicializarFormulario(); 
   this.mostrarInformacion();
   this.getCargarCombos();
 }

 inicializarFormulario(){ 
    this.formParams= new FormGroup({
        Id_Local: new FormControl('0'), 
        Id_Empresa: new FormControl('0'), 
        nombre_local: new FormControl(''), 
        direccion_local: new FormControl(''), 
        Id_Ubicacion: new FormControl(''), 
        orden_local: new FormControl(''), 
        estado: new FormControl('1'), 
        usuario_creacion: new FormControl(this.idUserGlobal), 
    }) 
 }

 inicializarFormulario_filtro(){ 
    this.formParamsFiltro = new FormGroup({
      estado : new FormControl('1'),   
    }) 
  }

  getCargarCombos(){ 
    this.spinner.show();
    combineLatest([  this.combosService.get_empresas()])
    .subscribe( ([ _empresas ])=>{     
    this.spinner.hide();   
    this.empresas = _empresas; 
 
    })
}

 mostrarInformacion(){ 
    this.spinner.show();
    this.localesService.get_mostrar_informacion( this.formParamsFiltro.value.estado  )
    .subscribe((res:RespuestaServer)=>{     
      this.spinner.hide();      
      if (res.ok==true) {         
          this.locales = res.data;
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

  if (this.formParams.value.Id_Empresa == '0' || this.formParams.value.Id_Empresa == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione la Empresa');
    return 
  } 

  if (this.formParams.value.nombre_local == '' || this.formParams.value.nombre_local == null) {
    this.alertasService.Swal_alert('error','Por favor ingrese el nombre local');
    return 
  } 

   this.formParams.patchValue({ "usuario_creacion" : this.idUserGlobal });
 
  if ( this.flag_modoEdicion==false) { //// nuevo  
 
     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
     Swal.showLoading(); 
    this.localesService.set_save_locales(this.formParams.value)
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
    this.localesService.set_edit_locales(this.formParams.value , this.formParams.value.Id_Local)
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

 

 editar({Id_Local, Id_Empresa, nombre_local, direccion_local, Id_Ubicacion, orden_local, estado, usuario_creacion   }){
   this.flag_modoEdicion=true;
    this.formParams= new FormGroup({
      Id_Local: new FormControl(Id_Local), 
      Id_Empresa: new FormControl(Id_Empresa), 
      nombre_local: new FormControl(nombre_local), 
      direccion_local: new FormControl(direccion_local), 
      Id_Ubicacion: new FormControl(Id_Ubicacion), 
      orden_local: new FormControl(orden_local), 
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
     this.localesService.set_anular_local(objBD.Id_Local).subscribe((res:RespuestaServer)=>{
         Swal.close();        
         if (res.ok ==true) { 
           
           for (const user of this.locales) {
             if (user.Id_Local == objBD.Id_Local ) {
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

