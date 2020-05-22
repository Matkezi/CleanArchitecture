import { api } from '../../api';
import { LoginResponse, ILoginData } from '../../../types/LoginProps';


export default
  {
    facebookLoginApi(facebookToken: string): Promise<LoginResponse> {
      return api.post('Account/facebook-login', { authToken: facebookToken })
    },
    loginApi(loginData: ILoginData): Promise<LoginResponse> {
      return api.post('Account/login', loginData)
    }
  };
