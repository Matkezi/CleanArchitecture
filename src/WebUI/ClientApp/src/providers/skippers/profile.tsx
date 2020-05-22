import React, { useState } from 'react';
import { ISkipperContext, ISkipper } from '../../types/ISkipper';
import skipperProfileApi from '../../services/api/skipper/skipperProfileApi';
import skillsApi from '../../services/api/skipper/skillsApi';
import { ISkill } from '../../types/ISkill';

const skipperDataEmpty: ISkipper["skipperData"] = {
    id: "",
    oib: "",
    email: "",
    newEmail: "",
    firstName: "",
    newPassword: "",
    lastName: "",
    dateOfBirth: "",
    address: "",
    zipCode: "",
    city: "",
    phoneNumber: "",
    country: "",
    price: 0,
    userPhotoUrl: "",
    skipperInsurancePolicy: "",
    personalSummary: "",
    listOfSkills: [{ id: -1, name: "", icon: "" }],
    listOfLanguages: [{ id: -1, label: "", levelOfKnowledge: -1 }]
};

export const SkipperProfileContext = React.createContext<ISkipperContext>({
    skipperData: skipperDataEmpty,
    skills: [],
    getSkipperById: (id: string) => null,
    updateSkipper: (skipper: ISkipper) => null,
    getSkills: () => null
});

export const SkipperProfileProvider: React.ComponentType<React.ReactNode> = props => {

    const [skipperData, setSkipperData] = useState<ISkipper["skipperData"]>({ ...skipperDataEmpty });
    const [skills, setSkills] = useState<ISkill[]>([]);

    const getSkipperById = async (id: string) => {
        const response = await skipperProfileApi.getSkipperById(id);
        setSkipperData(response);
        return response;
    }

    const updateSkipper = async (skipper: ISkipper) => {
        const response = await skipperProfileApi.updateSkipper(skipper);
        setSkipperData(response);
        return response;
    }
    const getSkills = async () => {
        const reponse = await skillsApi.getSkills();
        setSkills(reponse);
        return reponse;
    }

    return (
        <SkipperProfileContext.Provider
            value=
            {{
                skipperData, getSkipperById, updateSkipper,
                skills, getSkills
            }}>
            {props.children}
        </SkipperProfileContext.Provider>
    );
};