import { api } from '../../api';
import { ISkipper } from '../../../types/ISkipper';
import { AvalibilityCalendar } from '../../../types/Avalibility';

export default
    {
        getSkipperById(username: string): Promise<any> {
            return api.get('Skipper/' + username);
        },
        updateSkipper(skipper: ISkipper): Promise<any> {
            return api.put('Skipper/' + skipper.skipperData.id, skipper.skipperData)
        },
        getSkipperAvalibility(id: string): Promise<AvalibilityCalendar> {
            return api.get('Availability/' + id);
        },
        updateSkipperAvalibility(avalibility: AvalibilityCalendar): Promise<any> {
            return api.put('Skipper/avalibility/update', avalibility)
        }
    };