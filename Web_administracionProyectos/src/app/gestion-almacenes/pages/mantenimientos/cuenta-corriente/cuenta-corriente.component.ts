import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { AlertasService } from '../../../../services/alertas/alertas.service';
 
import { FuncionesglobalesService } from '../../../../services/funciones/funcionesglobales.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoginService } from '../../../../services/login/login.service';
import Swal from 'sweetalert2'; 
  
import { RespuestaServer } from '../../../../models/respuestaServer.models';
import { combineLatest } from 'rxjs';
import { CombosService } from 'src/app/services/combos/combos.service';
import { CuentaCorrienteService } from '../../../../services/gestion-almacenes/mantenimientos/cuenta-corriente.service';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap';
 
declare var $:any;

@Component({
  selector: 'app-cuenta-corriente',
  templateUrl: './cuenta-corriente.component.html',
  styleUrls: ['./cuenta-corriente.component.css']
})
export class CuentaCorrienteComponent implements OnInit {

  formParamsFiltro : FormGroup;
  formParams: FormGroup;
  formParamsServicio : FormGroup;

  idUserGlobal :number = 0;
  flag_modoEdicion :boolean =false;

  cuentasCorrientes  :any[]=[]; 
  filtrarMantenimiento = "";
  servicios:any[]=[]; 
  idCuentaCorrienteGlobal :number = 0;
  existePrioridad=false;

  serviciosCab  :any[]=[]; 
 
