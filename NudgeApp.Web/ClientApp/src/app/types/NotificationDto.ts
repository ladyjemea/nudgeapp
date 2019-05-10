import { NudgeResult } from "./Nudge";
import { TransportationType } from "./TripDto";
import { SkyCoverageType, WindCondition, RoadCondition, Probabilities } from "./ForecastDto";

export interface NotificationDto {
  Text: string,
  NudgeResult: NudgeResult,
  TransportationType: TransportationType,
  Type: TripType,
  Distance: number,
  Duration: number,
  ReafFeelTemperature: number,
  Temperature: number,
  Wind: number,
  PrecipitationProbability: number,
  CloudCoveragePercent: number,
  SkyCoverage: SkyCoverageType,
  WindCondition: WindCondition,
  Probability: Probabilities,
  RoadCondition: RoadCondition,
  DateTime: Date
}

export enum TripType {
  Walk,
  WithDestinaion
}
