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
 
import { TabDirective, TabsetComponent } from 'ngx-bootstrap';
import { AlmacenService } from '../../../../services/gestion-almacenes/mantenimientos/almacen.service';
import { ObrasService } from '../../../../services/gestion-almacenes/mantenimientos/obras.service';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
 
declare var $:any;

@Component({
  selector: 'app-obras',
  templateUrl: './obras.component.html',
  styleUrls: ['./obras.component.css']
})
export class ObrasComponent implements OnInit {

  formParamsFiltro : FormGroup;
  formParams: FormGroup;
  formParamsEmpresa : FormGroup;

  idUserGlobal :number = 0;
  flag_modoEdicion :boolean =false;

  obrasCab  :any[]=[]; 
  filtrarMantenimiento = "";

  delegaciones:any[]=[]; 
  proyectos:any[]=[]; 
  proyectosModal:any[]=[]; 

  tiposObras :any[]=[];   
  servicios:any[]=[]; 

  idObra_Global :number = 0;
  existePrioridad=false;
  empresasObraDet  :any[]=[]; 
  datepiekerConfig:Partial<BsDatepickerConfig>
  locales:any[]=[]; 
  tiposObra:any[]=[]; 
  estadosSigetrama:any[]=[]; 
  clientes:any[]=[]; 
  empresas:any[]=[]; 
 
