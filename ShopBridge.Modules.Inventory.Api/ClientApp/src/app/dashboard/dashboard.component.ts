import { Component, OnInit } from '@angular/core';
import Swal from 'sweetalert2';
import { HotelService } from '../services/hotel.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
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
  //Method to delete hotel details by calling the delete service
  DeleteHotelInfo(mhid: any): void {
    if (confirm("Are you sure to delete ")) {
      this.hotelService
        .DeleteHotelDetails(mhid)
        .subscribe((Response: any) => {
          // console.log(Response);
          if (Response.responseCode == 200) {
            Swal.fire('Deleted', 'Recods Deleted successfully!', 'warning')
          }
          else {
            Swal.fire('Failed ', 'please Try again !', 'error')
          }
          this.getHotelDetailList();
        });
    }
  }

}
