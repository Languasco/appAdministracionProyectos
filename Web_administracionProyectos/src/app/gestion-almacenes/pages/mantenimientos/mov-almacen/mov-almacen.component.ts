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
import { MovAlmacenService } from '../../../../services/gestion-almacenes/mantenimientos/mov-almacen.service';
declare var $:any;

@Component({
  selector: 'app-mov-almacen',
  templateUrl: './mov-almacen.component.html',
  styleUrls: ['./mov-almacen.component.css']
})
export class MovAlmacenComponent implements OnInit {

  formParamsFiltro : FormGroup;
  formParams: FormGroup;

  idUserGlobal :number = 0;
  flag_modoEdicion :boolean =false;

  delegaciones :any[]=[]; 
  filtrarMantenimiento = "";
 
 
  constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService, private funcionGlobalServices : FuncionesglobalesService, 
    private movAlmacenService : MovAlmacenService, private combosService : CombosService ) {         
    this.idUserGlobal = this.loginService.get_idUsuario();
  }
 
 ngOnInit(): void {
   this.inicializarFormulario_filtro(); 
   this.inicializarFormulario(); 
   this.mostrarInformacion();
 }

 inicializarFormulario(){ 
    this.formParams= new FormGroup({
        id_MovAlmacen: new FormControl(''),         
        descripcion_MovAlmacen: new FormControl(''), 
        abreviatura_MovAlmacen: new FormControl(''), 
        tipo_MovAlmacen: new FormControl('0'), 
        // codigo_Sunat: new FormControl(''), 
        esAfecto : new FormControl('1'), 
        afectaStock_MovAlmacen: new FormControl(''), 
        noAfectaStock_MovAlmacen: new FormControl(''), 
        // Anterior_Movimiento: new FormControl(''), 
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
    this.movAlmacenService.get_mostrar_informacion( this.formParamsFiltro.value.estado  )
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

  if (this.formParams.value.id_MovAlmacen == '' || this.formParams.value.id_MovAlmacen == null) {
    this.alertasService.Swal_alert('error','Por favor ingrese el codigo del Movimento');
    return 
  } 

  if (this.formParams.value.descripcion_MovAlmacen == '' || this.formParams.value.descripcion_MovAlmacen == null) {
    this.alertasService.Swal_alert('error','Por favor ingrese la descripcion');
    return 
  } 

  if (this.formParams.value.tipo_MovAlmacen == '0' || this.formParams.value.tipo_MovAlmacen == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione el Tipo de Movimiento');
    return 
  } 

  if (this.formParams.value.esAfecto == 1){
    this.formParams.patchValue({ "afectaStock_MovAlmacen" : 'SI' , "noAfectaStock_MovAlmacen" : 'NO' });
  }else{
    this.formParams.patchValue({ "afectaStock_MovAlmacen" : 'NO' , "noAfectaStock_MovAlmacen" : 'SI' });
  }

   this.formParams.patchValue({ "usuario_creacion" : this.idUserGlobal });
 
  if ( this.flag_modoEdicion==false) { //// nuevo  
 
    Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
    Swal.showLoading(); 

    const  codigoMov :any  = await this.movAlmacenService.get_verificar_codigo(this.formParams.value.id_MovAlmacen);
      if (codigoMov.ok) {
       Swal.close();
       this.alertasService.Swal_alert('error','El codigo del Movimiento ya esta registrada, verifique..');
       return;
     }  
  
     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
     Swal.showLoading(); 
    this.movAlmacenService.set_save_movAlmacen(this.formParams.value)
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
    this.movAlmacenService.set_edit_movAlmacen(this.formParams.value , this.formParams.value.id_MovAlmacen)
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


 editar({id_MovAlmacen, descripcion_MovAlmacen, abreviatura_MovAlmacen, tipo_MovAlmacen, afectaStock_MovAlmacen, noAfectaStock_MovAlmacen, estado }){
   
  this.flag_modoEdicion=true;
  
   const estaAfecto = afectaStock_MovAlmacen == 'SI' ? 1 : 2;
    this.formParams= new FormGroup({
        id_MovAlmacen: new FormControl(id_MovAlmacen),         
        descripcion_MovAlmacen: new FormControl(descripcion_MovAlmacen), 
        abreviatura_MovAlmacen: new FormControl(abreviatura_MovAlmacen), 
        tipo_MovAlmacen: new FormControl(tipo_MovAlmacen), 
        // codigo_Sunat: new FormControl(''), 
        esAfecto : new FormControl( String(estaAfecto) ), 
        afectaStock_MovAlmacen: new FormControl(afectaStock_MovAlmacen), 
        noAfectaStock_MovAlmacen: new FormControl(noAfectaStock_MovAlmacen), 
        // Anterior_Movimiento: new FormControl(''), 
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
     this.movAlmacenService.set_anular_movAlmacen(objBD.id_MovAlmacen).subscribe((res:RespuestaServer)=>{
         Swal.close();        
         if (res.ok ==true) { 
           
           for (const user of this.delegaciones) {
             if (user.id_MovAlmacen == objBD.id_MovAlmacen ) {
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

