import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Noticiaservice } from 'src/app/Services/noticiaService';

@Component({
  selector: 'app-noticias',
  templateUrl: './noticias.component.html',
  styleUrls: ['./noticias.component.scss']
})
export class NoticiasComponent implements OnInit {

  constructor(private noticiaservice : Noticiaservice,
              private router: Router) { }

  
  noticias!: any; 

  ngOnInit(): void {

   this.Listarnoticias()
  }

  Listarnoticias(){
    this.noticiaservice.ListarNoticia()
    .subscribe(noticias => {
      this.noticias = noticias;
      },
      error =>{
        this.router.navigate(["/login"]);
      }
    )
  }

}
