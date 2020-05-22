import { api } from '../../api';
import { SkipperRegistrationStepData, SkipperPreRegistration } from '../../../types/SkipperRegistrationContextProps';
import { API } from '../../../constants/apiRoutes';


export default
    {
        registerSkipper(registrationData: SkipperRegistrationStepData): Promise<any> {
            return api.post(API.SKIPPER.REGISTER, registrationData)
        },
        updateSkipper(registrationData: SkipperRegistrationStepData): Promise<any> {
            return api.put(API.SKIPPER.UPDATE(registrationData.id!), registrationData)
        },
        preRegisterSkipper(registrationData: SkipperPreRegistration): Promise<any> {
            return api.post(API.SKIPPER.PREREGISTER, registrationData)
        },
        getPreRegistration(url: string): Promise<SkipperPreRegistration> {
            return api.get(API.SKIPPER.GET_PREREGISTRATION(url))
        },
    };