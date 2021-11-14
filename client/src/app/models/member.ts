import { Brand } from "./brand";
import { Engine } from "./engine";
import { Model } from "./model";
import { Note } from "./note";
import { Photo } from "./photo";

export interface Member {
    id: number;
    username: string;
    photoUrl: string;
    createDate: Date;
    lastActive: Date;
    brandId: number;
    brand: Brand;
    modelId: number;
    model: Model;
    engineId: number;
    engine: Engine;
    enginePower: number;
    mileage: number;
    productionDate: Date;
    photos: Photo[];
    notes: Note[];
}