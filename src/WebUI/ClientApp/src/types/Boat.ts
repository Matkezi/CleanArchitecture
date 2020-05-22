import { Charter } from "./Charter";
import { IBoatType } from "./BoatType";
import { ILicenceType } from "./LicenceType";
import IFileData from "./IFileData";

export interface Boat {
    id: number;
    name: string;
    manufacturer: string;
    model: string;
    type: IBoatType | string | number;
    length: number;
    minimalRequiredLicense: ILicenceType | string | number;
    boatPhotoUrl: string;
    boatPhoto: IFileData,
    charter: Charter;
}

export interface IBooatsContext {
    charterBoats: Boat[];
    showForm: boolean;
    setShowForm: Function;
    setCharterBoats: Function;
    deleteBoat: Function;
    saveBoat: Function;
    updateBoat: Function;
}
