"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var SkyCoverageType;
(function (SkyCoverageType) {
    SkyCoverageType[SkyCoverageType["Clear"] = 0] = "Clear";
    SkyCoverageType[SkyCoverageType["PartlyCloudy"] = 1] = "PartlyCloudy";
    SkyCoverageType[SkyCoverageType["Cloudy"] = 2] = "Cloudy";
})(SkyCoverageType = exports.SkyCoverageType || (exports.SkyCoverageType = {}));
var RoadCondition;
(function (RoadCondition) {
    RoadCondition[RoadCondition["Dry"] = 0] = "Dry";
    RoadCondition[RoadCondition["Wet"] = 1] = "Wet";
    RoadCondition[RoadCondition["Ice"] = 2] = "Ice";
    RoadCondition[RoadCondition["Snow"] = 3] = "Snow";
})(RoadCondition = exports.RoadCondition || (exports.RoadCondition = {}));
var WindCondition;
(function (WindCondition) {
    WindCondition[WindCondition["StrongWinds"] = 0] = "StrongWinds";
    WindCondition[WindCondition["LightWinds"] = 1] = "LightWinds";
    WindCondition[WindCondition["Calm"] = 2] = "Calm";
})(WindCondition = exports.WindCondition || (exports.WindCondition = {}));
var Probabilities;
(function (Probabilities) {
    Probabilities[Probabilities["Rain"] = 0] = "Rain";
    Probabilities[Probabilities["Snow"] = 1] = "Snow";
    Probabilities[Probabilities["Ice"] = 2] = "Ice";
    Probabilities[Probabilities["Gust"] = 3] = "Gust";
    Probabilities[Probabilities["Normal"] = 4] = "Normal";
    Probabilities[Probabilities["Slippery"] = 5] = "Slippery";
    Probabilities[Probabilities["NotEvaluated"] = 6] = "NotEvaluated";
})(Probabilities = exports.Probabilities || (exports.Probabilities = {}));
//# sourceMappingURL=ForecastDto.js.map