export interface ILoginData {
  email: string;
  password: string;
  rememberMe: boolean;
}
export interface LoginProps {
  loginData: {
    token: string;
    username: string;
    id: string;
    role: string;
  };
  setLoginData: Function;
  doFacebookLogin: Function;
  doLogin: Function;
}
export interface LoginResponse {
  token: string;
  username: string;
  id: string;
  role: string;
  userPhotoUrl: string;
}
