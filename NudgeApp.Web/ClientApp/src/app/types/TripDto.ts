import { Time } from "@angular/common";

export interface TripDto {
  Duration: Time;
  Distance: number;
  DurationString: string;
  DistanceString: string;
  Start: Time;
  Stop: Time;
  ChangeNb: number;
  StartCoordinates: Coordinates;
  EndCoordinates: Coordinates;
  TravelParts: TravelPart[]
}

export interface TravelPart {
  DepartureName: string;
  ArrivalName: string;
  Type: TransporationType;
  Duration: Time;
}

export enum TransporationType {
  Car,
  Bus,
  Bike,
  Walk
}
