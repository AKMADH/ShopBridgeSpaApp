export class Hotel {
  mhid: number;
  name: string;
  description: string;
  hotelImage: string;
  price: number;
}

export class Response {
  responseCode: number;
  messages?: any;
  model: Hotel[];
}

export interface HotelDetails {
  name: string;
  description: string;
  hotelImage: string;
  price: number;
}
