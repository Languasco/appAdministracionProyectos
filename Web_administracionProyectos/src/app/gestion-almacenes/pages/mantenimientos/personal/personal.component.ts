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
 
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { PersonalService } from '../../../../services/gestion-almacenes/mantenimientos/personal.service';
import { InputFileI } from '../../../../models/inputFile.models';
import { UploadService } from '../../../../services/upload/upload.service';
 
declare var $:any;

@Component({
  selector: 'app-personal',
  templateUrl: './personal.component.html',
  styleUrls: ['./personal.component.css']
})
export class PersonalComponent implements OnInit {

  formParamsFiltro : FormGroup;
  formParams: FormGroup;
  formParamsEmpresa : FormGroup;

  idUserGlobal :number = 0;
  flag_modoEdicion :boolean =false;

  personalCab  :any[]=[]; 
  filtrarMantenimiento = "";
  idPersonal_Global :number = 0;

  delegaciones:any[]=[]; 
  proyectos:any[]=[]; 
  proyectosModal:any[]=[];  
  cargos:any[]=[]; 

  existePrioridad=false;
  empresasObraDet  :any[]=[]; 
  datepiekerConfig:Partial<BsDatepickerConfig>
  empresas:any[]=[]; 
  imgProducto= './assets/img/sinFoto.jpg';
  filePhoto:InputFileI[] = [];

