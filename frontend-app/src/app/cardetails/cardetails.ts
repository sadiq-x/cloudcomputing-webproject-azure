import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Car } from '../models/carmodel';
import { CardetailsService } from '../services/cardetails-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cardetails',
  templateUrl: './cardetails.html',
  styleUrl: './cardetails.css',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule] // 🔥 Import necessário aqui
})
export class CardetailsComponent implements OnInit {

  cars: Car[] = [];
  carForm: FormGroup;

  constructor(
    private carService: CardetailsService,
    private fb: FormBuilder
  ) {
    this.carForm = this.fb.group({
      id: [null],
      mark: ['', Validators.required],
      model: ['', Validators.required],
      color: ['', Validators.required],
      km: ['', Validators.required],
      price: ['', Validators.required],
      year: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.loadCars();
  }

  loadCars() {
    this.carService.getCar().subscribe({
      next: (rst: any) => {
        console.log(rst);
        this.cars = rst.carGet;
      }
    });
  }

  selectCar(car: Car) {
    this.carForm.patchValue(car);
  }

  onSubmit() {
    if (this.carForm.value.id) {
      
    } else {
      
    }
  }

  deleteCar() {
    
  }
}