import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
 
import { LoginService } from '../../services/login/login.service';
import { Router } from '@angular/router';

declare var $:any;

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  showMenu:boolean=false;
  formParams : FormGroup;
  menuPrincipal :any[]= [];
  nameUser = "";
  nombreModulo= "";

  constructor( private loginService : LoginService, private router:Router) {         
    this.loginService.isLogginUser$.subscribe(obj => {
      this.showMenu = obj.status;
      this.nameUser = this.loginService.getSessionNombre();
    }); 
  }

  ngOnInit(): void {     
    this.showMenu = this.loginService.getSession();
    this.nameUser = this.loginService.getSessionNombre();    
    const idModulo = this.loginService.getIdModulo(); 

    if (idModulo == 2 ) {
      this.nombreModulo= "GESTION ALMACENES"
    }
    if (idModulo == 3 ) {
      this.nombreModulo= "GESTION PROYECTOS"
    }

    this.menuModulo(idModulo);
  }
 
  cerrarSesion(){
    this.loginService.logOut();
    this.showMenu=false;
    this.menuPrincipal = [];
    this.loginService.updateLoginNameStatus(false,null);
    this.router.navigateByUrl('/autentificacion');    
  }  

  downloadApk(){
    window.open('./assets/apk/applus.apk');    
  }
  
  menuModulo(idmodulo :number){
    const menu =  this.loginService.getMenu_Modulos(idmodulo);
    if (menu) {
      this.menuPrincipal  = menu[0]; 
    }
  }
  
  regresarHome(){
    this.menuPrincipal = [];
    this.router.navigateByUrl('/home');
  }

}