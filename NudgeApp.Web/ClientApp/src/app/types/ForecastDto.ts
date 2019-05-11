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

export enum WindCondition {
  StrongWinds,
  LightWinds,
  Calm,
}

export enum Probabilities {
  Rain,
  Snow,
  Ice,
  Gust,
  Normal,
  Slippery,
  NotEvaluated
}

export interface IForecastCallback {
  (result: ForecastDto): void;
}
