import { ICountry } from '../../../types/ICountry';
import { api } from '../../api';
import { ILanguage } from '../../../types/ILanguage';

export default {
    getLanguages(): Promise<ILanguage[]> {
        return api.get("Meta/languages")
    },
    getCountries(): Promise<ICountry[]> {
        return api.get("Meta/countries")
    }
}