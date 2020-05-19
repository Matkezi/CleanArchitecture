import React, { useState } from 'react';
import { ICharterContext } from '../../types/Charter';
import charterApi from '../../services/api/charter/charterApi';

export const CharterContext = React.createContext<ICharterContext>({
    charterData: {
        id: "",
        email: "",
        newEmail: "",
        charterName: "",
        address: "",
        zipCode: "",
        city: "",
        country: "",
        trustedSkippers: []
    },
    getCharterById: (id: string) => null,
    setData: (data: ICharterContext["charterData"]) => null
});

export const CharterProvider: React.ComponentType<React.ReactNode> = props => {

    const [charterData, setCharterData] = useState<ICharterContext["charterData"]>({
        id: "",
        email: "",
        newEmail: "",
        charterName: "",
        address: "",
        zipCode: "",
        city: "",
        country: "",
        trustedSkippers: []
    });

    const getCharterById = async (id: string) => {
        const response = await charterApi.getCharterById(id);
        setCharterData(response);
        return response;
    }

    const setData = (data: ICharterContext["charterData"]) => {
        setCharterData(data);
    }

    return (
        <CharterContext.Provider
            value=
            {{
                charterData, setData, getCharterById
            }}>
            {props.children}
        </CharterContext.Provider>
    );
};