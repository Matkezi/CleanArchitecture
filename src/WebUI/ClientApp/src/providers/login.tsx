import React, { useState } from "react";
import { LoginProps, ILoginData } from "../types/LoginProps";
import loginApi from "../services/api/account/loginApi";
import { useHistory } from "react-router-dom";
import { CLIENT } from "../constants/clientRoutes";

export const LoginContext = React.createContext<LoginProps>({
  loginData: {
    token: "",
    username: "",
    id: "",
    role: ""
  },
  setLoginData: () => null,
  doFacebookLogin: () => null,
  doLogin: () => null
});

export const LoginProvider: React.ComponentType<React.ReactNode> = props => {
  const [loginData, setLoginData] = useState({
    token: "",
    username: "",
    id: "",
    role: ""
  });
  let history = useHistory();

  const doFacebookLogin = async (facebookToken: string) => {
    const loginResponse = await loginApi.facebookLoginApi(facebookToken);
    setLoginData(loginResponse);
    window.localStorage.setItem("authToken", loginResponse.token);
    if (loginResponse.role === "Skipper") {
      history.push(CLIENT.SKIPPER.DASHBOARD);
    } else if (loginResponse.role === "Charter") {
      history.push(CLIENT.CHARTER.DASHBOARD);
    } else if (loginResponse.role === "Admin") {
      history.push(CLIENT.CHARTER.DASHBOARD);
    }
  };

  const doLogin = async (loginData: ILoginData) => {
    const loginResponse = await loginApi.loginApi(loginData);
    setLoginData({ ...loginResponse });
    window.localStorage.setItem("authToken", loginResponse.token);
    if (loginResponse.role === "Skipper") {
      history.push(CLIENT.SKIPPER.DASHBOARD);
    } else if (loginResponse.role === "Charter") {
      history.push(CLIENT.CHARTER.DASHBOARD);
    } else if (loginResponse.role === "Admin") {
      history.push(CLIENT.CHARTER.DASHBOARD);
    }
  };
  return (
    <LoginContext.Provider
      value={{
        loginData,
        setLoginData,
        doFacebookLogin,
        doLogin
      }}
    >
      {props.children}
    </LoginContext.Provider>
  );
};
