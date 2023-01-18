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
import { FamiliaService } from '../../../../services/gestion-almacenes/mantenimientos/familia.service';
declare var $:any;


@Component({
  selector: 'app-familia',
  templateUrl: './familia.component.html',
  styleUrls: ['./familia.component.css']
})
export class FamiliaComponent implements OnInit {

  formParamsFiltro : FormGroup;
  formParams: FormGroup;

  idUserGlobal :number = 0;
  flag_modoEdicion :boolean =false;

  familias  :any[]=[]; 
  filtrarMantenimiento = "";
  empresas:any[]=[]; 
 
  constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService, private funcionGlobalServices : FuncionesglobalesService, 
    private familiaService : FamiliaService, private combosService : CombosService ) {         
    this.idUserGlobal = this.loginService.get_idUsuario();
  }
 
 ngOnInit(): void {
   this.inicializarFormulario_filtro(); 
   this.inicializarFormulario(); 
   this.mostrarInformacion();
 
 }

 inicializarFormulario(){  
    this.formParams= new FormGroup({
        id_FamiliaMaterial: new FormControl('0'), 
        nombre_FamiliaMaterial: new FormControl(''), 
        abreviatura_FamiliaMaterial: new FormControl(''), 
        Anterior_Familia: new FormControl(''), 
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
    this.familiaService.get_mostrar_informacion( this.formParamsFiltro.value.estado  )
    .subscribe((res:RespuestaServer)=>{           
      if (res.ok==true) {         
          this.familias  = res.data;
          setTimeout(()=>{ 
            this.spinner.hide();
          }, 1000);
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

  if (this.formParams.value.nombre_FamiliaMaterial == '' || this.formParams.value.nombre_FamiliaMaterial == null) {
    this.alertasService.Swal_alert('error','Por favor ingrese el nombre');
    return 
  } 

   this.formParams.patchValue({ "usuario_creacion" : this.idUserGlobal });
 
  if ( this.flag_modoEdicion==false) { //// nuevo  
 
     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
     Swal.showLoading(); 
    this.familiaService.set_save_familias (this.formParams.value)
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
    this.familiaService.set_edit_familias (this.formParams.value , this.formParams.value.id_FamiliaMaterial)
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

 

 editar({id_FamiliaMaterial, nombre_FamiliaMaterial,abreviatura_FamiliaMaterial,  estado   }){
   this.flag_modoEdicion=true;
    this.formParams= new FormGroup({
      id_FamiliaMaterial: new FormControl(id_FamiliaMaterial), 
      nombre_FamiliaMaterial: new FormControl(nombre_FamiliaMaterial), 
      abreviatura_FamiliaMaterial: new FormControl(abreviatura_FamiliaMaterial), 
      Anterior_Familia: new FormControl(''), 
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
     this.familiaService.set_anular_familias(objBD.id_FamiliaMaterial).subscribe((res:RespuestaServer)=>{
         Swal.close();        
         if (res.ok ==true) { 
           
           for (const user of this.familias ) {
             if (user.id_FamiliaMaterial == objBD.id_FamiliaMaterial ) {
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


