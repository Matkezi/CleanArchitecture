import { ICountry } from './ICountry';
import { ISkill } from "./ISkill";

export default interface SkipperRegistrationContextProps {
    skipperRegistration: Function,
    updateSkipper: Function,

    stepData: SkipperRegistrationStepData | undefined,
    setStepData: Function

    stepNumber: number,
    setStepNumber: Function
    skills: ISkill[],
    countries: ICountry[],
    getSkills: Function,
    setSkills: Function,
    getCountries: Function
}

export interface SkipperRegistrationStepData {
    id?: string,
    firstName: string,
    lastName: string,
    email: string,
    oib: string,
    password: string,
    repeatPassword: string,
    dateOfBirth: string,
    UserPhoto: {
        Name: string,
        photoURL?: string,
        Data: string
    },
    licenceURL: string,
    address: string,
    zipCode: string,
    city: string,
    country: ICountry,
    price: string,
    phoneNumber: string,
    licence:
    {
        dateOfIssue: string,
        licenceData: Blob,
        validUntil: string,
        licenceLevel: string,
        licenceName: string
    },
    yearOfFirsLicence: number,
    skills: [
        {
            id: string,
            name: string
        }
    ],
    language: [
        {
            id: string,
            name: string
        }
    ],
    tos: boolean,
    pp: boolean,
    listOfSkills: [],
    listOfLanguages: []
}

export interface StepNumber {
    stepNumber: number
}

export interface SkipperPreRegistration {
    firstName: String,
    lastName: String,
    email: String
}