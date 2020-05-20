import { Charter } from "./Charter";
import { IBoatType } from "./BoatType";
import { ILicenceType } from "./LicenceType";
import IFileData from "./IFileData";

export interface Boat {
    id: number;
    name: string;
    manufacturer: string;
    model: string;
    type: IBoatType | string;
    length: number;
    minimalRequiredLicense: ILicenceType | string;
    boatPhotoUrl: string;
    boatPhoto: IFileData,
    charter: Charter;
}

export interface IBooatsContext {
    charterBoats: Boat[];
    setCharterBoats: Function;
}
