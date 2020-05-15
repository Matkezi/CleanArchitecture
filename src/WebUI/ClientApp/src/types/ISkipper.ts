import { ILanguage } from './ILanguage';
import { ISkill } from "./ISkill";

export interface ISkipper {
    skipperData: {
        id: string;
        oib?: string;
        email?: string;
        newEmail?: string;
        firstName?: string;
        newPassword?: string;
        lastName?: string;
        dateOfBirth?: string;
        address?: string;
        zipCode?: string;
        city?: string;
        country?: string;
        userPhotoUrl?: string;
        skipperInsurancePolicy?: string;
        personalSummary?: string;
        listOfSkills?: ISkill[];
        listOfLanguages?: ILanguage[],
        phoneNumber?: string,
        price?: number
    }
}

export interface ISkipperSkills {
    skills: ISkill[]
}

export interface ISkipperContext extends ISkipper, ISkipperSkills {
    getSkipperById: Function,
    updateSkipper: Function,
    getSkills: Function
}

export interface ISkipperPhoto {
    userPhoto: {
        name: string,
        photoURL?: string,
        data: string
    }
}
