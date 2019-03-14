export interface ForecastDto {
  skyCoverage: SkyCoverageType;
  precipitationProbability: number;
  temperature: number;
  cloudCoveragePercent: number;
  wind: number;
  roadCondition: RoadCondition;
  time: Date;
}

export enum SkyCoverageType {
  Clear,
  PartlyCloudy,
  Cloudy
}

export enum RoadCondition {
  Dry,
  Wet,
  Ice,
  Snow
}

export interface IForecastCallback {
  (result: ForecastDto): void;
}