  @ViewChild('staticTabsPrincipal', { static: false }) staticTabsPrincipal: TabsetComponent;
  constructor(private alertasService : AlertasService, private spinner: NgxSpinnerService, private loginService: LoginService, private funcionGlobalServices : FuncionesglobalesService, 
    private personalService : PersonalService, private combosService : CombosService,  private almacenService : AlmacenService, private uploadService : UploadService ) {         
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
      personal : new FormControl(''),  
      estado : new FormControl('1'),   
    }) 
  }


  inicializarFormulario(){  
    this.formParams= new FormGroup({
        id_Personal: new FormControl('0'), 
        nroDoc_Personal: new FormControl(''), 
        tipoDoc_Personal: new FormControl('0'), 
        apellidos_Personal: new FormControl(''), 

        nombres_Personal: new FormControl(''), 
        direccion_Personal: new FormControl(''), 
        telefono_Personal: new FormControl(''), 
        costoMo_Personal: new FormControl(''), 
        fechaIngreso_Personal: new FormControl(null), 

        id_Cargo: new FormControl('0'), 
        tipoPersonal: new FormControl('0'), 
        fechaCese_Personal: new FormControl(null), 

        retiraMate_Personal: new FormControl(false), 
        retiraEquipamiento_Personal: new FormControl(false),  
 
        id_Delegacion: new FormControl('0'), 
        id_Proyecto: new FormControl('0'),  
        estado: new FormControl('1'), 
        usuario_creacion: new FormControl(this.idUserGlobal), 
    }) 
 }


  inicializarFormulario_empresa(){ 
    this.formParamsEmpresa = new FormGroup({
        id_ObraTD_Empresa : new FormControl('0'), 
        id_Personal : new FormControl('0'), 
        id_Colaborador_TD : new FormControl('0'), 
        prioridad_TD : new FormControl(false),
        estado : new FormControl('1'),
        usuario_creacion: new FormControl(this.idUserGlobal)
    }) 
  }

  getCargarCombos(){ 
    this.spinner.show();
    combineLatest([this.combosService.get_delegaciones(this.idUserGlobal) , this.combosService.get_empresas(), this.combosService.get_cargos() ])
    .subscribe( ([ _delegaciones,_empresas, _cargos])=>{     
      this.spinner.hide();   
      this.delegaciones = _delegaciones;  
      this.empresas = _empresas; 
      this.cargos = _cargos;
    })
}

 mostrarInformacion(){ 

    if (this.formParamsFiltro.value.delegacion == '0' || this.formParamsFiltro.value.delegacion == 0) {
      this.alertasService.Swal_alert('error','Por favor seleccione la Delegacion');
      return 
    } 
  
    this.spinner.show();
    this.personalService.get_mostrar_informacion( this.formParamsFiltro.value , this.idUserGlobal )
    .subscribe((res:RespuestaServer)=>{       
      this.spinner.hide();    
      if (res.ok==true) {         
          this.personalCab  = res.data;
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
    this.idPersonal_Global = 0;

    this.inicializarFormulario_empresa(); 
    this.proyectosModal = [];
    this.empresasObraDet = [];
    this.existePrioridad=false;

    setTimeout(()=>{ // 
      this.staticTabsPrincipal.tabs[0].active = true; 
      $('#txt_codigo').removeClass('disabledForm');
      this.imgProducto= './assets/img/sinFoto.jpg';
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

  if (this.formParams.value.tipoDoc_Personal == '0' || this.formParams.value.tipoDoc_Personal == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione el Tipo Documento');
    return 
  } 
  if (this.formParams.value.nroDoc_Personal == '' || this.formParams.value.nroDoc_Personal == null) {
    this.alertasService.Swal_alert('error','Por favor ingrese el numero de Documento');
    return 
  } 
  if (this.formParams.value.apellidos_Personal == '' || this.formParams.value.apellidos_Personal == null) {
    this.alertasService.Swal_alert('error','Por favor ingrese los Apellidos');
    return 
  }   if (this.formParams.value.nombres_Personal == '' || this.formParams.value.nombres_Personal == null) {
    this.alertasService.Swal_alert('error','Por favor ingrese los Nombres');
    return 
  } 
  if (this.formParams.value.tipoPersonal == '0' || this.formParams.value.tipoPersonal == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione el Tipo Personal');
    return 
  } 
  if (this.formParams.value.id_Cargo == '0' || this.formParams.value.id_Cargo == 0) {
    this.alertasService.Swal_alert('error','Por favor seleccione el Cargo');
    return 
  } 
 
  this.formParams.patchValue({ "usuario_creacion" : this.idUserGlobal });
 
  if ( this.flag_modoEdicion==false) { //// nuevo  
 
     Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
     Swal.showLoading();
 
    const objPersonal = await this.personalService.get_verificar_nroDoc(this.formParams.value.nroDoc_Personal );
    if (objPersonal) {
      Swal.close();   
      this.alertasService.Swal_alert('error','El nro Doc ya existe, verifique..');
      return;
    }

    this.personalService.set_save_personal( {...this.formParams.value, "retiraMate_Personal" : (this.formParams.value.retiraMate_Personal == true )? 'SI':'NO', "retiraEquipamiento_Personal" : (this.formParams.value.retiraEquipamiento_Personal == true )? 'SI':'NO' } )
      .subscribe((res:any)=>{  
        Swal.close();    
        if (res.ok ==true) {     
          this.flag_modoEdicion = true;
          this.idPersonal_Global = res.data;
          this.mostrarInformacion();
          this.alertasService.Swal_Success('Se agrego correctamente..');

          this.upload_imagePerson(this.idPersonal_Global);

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
    this.personalService.set_edit_personal( {...this.formParams.value, "retiraMate_Personal" : (this.formParams.value.retiraMate_Personal == true )? 'SI':'NO', "retiraEquipamiento_Personal" : (this.formParams.value.retiraEquipamiento_Personal == true )? 'SI':'NO' } , this.formParams.value.id_Personal)
      .subscribe((res:any)=>{  
        Swal.close();    
        if (res.ok ==true) {     
          this.flag_modoEdicion = true;
          this.mostrarInformacion();
          this.alertasService.Swal_Success('Se actualizo correctamente..');

          this.upload_imagePerson(this.idPersonal_Global);

        }else{
          this.alertasService.Swal_alert('error', JSON.stringify(res.data));
          alert(JSON.stringify(res.data));
        }
      })
   }
 }  

 editar({ id_Personal }){
   
  this.flag_modoEdicion=true;    
  this.idPersonal_Global = id_Personal;

  this.spinner.show();
  this.personalService.get_personalId( this.idPersonal_Global) //----- editar..
  .subscribe((res:RespuestaServer)=>{   
    this.spinner.hide();        
    if (res.ok==true) {     

      const { id_Personal, nroDoc_Personal, tipoDoc_Personal, apellidos_Personal, nombres_Personal, direccion_Personal, telefono_Personal, costoMo_Personal, fechaIngreso_Personal, id_Cargo, tipoPersonal,
              fechaCese_Personal, retiraMate_Personal, retiraEquipamiento_Personal, estado,fotoBase64, id_Delegacion, id_Proyecto }  = res.data[0];

      this.spinner.show();
      this.almacenService.get_proyectosDelegacion(id_Delegacion, this.idUserGlobal) //-----combo dependiente
        .subscribe((res:any)=>{  
          this.spinner.hide();      
          if (res.ok ==true) {    

                 this.proyectosModal = res.data;  
                
                 setTimeout(()=>{ //   
                   this.formParams= new FormGroup({
                      id_Personal: new FormControl(id_Personal), 
                      nroDoc_Personal: new FormControl(nroDoc_Personal), 
                      tipoDoc_Personal: new FormControl(tipoDoc_Personal), 
                      apellidos_Personal: new FormControl(apellidos_Personal), 
              
                      nombres_Personal: new FormControl(nombres_Personal), 
                      direccion_Personal: new FormControl(direccion_Personal), 
                      telefono_Personal: new FormControl(telefono_Personal), 
                      costoMo_Personal: new FormControl(costoMo_Personal), 
                      fechaIngreso_Personal: new FormControl( (fechaIngreso_Personal == null) ? null : new Date(fechaIngreso_Personal)), 
              
                      id_Cargo: new FormControl(id_Cargo), 
                      tipoPersonal: new FormControl(tipoPersonal), 
                      fechaCese_Personal: new FormControl( (fechaCese_Personal == null) ? null : new Date(fechaCese_Personal)), 
              
                      retiraMate_Personal: new FormControl(false), 
                      retiraEquipamiento_Personal: new FormControl(false),  
               
                      id_Delegacion: new FormControl(id_Delegacion), 
                      id_Proyecto: new FormControl(id_Proyecto),  
                      estado: new FormControl(estado), 
                      usuario_creacion: new FormControl(this.idUserGlobal), 
                  }) 
                   this.formParams.patchValue({ "retiraMate_Personal" : (retiraMate_Personal == 'SI'  )? true: false, "retiraEquipamiento_Personal" : (retiraEquipamiento_Personal == 'SI'  )? true: false });                               
                   this.staticTabsPrincipal.tabs[0].active = true;   
                   this.imgProducto = (!fotoBase64)? './assets/img/sinFoto.jpg' : fotoBase64;
                 
                 $('#txt_codigo').addClass('disabledForm');
                 
                $('#modal_mantenimiento').modal('show');  
              },0);  

              // this.obras_empresasDet(); // -----
   
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
     this.personalService.set_anular_personal(objBD.id_Personal).subscribe((res:RespuestaServer)=>{
         Swal.close();        
         if (res.ok ==true) { 
           
           for (const user of this.personalCab ) {
             if (user.id_Personal == objBD.id_Personal ) {
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
  
    if ( this.idPersonal_Global == 0) {
      this.alertasService.Swal_alert('error','Es necesario primero crear la Obra.');
      return;
    }
  
    if (this.formParamsEmpresa.value.id_Colaborador_TD == '' || this.formParamsEmpresa.value.id_Colaborador_TD == '0' || this.formParamsEmpresa.value.id_Colaborador_TD == 0) {
        this.alertasService.Swal_alert('error','Por favor seleccione la empresa');
        return;
    } 

    this.formParamsEmpresa.patchValue({ "id_Personal" :  this.idPersonal_Global , "usuario_creacion" :  this.idUserGlobal }); 

    const objServices = this.get_verificar_AgregoEmpresa(this.formParamsEmpresa.value.id_Colaborador_TD);
    if (objServices) {
      this.alertasService.Swal_alert('error','La empresa ya se encuentra registrado, verifique..');
      return;
    }
  
      Swal.fire({  icon: 'info', allowOutsideClick: false, allowEscapeKey: false, text: 'Espere por favor'  })
      Swal.showLoading(); 
      this.personalService.set_save_obraEmpresa({...this.formParamsEmpresa.value, prioridad_TD :  (this.formParamsEmpresa.value.prioridad_TD)? 'SI' : 'NO'})
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
  this.personalService.get_obrasEmpresasDet( this.idPersonal_Global  )
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
    this.personalService.set_eliminarObraEmpresa(item.id_ObraTD_Empresa).subscribe((res:RespuestaServer)=>{
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

  keyPress(event: any) {
    this.funcionGlobalServices.verificar_soloNumeros(event)  ;
  }

  keyPress2(event: any) {
    this.funcionGlobalServices.verificar_soloNumeros_sinPunto(event)  ;
  }

  openFile(){
    $('#inputFileOpen').click();
   }

   onFileChange(event:any) {   
    var filesTemporal = event.target.files; //FileList object   
    var fileE:InputFileI [] = []; 

    for (var i = 0; i < event.target.files.length; i++) { //for multiple files          
      fileE.push({
          'file': filesTemporal[i],
          'namefile': filesTemporal[i].name,
          'status': '',
          'message': ''
      })  
    }
     this.filePhoto = fileE;
 
     for (var i = 0; i < filesTemporal.length; i++) { //for multiple files          
         ((file)=> {
             var name = file;
             var reader = new FileReader();
             reader.onload = (e)=> {
                 let urlImage:any = e.target;
                 this.imgProducto = String(urlImage['result']);      
             }
             reader.readAsDataURL(file);
         })(filesTemporal[i]);
     }
 }

  
 upload_imagePerson(idPersonal:number) {
  if ( this.filePhoto.length ==0){
    return;
  }
  this.spinner.show();
  this.uploadService.upload_imagen_personal( this.filePhoto[0].file , idPersonal, this.idUserGlobal ).subscribe(
    (res:RespuestaServer) =>{
      this.spinner.hide();
      if (res.ok==true) {

      }else{
        this.alertasService.Swal_alert('error',JSON.stringify(res.data));
      }
      },(err) => {
        this.spinner.hide();
      this.alertasService.Swal_alert('error',JSON.stringify(err)); 
      }
  )
 }
 

}
