export interface ForecastDto {
  dateTime: Date;
  roadCondition: RoadCondition;
  skyCoverage: SkyCoverageType;
  Temperature: number;
  RealFeelTemperature: number;
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
