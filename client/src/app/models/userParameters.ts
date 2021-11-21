import { User } from "./user";

export class UserParameters {
    brandId = 0;
    modelId = 0;
    minEnginePower = 0;
    maxEnginePower = 1500;
    pageNumber = 1;
    pageSize = 20;
    orderBy = "CreateDateDesc";
}