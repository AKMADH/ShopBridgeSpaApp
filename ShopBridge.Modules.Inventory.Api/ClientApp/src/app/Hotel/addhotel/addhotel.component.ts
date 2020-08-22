import { HotelService } from './../../services/hotel.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import Swal from 'sweetalert2';
import { AngularFireStorage } from '@angular/fire/storage';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-addhotel',
  templateUrl: './addhotel.component.html',
  styleUrls: ['./addhotel.component.css']
})
export class AddhotelComponent implements OnInit {

  imgURL: any;
  IsShow: boolean = false;
  IsImgurl: boolean = true;
  public message: string;
  dataSaved: boolean;
  massage: string;
  hotelDetailsForm: FormGroup;
  hotelDetails: FormGroup;
  imagePath: any;
  IsSuccessmsg: boolean = true;
  HotelList: any;
  submitted: boolean = false;
  downloadURL: any;
  HotelImageURL: FormBuilder;
  constructor(private HotelService: HotelService, private fb: FormBuilder, private storage: AngularFireStorage) {

  }
  ngOnInit(): void {

    this.hotelDetailsForm = this.fb.group({
      name: new FormControl("", [Validators.required]),
      description: new FormControl("", [Validators.required]),
      hotelImage: new FormControl("", [Validators.required]),
      price: new FormControl("", [Validators.required]),

    })
    this.getHotelDetailList();
  }
  get f() { return this.hotelDetailsForm.controls; }
  getHotelDetailList(): void {
    this.HotelService
      .getHotelList()
      .subscribe((hoteldata: any) => {
        //alert(JSON.stringify( hoteldata.model));
        this.HotelList = hoteldata.model;

      });
  }
  getHotelByid(Mhid: any): void {
    this.HotelService
      .getHotelByMhid(Mhid)
      .subscribe((Response: any) => {
        console.log(Response);
        this.hotelDetails = Response.model;
      });
  }


  //selected image and uploaded to the firebase storage
  onFileSelected(event) {
    var n = Date.now();
    const file = event.target.files[0];
    const filePath = `Hotel/${n}`;
    const fileRef = this.storage.ref(filePath);
    const task = this.storage.upload(`Hotel/${n}`, file);
    task
      .snapshotChanges()
      .pipe(
        finalize(() => {
          this.downloadURL = fileRef.getDownloadURL();
          this.downloadURL.subscribe(url => {
            if (url) {
              this.fb = url;
            }
            this.IsShow = true;
            this.IsImgurl = false;

            this.HotelImageURL = this.fb;
            //alert(this.HotelImageURL);
            this.hotelDetailsForm.get("hotelImage").patchValue(this.HotelImageURL);
            
            alert(this.hotelDetailsForm.get("hotelImage").value);
          });
        })
      )
      .subscribe(url => {
        if (url) {
          url;
          //alert(this.imgURL);
        }
      });
  }
  onSubmit() {
    this.submitted = true;
    // stop here if form is invalid
    if (this.hotelDetailsForm.invalid) {
      return;
    }
    alert(JSON.stringify(this.hotelDetailsForm.value));
    this.InsertHotelDetails();
  }
  //Method to calling insert hotel detail service
  InsertHotelDetails(): void {

    this.HotelService.AddHotelDeTails(this.hotelDetailsForm.value)
      .subscribe((Response: any) => {
        if (Response.responseCode == 200) {

          this.dataSaved = true;
          this.alertWithSuccess();
          this.hotelDetailsForm.reset();
          this.IsShow = false;
          this.IsImgurl = true;
          this.getHotelDetailList();
        }

        else if (Response.responseCode == 600) {
          Swal.fire('Warning', 'Hotel name Already exists!', 'warning')
        }
        else {
          Swal.fire('Failed ', 'please Try again !', 'error')
        }
      }
      );

  }

  alertWithSuccess() {
    Swal.fire('Thank you...', 'Records Saved succesfully!', 'success')
  }

}
