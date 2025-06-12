import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class CardetailsService {

  constructor(private http: HttpClient) { }

  private apiUrlGetCar = environment.apiUrl + 'get/car';
  private apiUrlDeleteCar = environment.apiUrl + 'delete/cars';
  private apiUrlUpdateCar = environment.apiUrl + 'update/cars';
  private apiUrlCreateCar = environment.apiUrl + 'create/cars';


  getCar(){
    return this.http.get(this.apiUrlGetCar);
  }

}
