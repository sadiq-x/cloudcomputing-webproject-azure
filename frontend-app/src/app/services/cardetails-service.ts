import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class CardetailsService {

  constructor(private http: HttpClient) { }

  private apiUrlGetCar = environment.apiUrl + 'get/car';
  private apiUrlDeleteCar = environment.apiUrl + 'delete/car';
  private apiUrlUpdateCar = environment.apiUrl + 'update/car';
  private apiUrlCreateCar = environment.apiUrl + 'create/car';


  getCar(){
    console.log(this.apiUrlGetCar)
    return this.http.get(this.apiUrlGetCar);
  }
  deleteCar(obj: any){
    return this.http.post(this.apiUrlDeleteCar, obj);
  }
  updateCar(obj: any){
    console.log(obj)
    return this.http.post(this.apiUrlUpdateCar, obj);
  }
  createCar(obj: any){
    console.log(this.apiUrlCreateCar)
    return this.http.post(this.apiUrlCreateCar, obj);
  }

}