  @ViewChild('staticTabsPrincipal', { static: false }) staticTabsPrincipal: TabsetComponent;
  constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService, private funcionGlobalServices : FuncionesglobalesService, 
    private obrasService : ObrasService, private combosService : CombosService,  private almacenService : AlmacenService ) {         
    this.idUserGlobal = this.loginService.get_idUsuario();
    this.datepiekerConfig = Object.assign({}, { containerClass : 'theme-dark-blue',  dateInputFormat: 'DD/MM/YYYY' })
  }
 
 ngOnInit(): void {
   this.inicializarFormulario_filtro(); 
   this.inicializarFormulario(); 
   this.inicializarFormulario_empresa(); 
   this.getCargarCombos();
 }



 inicializarFormulario_filtro(){ 
    this.formParamsFiltro = new FormGroup({
      delegacion : new FormControl('0'),  
      proyecto : new FormControl('0'),  
      area : new FormControl('0'),  
      tipoObra : new FormControl('0'),  
      estado : new FormControl('1'),   
    }) 
  }


  inicializarFormulario(){  
    this.formParams= new FormGroup({
        id_TD: new FormControl('0'), 
        id_TipoTD: new FormControl('0'), 
        Codigo_TD: new FormControl(''), 

        id_EstaCliente: new FormControl('0'), 
        descripcion_TD: new FormControl(''), 
        id_Area: new FormControl('0'), 
        direccion_TD: new FormControl(''), 

        fechaRecepcion_TD: new FormControl(null), 
        fechaInicio_TD: new FormControl(null), 
        fechaTermino_TD: new FormControl(null), 

        id_Cliente_TD: new FormControl('0'), 
        id_Colaborador_TD: new FormControl('0'), 
        id_Ubigeo: new FormControl('0'), 

        salidaMat_TD: new FormControl(false), 
        devolucionMat_TD: new FormControl(false), 
        transferenciaOrigen_TD: new FormControl(false), 
        transferenciaDestino_TD: new FormControl(false), 

        id_Empresa: new FormControl('0'), 
        id_Delegacion: new FormControl('0'), 
        id_Proyecto: new FormControl('0'), 
        estado: new FormControl('1'), 
        usuario_creacion: new FormControl(this.idUserGlobal), 
    }) 
 }


  inicializarFormulario_empresa(){ 
    this.formParamsEmpresa = new FormGroup({
        id_ObraTD_Empresa : new FormControl('0'), 
        id_TD : new FormControl('0'), 
        id_Colaborador_TD : new FormControl('0'), 
        prioridad_TD : new FormControl(false),
        estado : new FormControl('1'),
        usuario_creacion: new FormControl(this.idUserGlobal)
    }) 
  }

  getCargarCombos(){ 
    this.spinner.show();
    combineLatest([this.combosService.get_delegaciones(this.idUserGlobal), this.combosService.get_tiposObras(),this.combosService.get_servicios(),this.combosService.get_locales(this.idUserGlobal), 
      this.combosService.get_estadosSigetramas(this.idUserGlobal), this.combosService.get_clientes(this.idUserGlobal) , this.combosService.get_empresas() ])
    .subscribe( ([ _delegaciones, _tiposObras,  _servicios,_locales , _estadosSigetrama, _clientes, _empresas])=>{     
      this.spinner.hide();   
      this.delegaciones = _delegaciones; 
      this.tiposObras = _tiposObras; 
      this.servicios = _servicios; 
      this.locales = _locales ;
      this.estadosSigetrama = _estadosSigetrama ;
      this.clientes = _clientes ;
      this.empresas = _empresas;
 
    })
}

 mostrarInformacion(){ 

    if (this.formParamsFiltro.value.delegacion == '0' || this.formParamsFiltro.value.delegacion == 0) {
      this.alertasService.Swal_alert('error','Por favor seleccione la Delegacion');
      return 
    } 
  
    this.spinner.show();
    this.obrasService.get_mostrar_informacion( this.formParamsFiltro.value  )
    .subscribe((res:RespuestaServer)=>{       
      this.spinner.hide();    
      if (res.ok==true) {         
          this.obrasCab  = res.data;
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
    this.idObra_Global = 0;

    this.inicializarFormulario_empresa(); 
    this.proyectosModal = [];
    this.empresasObraDet = [];
    this.existePrioridad=false;

    setTimeout(()=>{ // 
      this.staticTabsPrincipal.tabs[0].active = true; 
      $('#txt_codigo').removeClass('disabledForm');
      $('#modal_mantenimiento').modal('show');  
    },0); 
 } 

 async saveUpdate(){  

  if (this.formParams.value.id_Delegacion == '0' || this.formParams.value.id_Delegacion == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione la Delegacion');
    return 
  } 
  if (this.formParams.value.id_Proyecto == '0' || this.formParams.value.id_Proyecto == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione el Proyecto');
    return 
  } 

  if (this.formParams.value.id_TipoTD == '0' || this.formParams.value.id_TipoTD == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione el Tipo Obra');
    return 
  } 
  if (this.formParams.value.Codigo_TD == '' || this.formParams.value.Codigo_TD == null) {
    this.alertasService.Swal_alert('error','Por favor ingrese el Nro Obra');
    return 
  } 
  if (this.formParams.value.id_EstaCliente == '0' || this.formParams.value.id_EstaCliente == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione el Estado de Sigetrama');
    return 
  }  
  if (this.formParams.value.descripcion_TD == '' || this.formParams.value.descripcion_TD == null) {
    this.alertasService.Swal_alert('error','Por favor ingrese Descripcion de la Obra');
    return 
  } 
  if (this.formParams.value.direccion_TD == '' || this.formParams.value.direccion_TD == null) {
    this.alertasService.Swal_alert('error','Por favor ingrese la Direccion de la Obra');
    return 
  } 
  if (this.formParams.value.id_Area == '0' || this.formParams.value.id_Area == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione el Servicio');
    return 
  } 
  if (this.formParams.value.id_Cliente_TD == '0' || this.formParams.value.id_Cliente_TD == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione el Cliente');
    return 
  } 
    
  if (this.formParams.value.id_Colaborador_TD == '0' || this.formParams.value.id_Colaborador_TD == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione el Local');
    return 
  } 

  const { descripcion : tipoTd} = this.tiposObras.find(item => item.id == this.formParams.value.id_TipoTD )
  let codigoObra = tipoTd + '' + this.formParams.value.Codigo_TD;

  this.formParams.patchValue({ "usuario_creacion" : this.idUserGlobal });
 
  if ( this.flag_modoEdicion==false) { //// nuevo  
 
     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
     Swal.showLoading();


     const codRuc = await this.obrasService.get_verificar_nroObra(codigoObra);
     if (codRuc) {
      Swal.close();
      this.alertasService.Swal_alert('error','El nro Ruc ya existe, verifique..');
      return;
     }   
    this.obrasService.set_save_obras( {...this.formParams.value, "Codigo_TD" : codigoObra ,  "salidaMat_TD" : (this.formParams.value.salidaMat_TD == true )? 'SI':'NO', "devolucionMat_TD" : (this.formParams.value.devolucionMat_TD == true )? 'SI':'NO' , "transferenciaOrigen_TD" : (this.formParams.value.transferenciaOrigen_TD == true )? 'SI':'NO', "transferenciaDestino_TD" : (this.formParams.value.transferenciaDestino_TD == true )? 'SI':'NO'  } )
      .subscribe((res:any)=>{  
        Swal.close();    
        if (res.ok ==true) {     
          this.flag_modoEdicion = true;
          this.idObra_Global = res.data;
          this.mostrarInformacion();
          this.alertasService.Swal_Success('Se agrego correctamente..');

          setTimeout(()=>{
            $('#txt_codigo').addClass('disabledForm');
          })

        }else{
          this.alertasService.Swal_alert('error', JSON.stringify(res.data));
          alert(JSON.stringify(res.data));
        }
      }) 
     
   }else{ /// editar

     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Actualizando, espere por favor'  })
     Swal.showLoading();    
    this.obrasService.set_edit_obras({...this.formParams.value, "Codigo_TD" : codigoObra, "salidaMat_TD" : (this.formParams.value.salidaMat_TD == true )? 'SI':'NO', "devolucionMat_TD" : (this.formParams.value.devolucionMat_TD == true )? 'SI':'NO' , "transferenciaOrigen_TD" : (this.formParams.value.transferenciaOrigen_TD == true )? 'SI':'NO', "transferenciaDestino_TD" : (this.formParams.value.transferenciaDestino_TD == true )? 'SI':'NO'  } , this.formParams.value.id_TD)
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

 editar({ id_TD }){
   
    this.flag_modoEdicion=true;    
    this.idObra_Global = id_TD;

    this.spinner.show();
    this.obrasService.get_obrasId( this.idObra_Global) //----- editar..
    .subscribe((res:RespuestaServer)=>{   
      this.spinner.hide();        
      if (res.ok==true) {     

        const { id_TipoTD,Codigo_TD,id_EstaCliente,descripcion_TD,id_Area, direccion_TD,fechaRecepcion_TD, fechaInicio_TD,fechaTermino_TD, id_Cliente_TD, id_Colaborador_TD, id_Ubigeo, 
                salidaMat_TD, devolucionMat_TD, transferenciaOrigen_TD,transferenciaDestino_TD, estado, id_Empresa, id_Delegacion, id_Proyecto,  }  = res.data[0];

        this.spinner.show();
        this.almacenService.get_proyectosDelegacion(id_Delegacion, this.idUserGlobal) //-----combo dependiente
          .subscribe((res:any)=>{  
            this.spinner.hide();      
            if (res.ok ==true) {    

                   this.proyectosModal = res.data;  
                  
                   setTimeout(()=>{ //    
  
                    this.formParams= new FormGroup({
                      id_TD: new FormControl(id_TD), 
                      id_TipoTD: new FormControl(id_TipoTD), 
                      Codigo_TD: new FormControl(Codigo_TD), 
              
                      id_EstaCliente: new FormControl(id_EstaCliente), 
                      descripcion_TD: new FormControl(descripcion_TD), 
                      id_Area: new FormControl(id_Area), 
                      direccion_TD: new FormControl(direccion_TD), 
              
                      fechaRecepcion_TD: new FormControl( (fechaRecepcion_TD == null) ? null : new Date(fechaRecepcion_TD)), 
                      fechaInicio_TD: new FormControl( (fechaInicio_TD == null) ? null : new Date(fechaInicio_TD)), 
                      fechaTermino_TD: new FormControl( (fechaTermino_TD == null) ? null :  new Date(fechaTermino_TD)), 
              
                      id_Cliente_TD: new FormControl(id_Cliente_TD), 
                      id_Colaborador_TD: new FormControl(id_Colaborador_TD), 
                      id_Ubigeo: new FormControl('0'), 
              
                      salidaMat_TD: new FormControl(false), 
                      devolucionMat_TD: new FormControl(false), 
                      transferenciaOrigen_TD: new FormControl(false), 
                      transferenciaDestino_TD: new FormControl(false), 
              
                      id_Empresa: new FormControl(id_Empresa), 
                      id_Delegacion: new FormControl(id_Delegacion), 
                      id_Proyecto: new FormControl(id_Proyecto), 
                      estado: new FormControl(estado), 
                      usuario_creacion: new FormControl(this.idUserGlobal), 
                    }) 

                   this.formParams.patchValue({ "salidaMat_TD" : (salidaMat_TD == 'SI'  )? true: false, "devolucionMat_TD" : (devolucionMat_TD == 'SI'  )? true: false , "transferenciaOrigen_TD" : (transferenciaOrigen_TD == 'SI'  )? true: false, "transferenciaDestino_TD" : (transferenciaDestino_TD == 'SI'  )? true: false });                               
                   this.staticTabsPrincipal.tabs[0].active = true;   
                   
                   $('#txt_codigo').addClass('disabledForm');
                   
                  $('#modal_mantenimiento').modal('show');  
                },0);  

                this.obras_empresasDet(); // -----
     
            }else{
              this.alertasService.Swal_alert('error', JSON.stringify(res.data));
              alert(JSON.stringify(res.data));
            }
          })
 
      }else{
        this.spinner.hide();
        this.alertasService.Swal_alert('error', JSON.stringify(res.data));
        alert(JSON.stringify(res.data));
      }
    })


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
     this.obrasService.set_anular_obras(objBD.id_TD).subscribe((res:RespuestaServer)=>{
         Swal.close();        
         if (res.ok ==true) { 
           
           for (const user of this.obrasCab ) {
             if (user.id_TD == objBD.id_TD ) {
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

  async save_obraEmpresa(){ 
  
    if ( this.idObra_Global == 0) {
      this.alertasService.Swal_alert('error','Es necesario primero crear la Obra.');
      return;
    }
  
    if (this.formParamsEmpresa.value.id_Colaborador_TD == '' || this.formParamsEmpresa.value.id_Colaborador_TD == '0' || this.formParamsEmpresa.value.id_Colaborador_TD == 0) {
        this.alertasService.Swal_alert('error','Por favor seleccione la empresa');
        return;
    } 

    this.formParamsEmpresa.patchValue({ "id_TD" :  this.idObra_Global , "usuario_creacion" :  this.idUserGlobal }); 

    const objServices = this.get_verificar_AgregoEmpresa(this.formParamsEmpresa.value.id_Colaborador_TD);
    if (objServices) {
      this.alertasService.Swal_alert('error','La empresa ya se encuentra registrado, verifique..');
      return;
    }
  
      Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
      Swal.showLoading(); 
      this.obrasService.set_save_obraEmpresa({...this.formParamsEmpresa.value, prioridad_TD :  (this.formParamsEmpresa.value.prioridad_TD)? 'SI' : 'NO'})
      .subscribe((res:RespuestaServer)=>{ 
        Swal.close();     
        if (res.ok==true) {         
           console.log(res);
           this.obras_empresasDet();
        }else{
          Swal.close();    
          alert(JSON.stringify(res.data));
        }
      })   
   } 

   get_verificar_AgregoEmpresa(id:number){ 
    const objEmpresa = this.empresasObraDet.find(u=> u.id_Colaborador_TD  == id);
    return objEmpresa;
  }

  
 obras_empresasDet(){ 
  this.spinner.show();
  this.obrasService.get_obrasEmpresasDet( this.idObra_Global  )
  .subscribe((res:RespuestaServer)=>{   
    this.spinner.hide();        
    if (res.ok==true) {     

        this.empresasObraDet  = res.data;
        this.inicializarFormulario_empresa();

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
    const objEmpresa = this.empresasObraDet.find(u=> String(u.prioridad_TD).toUpperCase()  == 'SI');
    return (objEmpresa) ? true: false;
  }

  changeDelegacion(opcion:any, esModal:boolean){
    if ( opcion.target.value == 0 || opcion.target.value == '0' ) {

      if (esModal == false){
        this.proyectos = [];
        this.formParamsFiltro.patchValue({"proyecto": '0'});
      }else{
        this.proyectosModal = [];
        this.formParams.patchValue({"id_Proyecto": '0'});
      } 
      return;
    }     
    this.mostrarProyectosDelegacion(opcion.target.value, esModal );
  }
  
  mostrarProyectosDelegacion(idDelegacion:number, esModal:boolean){ 
    this.spinner.show();
    this.almacenService.get_proyectosDelegacion(idDelegacion, this.idUserGlobal)
      .subscribe((res:any)=>{  
        this.spinner.hide();      
        if (res.ok ==true) {        
          if (esModal == false){
            this.proyectos = res.data;
          }else{
            this.proyectosModal = res.data;
          } 
        }else{
          this.alertasService.Swal_alert('error', JSON.stringify(res.data));
          alert(JSON.stringify(res.data));
        }
      })
  }

  eliminarEmpresaObra(item:any){
    Swal.fire({
      icon: 'info', allowOutsideClick: false,allowEscapeKey: false,  text: 'Espere por favor'
    })
    Swal.showLoading();
    this.obrasService.set_eliminarObraEmpresa(item.id_ObraTD_Empresa).subscribe((res:RespuestaServer)=>{
      Swal.close();
       if (res.ok) {
        this.alertasService.Swal_Success("Proceso realizado correctamente..")
        var index = this.empresasObraDet.indexOf( item );
        this.empresasObraDet.splice( index, 1 );

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
