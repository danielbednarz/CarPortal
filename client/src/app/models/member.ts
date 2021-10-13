import { Brand } from "./brand";
import { Model } from "./model";
import { Photo } from "./photo";

export interface Member {
    id: number;
    username: string;
    photoUrl: string;
    name: string;
    createDate: Date;
    lastActive: Date;
    brand: Brand;
    brandId: number;
    model: Model;
    modelId: number;
    engineCapacity: string;
    enginePower: number;
    mileage: number;
    productionDate: Date;
    photos: Photo[];
}