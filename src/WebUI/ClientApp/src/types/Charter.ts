import { Skipper } from "./Skipper";

export interface Charter {
    charterData: ICharterData
}

export interface ICharterContext extends Charter {
    getCharterById: Function,
    setData: Function
}

export interface ICharterData {
    id: string;
    email: string;
    newEmail: string;
    charterName: string;
    address: string;
    zipCode: string;
    city: string;
    country: string;
    trustedSkippers: Skipper[]
}