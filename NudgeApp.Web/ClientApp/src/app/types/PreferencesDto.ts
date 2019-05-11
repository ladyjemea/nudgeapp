import { TransportationType } from "./TripDto";

export interface PreferencesDto {
  transportationType: TransportationType,
  minTemperature: number,
  maxTemperature: number,
  rainyTrip: boolean,
  snowyTrip: boolean
}
