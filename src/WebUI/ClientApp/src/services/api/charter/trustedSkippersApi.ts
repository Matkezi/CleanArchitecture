import { api } from '../../api';
import { TrustedSkipperProfile } from '../../../types/TrustedSkippers';


export default
    {
        getPendingSkippers(): Promise<TrustedSkipperProfile> {
            return api.get('Skipper/pending')
        },
        getAcceptedSkippers(): Promise<TrustedSkipperProfile> {
            return api.get('Skipper/trusted')
        },
        getDeclinedSkippers(): Promise<TrustedSkipperProfile> {
            return api.get('Skipper/untrusted')
        },
        updateTrustedSkippers(trustedSkippers: string[]): Promise<any> {
            return api.put('Skipper/trusted', trustedSkippers)
        },
        updateUnTrustedSkippers(untrustedSkippers: string[]): Promise<any> {
            return api.put('Skipper/untrusted', untrustedSkippers)
        }
    };


