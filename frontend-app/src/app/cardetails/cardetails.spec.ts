import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Cardetails } from './cardetails';

describe('Cardetails', () => {
  let component: Cardetails;
  let fixture: ComponentFixture<Cardetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [Cardetails]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Cardetails);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
