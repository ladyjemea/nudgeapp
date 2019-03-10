import { Time } from "@angular/common";

export interface TripDto {
  duration: Time;
  distance: number;
  durationString: string;
  distanceString: string;
  start: Time;
  stop: Time;
  changeNb: number;
  startCoordinates: Coordinates;
  endCoordinates: Coordinates;
  travelParts: TravelPart[]
}

export interface TravelPart {
  departureName: string;
  arrivalName: string;
  type: TransporationType;
  duration: Time;
}

export enum TransporationType {
  Car,
  Bus,
  Bike,
  Walk
}
