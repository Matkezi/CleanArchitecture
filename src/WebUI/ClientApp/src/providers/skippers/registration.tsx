import React, { useState } from 'react';
import SkipperRegistrationContextProps, { SkipperRegistrationStepData } from '../../types/SkipperRegistrationContextProps';
import skipperApi from '../../services/skipperService/registrationApi';
import skillsApi from '../../services/skipperService/skillsApi';
import { ISkill } from '../../types/ISkill';
import { ICountry } from '../../types/ICountry';
import metaApi from '../../services/shared/metaApi';


export const SkipperRegistrationContext = React.createContext<SkipperRegistrationContextProps>({

  stepData: {
    id: "",
    firstName: "",
    lastName: "",
    email: "",
    oib: "",
    password: "",
    repeatPassword: "",
    dateOfBirth: "",
    address: "",
    UserPhoto: {
      Name: "",
      photoURL: "",
      Data: ""
    },
    licenceURL: "",
    zipCode: "",
    city: "",
    country: {
      id: 0,
      englishName: "",
      twoLetterCode: "",
      lable: "",
      skipperId: ""
    },
    phoneNumber: "",
    price: "",
    licence: {
      dateOfIssue: "",
      licenceLevel: "",
      validUntil: "",
      licenceData: new Blob(),
      licenceName: ""
    },
    yearOfFirsLicence: -1,
    skills: [{
      id: "",
      name: "",
    }],
    language: [{
      id: "",
      name: "",
    }],
    tos: false,
    pp: false,
    listOfLanguages: [],
    listOfSkills: []
  },
  setStepData: () => null,

  stepNumber: 0,
  setStepNumber: () => null,

  skipperRegistration: () => null,
  updateSkipper: () => null,
  skills: [{
    id: 0,
    name: "",
    icon: ""
  }],
  countries: [{
    id: -1,
    englishName: "",
    twoLetterCode: "",
    lable: "",
    skipperId: ""
  }],
  getSkills: () => null,
  setSkills: () => null,
  getCountries: () => null
});

export const SkipperRegistrationProvider: React.ComponentType<React.ReactNode> = props => {

  const [stepData, setStepData] = useState();

  const [stepNumber, setStepNumber] = useState<number>(0);

  const [skills, setSkills] = useState<ISkill[]>([]);

  const [countries, setCountries] = useState<ICountry[]>([]);

  const skipperRegistration = async (skipperData: SkipperRegistrationStepData) => {
    const response = await skipperApi.registerSkipper(skipperData);
    return response;
  }

  const getCountries = async () => {
    const response = await metaApi.getCountries();
    setCountries(response);
  }

  const getSkills = async () => {
    const response = await skillsApi.getSkills();
    return response;
  }

  const updateSkipper = async (skipperData: SkipperRegistrationStepData) => {
    const response = await skipperApi.updateSkipper(skipperData);
    return response;
  }

  return (
    <SkipperRegistrationContext.Provider
      value=
      {{
        stepData, setStepData, stepNumber, setStepNumber,
        skipperRegistration, updateSkipper,
        getSkills, skills, setSkills, countries, getCountries
      }}>
      {props.children}
    </SkipperRegistrationContext.Provider>
  );
};