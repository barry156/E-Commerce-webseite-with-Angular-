import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AuthentificationService {

  private apiUrl1 = 'http://127.0.0.1:7136/api/ui/get/product/{5}'; 
  private apiUrl2 = 'http://127.0.0.1:7136/api/ui/post';// Update this with your actual API URL
  private _userIsLogged : boolean = false;
  private _idOfLoggedUser : number = 0 ;

  constructor(private http: HttpClient ) {}

  register(value : any): Observable<any> {
  
    return this.http.post(`${this.apiUrl2}/register`,value);
  }
  login(value : any): Observable<any> {
  
    return this.http.post(`${this.apiUrl2}/login`,value);
  }
  getProduct()  {
    return this.http.get(`${this.apiUrl1}`);
  }
  get userIsLogged(): boolean {
    return this._userIsLogged;
  }
  
  set userIsLogged(value: boolean) {
    this._userIsLogged = value;
  }
  get idOfLoggedUser(): number {
    return this._idOfLoggedUser ;
  }
  
  set idOfLoggedUser(value: number) {
    this. _idOfLoggedUser = value;
  }
}

  

