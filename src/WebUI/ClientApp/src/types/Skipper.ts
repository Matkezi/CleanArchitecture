import { ILanguage } from './ILanguage';
export interface Skipper {
    id: string;
    oib: string;
    email: string;
    newEmail: string;
    firstName: string;
    newPassword: string;
    lastName: string;
    dateOfBirth: string;
    address: string;
    zipCode: string;
    phoneNumber: string;
    city: string;
    country: string;
    userPhoto: string;
    userPhotoUrl: string;
    skipperInsurancePolicy: string;
    personalSummary: string;
    listOfSkills: Skill[];
    listOfLanguages: ILanguage[];
    price: number;
    yearOfFirstLicense: number;
}

export interface Skill {
    id: number;
    name: string,
    skipperId: string,
    icon: string
}
