import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { HotelviewComponent } from './hotelview.component';

describe('HotelviewComponent', () => {
  let component: HotelviewComponent;
  let fixture: ComponentFixture<HotelviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ HotelviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HotelviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
