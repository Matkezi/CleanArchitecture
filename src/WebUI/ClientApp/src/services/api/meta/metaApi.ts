import { ICountry } from '../../../types/ICountry';
import { api } from '../../api';
import { ILanguage } from '../../../types/ILanguage';
import { API } from '../../../constants/apiRoutes';

export default {
    getLanguages(): Promise<ILanguage[]> {
        return api.get(API.META.GET_LANGUAGES)
    },
    getCountries(): Promise<ICountry[]> {
        return api.get(API.META.GET_COUNTRIES)
    }
}