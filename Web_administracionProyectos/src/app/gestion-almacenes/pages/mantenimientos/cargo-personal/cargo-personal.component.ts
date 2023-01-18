import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AlertasService } from '../../../../services/alertas/alertas.service';
 
import { FuncionesglobalesService } from '../../../../services/funciones/funcionesglobales.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoginService } from '../../../../services/login/login.service';
import Swal from 'sweetalert2'; 
 
 
import { RespuestaServer } from '../../../../models/respuestaServer.models';
import { CargoPersonalService } from 'src/app/services/gestion-almacenes/mantenimientos/cargo-personal.service';
 
declare var $:any;

@Component({
  selector: 'app-cargo-personal',
  templateUrl: './cargo-personal.component.html',
  styleUrls: ['./cargo-personal.component.css']
})
export class CargoPersonalComponent implements OnInit {

  formParamsFiltro : FormGroup;
  formParams: FormGroup;

  idUserGlobal :number = 0;
  flag_modoEdicion :boolean =false;

  cargos  :any[]=[]; 
  filtrarMantenimiento = "";
  empresas:any[]=[]; 
 
  constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService, private funcionGlobalServices : FuncionesglobalesService, 
    private cargoPersonalService : CargoPersonalService ) {         
    this.idUserGlobal = this.loginService.get_idUsuario();
  }
 
 ngOnInit(): void {
   this.inicializarFormulario_filtro(); 
   this.inicializarFormulario(); 
   this.mostrarInformacion();
 
 }

 inicializarFormulario(){  
    this.formParams= new FormGroup({
        id_Cargo: new FormControl('0'), 
        nombre_cargo: new FormControl(''), 
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
    this.cargoPersonalService.get_mostrar_informacion( this.formParamsFiltro.value.estado  )
    .subscribe((res:RespuestaServer)=>{           
      if (res.ok==true) {         
          this.cargos  = res.data;
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

  if (this.formParams.value.nombre_cargo == '' || this.formParams.value.nombre_cargo == null) {
    this.alertasService.Swal_alert('error','Por favor ingrese el nombre');
    return 
  } 

   this.formParams.patchValue({ "usuario_creacion" : this.idUserGlobal });
 
  if ( this.flag_modoEdicion==false) { //// nuevo  

    const  nombreCargo :any  = await this.cargoPersonalService.get_verificar_nombre(String(this.formParams.value.nombre_cargo).toUpperCase()  );
    if (nombreCargo.ok) {
     Swal.close();
     this.alertasService.Swal_alert('error','El cargo ya esta registrado, verifique..');
     return;
   }  
 
     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
     Swal.showLoading(); 
    this.cargoPersonalService.set_save_cargos(this.formParams.value)
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
    this.cargoPersonalService.set_edit_cargos(this.formParams.value , this.formParams.value.id_Cargo)
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

 

 editar({id_Cargo, nombre_cargo,  estado   }){
   this.flag_modoEdicion=true;
    this.formParams= new FormGroup({
      id_Cargo: new FormControl(id_Cargo), 
      nombre_cargo: new FormControl(nombre_cargo), 
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
     this.cargoPersonalService.set_anular_cargos(objBD.id_Cargo).subscribe((res:RespuestaServer)=>{
         Swal.close();        
         if (res.ok ==true) { 
           
           for (const user of this.cargos ) {
             if (user.id_Cargo == objBD.id_Cargo ) {
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
