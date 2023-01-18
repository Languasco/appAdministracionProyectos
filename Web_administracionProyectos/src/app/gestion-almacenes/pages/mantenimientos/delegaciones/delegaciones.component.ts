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
import { DelegacionService } from '../../../../services/gestion-almacenes/mantenimientos/delegacion.service';
declare var $:any;

@Component({
  selector: 'app-delegaciones',
  templateUrl: './delegaciones.component.html',
  styleUrls: ['./delegaciones.component.css']
})

export class DelegacionesComponent implements OnInit {

  formParamsFiltro : FormGroup;
  formParams: FormGroup;

  idUserGlobal :number = 0;
  flag_modoEdicion :boolean =false;

  delegaciones :any[]=[]; 
  filtrarMantenimiento = "";
  empresas:any[]=[]; 
 
  constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService, private funcionGlobalServices : FuncionesglobalesService, 
    private delegacionService : DelegacionService, private combosService : CombosService ) {         
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
        id_Delegacion: new FormControl('0'), 
        id_Empresa: new FormControl('0'), 
        codigo_delegacion: new FormControl(''), 
        nombre_delegacion: new FormControl(''), 
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
    this.delegacionService.get_mostrar_informacion( this.formParamsFiltro.value.estado  )
    .subscribe((res:RespuestaServer)=>{           
      if (res.ok==true) {         
          this.delegaciones = res.data;
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
      $('#txt_codigo').removeClass('disabledForm');
      $('#modal_mantenimiento').modal('show');  
    },0); 
 } 

 async saveUpdate(){ 

  if (this.formParams.value.id_Empresa == '0' || this.formParams.value.id_Empresa == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione la Empresa');
    return 
  } 

  if (this.formParams.value.codigo_delegacion == '' || this.formParams.value.codigo_delegacion == null) {
    this.alertasService.Swal_alert('error','Por favor ingrese el nombre codigo de la delegacion');
    return 
  } 

  if (this.formParams.value.nombre_delegacion == '' || this.formParams.value.nombre_delegacion == null) {
    this.alertasService.Swal_alert('error','Por favor ingrese el nombre de la delegacion');
    return 
  } 

   this.formParams.patchValue({ "usuario_creacion" : this.idUserGlobal });
 
  if ( this.flag_modoEdicion==false) { //// nuevo  
 
    Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
    Swal.showLoading(); 

    const  descDist :any  = await this.delegacionService.get_verificar_codigo(this.formParams.value.codigo_delegacion);
      if (descDist.ok) {
       Swal.close();
       this.alertasService.Swal_alert('error','El codigo de la delegacion ya esta registrada, verifique..');
       return;
     }  
  
     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
     Swal.showLoading(); 
    this.delegacionService.set_save_delegaciones(this.formParams.value)
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
    this.delegacionService.set_edit_delegaciones(this.formParams.value , this.formParams.value.id_Delegacion)
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


 editar({id_Delegacion, id_Empresa, codigo_delegacion, nombre_delegacion, estado, usuario_creacion   }){
   
   this.flag_modoEdicion=true;

   this.formParams= new FormGroup({
      id_Delegacion: new FormControl(id_Delegacion), 
      id_Empresa: new FormControl(id_Empresa), 
      codigo_delegacion: new FormControl(codigo_delegacion), 
      nombre_delegacion: new FormControl(nombre_delegacion), 
      estado: new FormControl(estado), 
      usuario_creacion: new FormControl(this.idUserGlobal), 
  })
   setTimeout(()=>{ // 
    $('#txt_codigo').addClass('disabledForm');
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
     this.delegacionService.set_anular_delegacion(objBD.id_Delegacion).subscribe((res:RespuestaServer)=>{
         Swal.close();        
         if (res.ok ==true) { 
           
           for (const user of this.delegaciones) {
             if (user.id_Delegacion == objBD.id_Delegacion ) {
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


