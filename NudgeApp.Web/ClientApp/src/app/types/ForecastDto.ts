export interface ForecastDto {
  time: Date;
  temperature: number;
  realFeelTeamperature: number;
  ceiling: number;
  rain: number;
  rainProbability: number;
  snow: number;
  snowProbability: number;
  ice: number;
  iceProbability: number;
  visibility: number;
  wind: number;
  daylight: boolean;
  roadCondition: RoadCondition;
  skyCoverage: SkyCoverageType;
  precipitationProbability: number;
  cloudCoveragePercent: number;
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
