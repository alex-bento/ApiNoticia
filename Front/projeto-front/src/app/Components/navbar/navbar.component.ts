import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AutenticaService } from 'src/app/Services/AutenticaService';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit {

  constructor(private router: Router,
    private autenticaService: AutenticaService) { }

  ngOnInit(): void {
  }

  Sair(){
    this.autenticaService.LimparToken();
    this.router.navigate(["/login"]);
  }

}
