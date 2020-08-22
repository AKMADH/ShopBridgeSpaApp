import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HotelService } from './../../services/hotel.service';

@Component({
  selector: 'app-hotel-details',
  templateUrl: './hotel-details.component.html',
  styleUrls: ['./hotel-details.component.css']
})
export class HotelDetailsComponent implements OnInit {
  hotelDetails: any;
  Mhid: any;

  constructor(private route: ActivatedRoute, private HotelService: HotelService) { }

  ngOnInit(): void {
    this.Mhid = this.route.snapshot.params['id'];
    this.getHotelByid();
  }
  getHotelByid(): void {
    this.HotelService
      .getHotelByMhid(this.Mhid)
      .subscribe((Response: any) => {
        //console.log(Response);
        this.hotelDetails = Response.model;
      });
  }
}

