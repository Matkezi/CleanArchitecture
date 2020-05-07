import { ILanguage } from './ILanguage';
export interface LanguageProps {
    languages: ILanguage[]
}

export interface LanguageFunc extends LanguageProps {
    getLanguages: Function
}