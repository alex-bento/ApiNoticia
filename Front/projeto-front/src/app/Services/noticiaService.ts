import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "src/environments/environment";

@Injectable(
    {
        providedIn:'root'
    }
)

export class Noticiaservice 
{
    constructor(private httpCliente: HttpClient){

    }

    private readonly baseURL = environment["endPoint"];

    ListarNoticia(){
        return this.httpCliente.post<any>(`${this.baseURL}/ListarNoticias/`, null);
    }
}