  @ViewChild('staticTabsPrincipal', { static: false }) staticTabsPrincipal: TabsetComponent;
  constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService, private funcionGlobalServices : FuncionesglobalesService, 
    private cuentaCorrienteService : CuentaCorrienteService, private combosService : CombosService ) {         
    this.idUserGlobal = this.loginService.get_idUsuario();
  }
 
 ngOnInit(): void {
   this.inicializarFormulario_filtro(); 
   this.inicializarFormulario(); 
   this.inicializarFormulario_servicio(); 
   this.mostrarInformacion(); 
   this.getCargarCombos();
 }

 inicializarFormulario(){  
    this.formParams= new FormGroup({
        id_CtaCte: new FormControl('0'), 
        nroRUC_CtaCte: new FormControl(''), 
        tipoPersona_CtaCte: new FormControl('0'), 
        razonSocial_CtaCte: new FormControl(''), 
        direccion_CtaCte: new FormControl(''), 

        telefono1_CtaCte: new FormControl(''), 
        telefono2_CtaCte: new FormControl(''), 
        paginaWeb_CtaCte: new FormControl(''), 
        contacto_CtaCte: new FormControl(''), 
        email_CtaCte: new FormControl(''), 

        colaborador_CtaCte: new FormControl(false), 
        transportista: new FormControl(false), 
        proveedor_CtaCte: new FormControl(false), 
        cliente_CtaCte: new FormControl(false),

        salidaMat_CtaCte: new FormControl(false), 
        devolucionMat_CtaCte: new FormControl(false), 
        transferenciaOrigen_CtaCte: new FormControl(false), 
        transferenciaDestino_CtaCte: new FormControl(false), 

        estado: new FormControl('1'), 
        usuario_creacion: new FormControl(this.idUserGlobal), 
    }) 
 }

 inicializarFormulario_filtro(){ 
    this.formParamsFiltro = new FormGroup({
      estado : new FormControl('1'),   
    }) 
  }

  inicializarFormulario_servicio(){ 
    this.formParamsServicio = new FormGroup({
        id_CtaCte_Servicio : new FormControl('0'), 
        id_CtaCte : new FormControl('0'), 
        id_Area : new FormControl('0'), 
        prioridad_CtaCte_Servicio : new FormControl(false),
        estado : new FormControl('1'),
        usuario_creacion: new FormControl(this.idUserGlobal)
    }) 
  }

  getCargarCombos(){ 
    this.spinner.show();
    combineLatest([  this.combosService.get_servicios()])
    .subscribe( ([ _servicios ])=>{     
    this.spinner.hide();   
    this.servicios = _servicios; 
 
    })
}

 mostrarInformacion(){ 
    this.spinner.show();
    this.cuentaCorrienteService.get_mostrar_informacion( this.formParamsFiltro.value.estado  )
    .subscribe((res:RespuestaServer)=>{           
      if (res.ok==true) {         
          this.cuentasCorrientes  = res.data;
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
    this.idCuentaCorrienteGlobal = 0;

    this.inicializarFormulario_servicio(); 
    this.serviciosCab = [];
    this.existePrioridad=false;

    setTimeout(()=>{ // 
      this.staticTabsPrincipal.tabs[0].active = true; 
      $('#modal_mantenimiento').modal('show');  
    },0); 
 } 

 async saveUpdate(){  

  if (this.formParams.value.nroRUC_CtaCte == '' || this.formParams.value.nroRUC_CtaCte == null) {
    this.alertasService.Swal_alert('error','Por favor ingrese el Ruc');
    return 
  } 
  if (this.formParams.value.tipoPersona_CtaCte == '0' || this.formParams.value.tipoPersona_CtaCte == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione el Tipo de Persona');
    return 
  } 
  if (this.formParams.value.razonSocial_CtaCte == '' || this.formParams.value.razonSocial_CtaCte == null) {
    this.alertasService.Swal_alert('error','Por favor ingrese la Razon Social');
    return 
  } 
  if (this.formParams.value.direccion_CtaCte == '' || this.formParams.value.direccion_CtaCte == null) {
    this.alertasService.Swal_alert('error','Por favor ingrese la Direccion');
    return 
  } 
   
  this.formParams.patchValue({ "usuario_creacion" : this.idUserGlobal });
 
  if ( this.flag_modoEdicion==false) { //// nuevo  

 
     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
     Swal.showLoading();

     const codRuc = await this.cuentaCorrienteService.get_verificar_ruc(this.formParams.value.nroRUC_CtaCte);
     if (codRuc) {
      Swal.close();
      this.alertasService.Swal_alert('error','El nro Ruc ya existe, verifique..');
      return;
     }  

      this.cuentaCorrienteService.set_save_cuentasCorrientes ( {...this.formParams.value, "colaborador_CtaCte" : (this.formParams.value.colaborador_CtaCte == true )? 'SI':'NO', "transportista" : (this.formParams.value.transportista == true )? 'SI':'NO' ,
                                                          "proveedor_CtaCte" : (this.formParams.value.proveedor_CtaCte == true )? 'SI':'NO', "cliente_CtaCte" : (this.formParams.value.cliente_CtaCte == true )? 'SI':'NO' ,  
                                                          "salidaMat_CtaCte" : (this.formParams.value.salidaMat_CtaCte == true )? 'SI':'NO', "devolucionMat_CtaCte" : (this.formParams.value.devolucionMat_CtaCte == true )? 'SI':'NO' , 
                                                          "transferenciaOrigen_CtaCte" : (this.formParams.value.transferenciaOrigen_CtaCte == true )? 'SI':'NO',"transferenciaDestino_CtaCte" : (this.formParams.value.transferenciaDestino_CtaCte == true )? 'SI':'NO'   } )
      .subscribe((res:any)=>{  
        Swal.close();    
        if (res.ok ==true) {     
          this.flag_modoEdicion = true;
          this.idCuentaCorrienteGlobal = res.data;
          this.mostrarInformacion();
          this.alertasService.Swal_Success('Se agrego correctamente..');
        }else{
          this.alertasService.Swal_alert('error', JSON.stringify(res.data));
          alert(JSON.stringify(res.data));
        }
      })
     
   }else{ /// editar

     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Actualizando, espere por favor'  })
     Swal.showLoading();    
    this.cuentaCorrienteService.set_edit_cuentasCorrientes ( {...this.formParams.value, "colaborador_CtaCte" : (this.formParams.value.colaborador_CtaCte == true )? 'SI':'NO', "transportista" : (this.formParams.value.transportista == true )? 'SI':'NO' ,
                                                              "proveedor_CtaCte" : (this.formParams.value.proveedor_CtaCte == true )? 'SI':'NO', "cliente_CtaCte" : (this.formParams.value.cliente_CtaCte == true )? 'SI':'NO' ,  
                                                              "salidaMat_CtaCte" : (this.formParams.value.salidaMat_CtaCte == true )? 'SI':'NO', "devolucionMat_CtaCte" : (this.formParams.value.devolucionMat_CtaCte == true )? 'SI':'NO' , 
                                                              "transferenciaOrigen_CtaCte" : (this.formParams.value.transferenciaOrigen_CtaCte == true )? 'SI':'NO',"transferenciaDestino_CtaCte" : (this.formParams.value.transferenciaDestino_CtaCte == true )? 'SI':'NO'   } , this.formParams.value.id_CtaCte)
      .subscribe((res:any)=>{  
        Swal.close();    
        if (res.ok ==true) {     
          this.flag_modoEdicion = true;
          this.mostrarInformacion();
          this.alertasService.Swal_Success('Se actualizo correctamente..');
        }else{
          this.alertasService.Swal_alert('error', JSON.stringify(res.data));
          alert(JSON.stringify(res.data));
        }
      })
   }
 }  

 editar({  id_CtaCte, nroRUC_CtaCte, tipoPersona_CtaCte, razonSocial_CtaCte, direccion_CtaCte, telefono1_CtaCte, telefono2_CtaCte, paginaWeb_CtaCte, contacto_CtaCte, email_CtaCte, proveedor_CtaCte, 
  cliente_CtaCte, transportista, colaborador_CtaCte, salidaMat_CtaCte, devolucionMat_CtaCte, transferenciaOrigen_CtaCte, transferenciaDestino_CtaCte, estado   }){
   
    this.flag_modoEdicion=true;
    this.idCuentaCorrienteGlobal = id_CtaCte;
 
    this.formParams= new FormGroup({
      id_CtaCte: new FormControl(id_CtaCte), 
      nroRUC_CtaCte: new FormControl(nroRUC_CtaCte), 
      tipoPersona_CtaCte: new FormControl(tipoPersona_CtaCte), 
      razonSocial_CtaCte: new FormControl(razonSocial_CtaCte), 
      direccion_CtaCte: new FormControl(direccion_CtaCte), 

      telefono1_CtaCte: new FormControl(telefono1_CtaCte), 
      telefono2_CtaCte: new FormControl(telefono2_CtaCte), 
      paginaWeb_CtaCte: new FormControl(paginaWeb_CtaCte), 
      contacto_CtaCte: new FormControl(contacto_CtaCte), 
      email_CtaCte: new FormControl(email_CtaCte), 

      colaborador_CtaCte: new FormControl(false), 
      transportista: new FormControl(false), 
      proveedor_CtaCte: new FormControl(false), 
      cliente_CtaCte: new FormControl(false),

      salidaMat_CtaCte: new FormControl(false), 
      devolucionMat_CtaCte: new FormControl(false), 
      transferenciaOrigen_CtaCte: new FormControl(false), 
      transferenciaDestino_CtaCte: new FormControl(false), 

      estado: new FormControl(estado), 
      usuario_creacion: new FormControl(this.idUserGlobal),
    })

   setTimeout(()=>{ // 
    this.formParams.patchValue({ "colaborador_CtaCte" : (colaborador_CtaCte == 'SI' )? true: false, "transportista" : (transportista == 'SI'  )? true: false ,"proveedor_CtaCte" : (proveedor_CtaCte == 'SI'  )? true: false, "cliente_CtaCte" : (cliente_CtaCte == 'SI'  )? true: false  });
    this.formParams.patchValue({ "salidaMat_CtaCte" : (salidaMat_CtaCte == 'SI'  )? true: false, "devolucionMat_CtaCte" : (devolucionMat_CtaCte == 'SI'  )? true: false , "transferenciaOrigen_CtaCte" : (transferenciaOrigen_CtaCte == 'SI'  )? true: false, "transferenciaDestino_CtaCte" : (transferenciaDestino_CtaCte == 'SI'  )? true: false });                               

    this.staticTabsPrincipal.tabs[0].active = true; 
    $('#modal_mantenimiento').modal('show');  
  },0);  

  this.cuentasCorrientes_serviciosDet();
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
     this.cuentaCorrienteService.set_anular_cuentasCorrientes(objBD.id_CtaCte).subscribe((res:RespuestaServer)=>{
         Swal.close();        
         if (res.ok ==true) { 
           
           for (const user of this.cuentasCorrientes ) {
             if (user.id_CtaCte == objBD.id_CtaCte ) {
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

 selectTab(data: TabDirective){   

  const nameTab = data.heading;  

     switch (nameTab) {
       case 'Datos Generales':
        setTimeout(()=>{ // 
 
          // alert('tab 0')
        },0);
       break;
       case 'Datos de Inspeccion':  
         setTimeout(()=>{ // 
 
          // alert('tab 1')
        },0);
       break;
       case 'Anomalias':  
         setTimeout(()=>{ // 
 
          // alert('tab 2')
        },0);
       break;           
       default:
         break;
     }
}

  async save_servicios(){ 
  
    if ( this.idCuentaCorrienteGlobal == 0) {
      this.alertasService.Swal_alert('error','Es necesario primero crear Cuenta Corriente.');
      return;
    }
  
    if (this.formParamsServicio.value.id_Area == '' || this.formParamsServicio.value.id_Area == '0' || this.formParamsServicio.value.id_Area == 0) {
        this.alertasService.Swal_alert('error','Por favor seleccione el Servicio');
        return;
    } 

    this.formParamsServicio.patchValue({ "id_CtaCte" :  this.idCuentaCorrienteGlobal , "usuario_creacion" :  this.idUserGlobal }); 

    const objServices = this.get_verificarServicio(this.formParamsServicio.value.id_Area);
    if (objServices) {
      this.alertasService.Swal_alert('error','El servicio ya se encuentra registrado, verifique..');
      return;
    }
  
      Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
      Swal.showLoading(); 
      this.cuentaCorrienteService.set_save_serviciosCorrientes({...this.formParamsServicio.value, prioridad_CtaCte_Servicio :  (this.formParamsServicio.value.prioridad_CtaCte_Servicio)? 'SI' : 'NO'})
      .subscribe((res:RespuestaServer)=>{ 
        Swal.close();     
        if (res.ok==true) {         
           console.log(res);
           this.cuentasCorrientes_serviciosDet();
        }else{
          Swal.close();    
          alert(JSON.stringify(res.data));
        }
      })   
   } 

   get_verificarServicio(id:number){ 
    const objServicio = this.serviciosCab.find(u=> u.id_Area  == id);
    return objServicio;
  }

  
 cuentasCorrientes_serviciosDet(){ 
  this.spinner.show();
  this.cuentaCorrienteService.get_cuentasCorrientes_servicios( this.idCuentaCorrienteGlobal  )
  .subscribe((res:RespuestaServer)=>{   
    this.spinner.hide();        
    if (res.ok==true) {     

        this.serviciosCab  = res.data;
        this.inicializarFormulario_servicio();

        setTimeout(()=>{ 
          this.existePrioridad = this.checkearPrioridad();
        }, 0);
    }else{
      this.spinner.hide();
      this.alertasService.Swal_alert('error', JSON.stringify(res.data));
      alert(JSON.stringify(res.data));
    }
  })
}
   
 checkearPrioridad() {
    const objServicio = this.serviciosCab.find(u=> String(u.prioridad_CtaCte_Servicio).toUpperCase()  == 'SI');
    return (objServicio) ? true: false;
  }

  eliminarServicio(item:any){
    Swal.fire({
      icon: 'info', allowOutsideClick: false,allowEscapeKey: false,  text: 'Espere por favor'
    })
    Swal.showLoading();
    this.cuentaCorrienteService.set_eliminarCuentasCorrientes_servicios(item.id_CtaCte_Servicio).subscribe((res:RespuestaServer)=>{
      Swal.close();
       if (res.ok) {
        this.alertasService.Swal_Success("Proceso realizado correctamente..")
        var index = this.serviciosCab.indexOf( item );
        this.serviciosCab.splice( index, 1 );

        setTimeout(()=>{ 
          this.existePrioridad = this.checkearPrioridad();
        }, 0);

      }else{
        this.alertasService.Swal_alert('error', JSON.stringify(res.data));
        alert(JSON.stringify(res.data));
      }
    })
  }

}


