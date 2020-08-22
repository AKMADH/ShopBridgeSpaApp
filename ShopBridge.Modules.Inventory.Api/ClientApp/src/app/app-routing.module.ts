import { UpdateHotelComponent } from './Hotel/update-hotel/update-hotel.component';
import { HotelviewComponent } from './Hotel/hotelview/hotelview.component';
import { AddhotelComponent } from './Hotel/addhotel/addhotel.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HotelDetailsComponent } from './Hotel/hotel-details/hotel-details.component';


const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'Addhotel', component: AddhotelComponent },
  { path: 'hotel-detail/:id', component: HotelDetailsComponent },
 { path: 'hotelview', component: HotelviewComponent },
  { path: 'update/:id', component: UpdateHotelComponent }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
