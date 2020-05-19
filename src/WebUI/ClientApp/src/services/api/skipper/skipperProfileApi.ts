import { api } from '../../api';
import { ISkipper } from '../../../types/ISkipper';
import { AvalibilityCalendar } from '../../../types/Avalibility';
import { API } from '../../../constants/apiRoutes';

export default
    {
        getSkipperById(username: string): Promise<any> {
            return api.get(API.SKIPPER.GET(username));
        },
        updateSkipper(skipper: ISkipper): Promise<any> {
            return api.put(API.SKIPPER.UPDATE(skipper.skipperData.id), skipper.skipperData)
        },
        getSkipperAvalibility(id: string): Promise<AvalibilityCalendar> {
            return api.get(API.SKIPPER.AVAILABILITY.GET_AVAILABILITY(id));
        },
        updateSkipperAvalibility(avalibility: AvalibilityCalendar): Promise<any> {
            return api.put(API.SKIPPER.AVAILABILITY.UPDATE_AVAILABILITY, avalibility)
        }
    };