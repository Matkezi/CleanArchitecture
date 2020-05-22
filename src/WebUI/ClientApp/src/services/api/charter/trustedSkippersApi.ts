import { api } from '../../api';
import { TrustedSkipperProfile } from '../../../types/TrustedSkippers';
import { API } from '../../../constants/apiRoutes';


export default
    {
        getPendingSkippers(): Promise<TrustedSkipperProfile> {
            return api.get(API.CHARTER.SKIPPERS.PENDING)
        },
        getAcceptedSkippers(): Promise<TrustedSkipperProfile> {
            return api.get(API.CHARTER.SKIPPERS.TRUSTED)
        },
        getDeclinedSkippers(): Promise<TrustedSkipperProfile> {
            return api.get(API.CHARTER.SKIPPERS.UNTRUSTED)
        },
        updateTrustedSkippers(trustedSkippers: string[]): Promise<any> {
            return api.put(API.CHARTER.SKIPPERS.UPDATE_TRUSTED, trustedSkippers)
        },
        updateUnTrustedSkippers(untrustedSkippers: string[]): Promise<any> {
            return api.put(API.CHARTER.SKIPPERS.UPDATE_UNTRUSTED, untrustedSkippers)
        }
    };


