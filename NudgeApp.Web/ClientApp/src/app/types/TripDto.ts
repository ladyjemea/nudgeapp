import { Time } from "@angular/common";

export interface TripDto {
  mode: TransporationType;
  duration: Time;
  distance: number;
  durationString: string;
  distanceString: string;
  start: Time;
  stop: Time;
  link: string;
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

export interface TravelObject {
  From: NudgeCoordinates;
  To: NudgeCoordinates;
  When: Date;
  Schedule: TripSchedule;
}

export interface NudgeCoordinates {
  Latitude: number;
  Longitude: number;
}

export enum TripSchedule {
  Departure = 1,
  Arival = 2
}

export interface ITripCallback {
  (result: TripDto): void;
}
