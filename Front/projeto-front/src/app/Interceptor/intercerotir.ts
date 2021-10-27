import { HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Observable } from "rxjs";
import { AutenticaService } from "../Services/AutenticaService";
import { map } from "rxjs/operators";
import { Injectable } from "@angular/core";


@Injectable()
export class Interceptor implements HttpInterceptor
{

    constructor(private autenticaService : AutenticaService)
    {

    }
    
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        
       

        let headers;
        if (req.body instanceof FormData) {
            headers : new HttpHeaders(
                {
                    contentType: "false",
                    processData:"false",
                    Authorization: "Bearer " + this.autenticaService.ObterToken()
                }            
            )
        } else{
            headers = new HttpHeaders()
            .append("accept", "application/json")
            .append("Content-Type", "application/json")
            .append("Authorization", "Bearer " + this.autenticaService.ObterToken());
        }

        let request = req.clone({ headers })

        return next.handle(request).pipe(
            map((event) => {
                return event
            })
        )
    }

}