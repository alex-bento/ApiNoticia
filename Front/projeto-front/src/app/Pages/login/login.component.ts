import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginModel } from 'src/app/model/LoginMode';
import { AutenticaService } from 'src/app/Services/AutenticaService';
import { LoginService } from 'src/app/Services/loginService';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm!: FormGroup;

  constructor(private FormBuilder : FormBuilder, 
    private router: Router, 
    public loginService : LoginService,
    private autenticaService : AutenticaService ) { }

  ngOnInit(): void {
    this.loginForm = this.FormBuilder.group(
      {
        email: ['',[Validators.required, Validators.email]],
        senha: ['',[Validators.required]]
      }
    );
  }
  submitLogin()
  {
    var dadosLogin = this.loginForm.getRawValue() as LoginModel;

    this.loginService.LoginUsuario(dadosLogin).subscribe(
      token => {
        this.autenticaService.DefinirToken(token);

        this.router.navigate(["/noticias"]);
      },
      erro => {

      }
    )

    
  }
}
