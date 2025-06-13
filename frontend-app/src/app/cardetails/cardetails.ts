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
  imports: [CommonModule, ReactiveFormsModule]
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
        this.cars = rst.carGet;
      }
    });
  }

  selectCar(car: Car) {
    this.carForm.patchValue(car);
  }

  createCar() {
    if (this.carForm.value.id) {
      console.log(this.carForm.value)
      this.carService.updateCar(this.carForm.value).subscribe({
        next: (rst: any) => {
          console.log(rst)
          this.loadCars();
          this.resetForm();
        }
      });
    } else {
      console.log(this.carForm.value)
      this.carService.createCar(this.carForm.value).subscribe({
        next: () => {
          this.loadCars();
          this.resetForm();
        }
      });
    }
  }

  getCarList() {
    this.loadCars();
  }

  deleteCar(carId?: number) {
    console.log(carId)
    if (confirm('Tem certeza que deseja apagar este carro?')) {
      this.carService.deleteCar({ id: carId }).subscribe({
        next: () => {
          this.loadCars();
          this.resetForm();
        }
      });
    }
  }

  resetForm() {
    this.carForm.reset();
  }
}