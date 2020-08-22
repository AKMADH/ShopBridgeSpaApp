import { Component, OnInit } from '@angular/core';
import { HotelService } from 'src/app/services/hotel.service';

@Component({
  selector: 'app-hotelview',
  templateUrl: './hotelview.component.html',
  styleUrls: ['./hotelview.component.css']
})
export class HotelviewComponent implements OnInit {
  HotelList: any;

  constructor(private hotelService: HotelService) { }
  ngOnInit() {
    this.getHotelDetailList();
  }
  getHotelDetailList(): void {
    this.hotelService
      .getHotelList()
      .subscribe((hoteldata: any) => {
        //alert(JSON.stringify( hoteldata.model));
        this.HotelList = hoteldata.model;

      });
  }
}
