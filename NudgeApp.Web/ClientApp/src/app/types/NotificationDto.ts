import { NudgeResult } from "./Nudge";
import { TransportationType } from "./TripDto";
import { SkyCoverageType, WindCondition, RoadCondition, Probabilities } from "./ForecastDto";

export interface NotificationDto {
  id: any,
  text: string,
  createdOn: Date,
  nudgeResult: NudgeResult,
  transportationType: TransportationType,
  tripType: TripType,
  distance: number,
  duration: number,
  reafFeelTemperature: number,
  temperature: number,
  wind: number,
  precipitationProbability: number,
  cloudCoveragePercent: number,
  skyCoverage: SkyCoverageType,
  windCondition: WindCondition,
  probability: Probabilities,
  roadCondition: RoadCondition,
  dateTime: Date
}

export enum TripType {
  Walk,
  WithDestinaion
}
