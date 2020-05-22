import React, { useState } from "react";
import { ILanguage } from './../types/ILanguage';
import { LanguageFunc } from "../types/LanguageProps";
import metaApi from "../services/api/meta/metaApi";

export const LanguageContext = React.createContext<LanguageFunc>({
    languages: [],
    getLanguages: () => null
});

export const LanguageProvider: React.ComponentType<React.ReactNode> = props => {
    const [languages, setLanguages] = useState<ILanguage[]>([]);

    const getLanguages = async () => {
        const languageResponse = await metaApi.getLanguages();
        setLanguages(languageResponse);
    };

    return (
        <LanguageContext.Provider
            value={{
                languages, getLanguages
            }}
        >
            {props.children}
        </LanguageContext.Provider>
    );
};
