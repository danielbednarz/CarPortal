import { User } from "./user";

export interface FuelReport {
    id: string;
    cost: number;
    fuelAmount: number;
    traveledDistance: number;
    userId: number;
    user: User;
}