import { Charter } from "./Charter";
import { IBoatType } from "./BoatType";
import { ILicenceType } from "./LicenceType";

export interface Boat {
    id: number;
    name: string;
    manufacturer: string;
    model: string;
    type: IBoatType | string;
    length: number;
    minimalRequiredLicence: ILicenceType | string;
    boathPhotoUrl: string;
    charter: Charter;
}

export interface IBooatsContext {
    charterBoats: Boat[];
    setCharterBoats: Function;
}
