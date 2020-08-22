import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { HotelService } from 'src/app/services/hotel.service';
import Swal from 'sweetalert2';
import { AngularFireStorage } from '@angular/fire/storage';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-update-hotel',
  templateUrl: './update-hotel.component.html',
  styleUrls: ['./update-hotel.component.css']
})
export class UpdateHotelComponent implements OnInit {
  dataSaved: boolean;
  hotelDetailsForm: any;
  submitted: boolean;
  imgURL: string | ArrayBuffer;
  hotelDetails: any;
  message: string;
  HotelList: any;
  imagePath: any;
  IsShow: boolean;
  IsImgurl: boolean;
  massage: string;
  Mhid: any;
  EventValue: string;
  HotelImageURL: FormBuilder;
  downloadURL: any;

  constructor(private HotelService: HotelService, private fb: FormBuilder,
    private route: ActivatedRoute, private storage: AngularFireStorage) {
  }
  ngOnInit(): void {
    this.Mhid = this.route.snapshot.params['id'];
    this.getHotelByid();
    this.hotelDetailsForm = this.fb.group({
      name: new FormControl(""),
      hotelImage: new FormControl("", [Validators.required, Validators.minLength(4)]),
      description: new FormControl("", [Validators.required]),
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
  getHotelByid(): void {
    this.HotelService
      .getHotelByMhid(this.Mhid)
      .subscribe((Response: any) => {
        console.log(Response);
        this.hotelDetails = Response.model;
        this.HotelImageURL = Response.model.hotelImage;
        this.hotelDetailsForm.patchValue(
          {
            name: Response.model.name,
            description: Response.model.description,
            price: Response.model.price,
            hotelImage: Response.model.hotelImage
          });

        //alert(Response.model.hotelImage);
      });
  }


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

    //alert(JSON.stringify(this.hotelDetailsForm.value));
    this.UpdateHotelDetails();
  }
  UpdateHotelDetails(): void {

    this.HotelService.UpdateHotelDeTails(this.Mhid, this.hotelDetailsForm.value).subscribe(
      () => {

        this.dataSaved = true;
        this.alertWithSuccess();

      }
    );

  }

  alertWithSuccess() {
    Swal.fire('Updated', 'Records Updated succesfully!', 'success')
  }
}
