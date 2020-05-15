import { Charter } from "./Charter";

export interface Boat
    {
        id: number;
        name: string;
        manufacturer: string;
        model: string;
        type: string;
        length: number;
        minimalRequiredLicence: string;
        boathPhotoUrl: string;
        charter: Charter;
    }

export interface IBooatsContext {
    charterBoats: Boat[];
    setCharterBoats: Function;
    }
