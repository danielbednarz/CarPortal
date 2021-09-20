import { Photo } from "./photo";

export interface Member {
    id: number;
    username: string;
    photoUrl: string;
    name: string;
    createDate: Date;
    lastActive: Date;
    brand: string;
    model: string;
    engineCapacity: string;
    enginePower: number;
    mileage: number;
    productionDate: Date;
    photos: Photo[];
}