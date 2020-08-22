import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AddhotelComponent } from './Hotel/addhotel/addhotel.component';
import { HotelviewComponent } from './Hotel/hotelview/hotelview.component';
import { HotelDetailsComponent } from './Hotel/hotel-details/hotel-details.component';
import { UpdateHotelComponent } from './Hotel/update-hotel/update-hotel.component';
import { AppRoutingModule } from './app-routing.module'; 
import { AngularFireModule } from "@angular/fire";
import { environment } from "../environments/environment";
import { AngularFireStorage } from '@angular/fire/storage';
import {
  AngularFireStorageModule
} from "@angular/fire/storage";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    CounterComponent,
    FetchDataComponent,
    DashboardComponent,
    AddhotelComponent,
    HotelviewComponent,
    HotelDetailsComponent,
   UpdateHotelComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    AngularFireModule,
    AngularFireStorageModule,
    AngularEditorModule,
    FormsModule,
    AngularFireModule,
    AngularFireModule.initializeApp(environment.firebaseConfig, "cloud"),
    RouterModule.forRoot([
      { path: '', component: DashboardComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ]),
    AppRoutingModule
  ],
  providers: [AngularFireStorage],
  bootstrap: [AppComponent]
})
export class AppModule { }
