import { Engine } from "./engine";
import { Model } from "./model";

export interface EnginesForModel {
    id: number;
    modelId: number;
    model: Model;
    engineId: number;
    engine: Engine;
}