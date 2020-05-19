import { api } from '../../api';
import { SkipperRegistrationStepData, SkipperPreRegistration } from '../../../types/SkipperRegistrationContextProps';


export default
    {
        registerSkipper(registrationData: SkipperRegistrationStepData): Promise<any> {
            return api.post('Skipper', registrationData)
        },
        updateSkipper(registrationData: SkipperRegistrationStepData): Promise<any> {
            return api.put('Skipper/' + registrationData.id, registrationData)
        },
        preRegisterSkipper(registrationData: SkipperPreRegistration): Promise<any> {
            return api.post('Skipper/preregister', registrationData)
        },
        getPreRegistration(url: String): Promise<SkipperPreRegistration> {
            return api.get('Skipper/preregister/' + url)
        },
    };