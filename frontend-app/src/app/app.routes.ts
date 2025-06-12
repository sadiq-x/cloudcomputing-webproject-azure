import { Routes } from '@angular/router';
import { CardetailsComponent } from '../app/cardetails/cardetails';

export const routes: Routes = [
     { path: '', redirectTo: 'cars', pathMatch: 'full' },
    { path: 'cars', component: CardetailsComponent, pathMatch: 'full' },
];
