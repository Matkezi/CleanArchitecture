import SkipperLoginForm from '../../../components/shared/login/skipperLoginForm'
import ForgottenPasswordRequest from '../../../components/shared/login/forgottenPasswordReq'
import { ILoginData } from '../../../types/LoginProps';
import React, { useContext, useState } from 'react'
import { ReactFacebookLoginInfo } from 'react-facebook-login';
import { LoginContext } from '../../../providers/login';
import styles from './styles.module.scss'
import { Grid, LinearProgress } from '@material-ui/core'
import { NotificationContext } from '../../../providers/notification';
import { NotificationType } from '../../../types/NotificationProps';
import LoginLayout from '../loginLayout';
import Footer from '../../routing/Footer';

interface IProps {
    history: any
}


const SkipperLogin: React.FC<IProps> = (props: IProps) => {
    const loginContext = useContext(LoginContext);
    const notificationContext = useContext(NotificationContext);
    const [loading, setLoading] = useState(false)

    const submitLogin = async (loginData: ILoginData) => {
        setLoading(true);
        try {
            await loginContext.doLogin(loginData)
        } catch (e) {
            setLoading(false)
            notificationContext.setSnackbar({ showSnackbar: true, message: e.message, type: NotificationType.Info })
        } finally {
            setLoading(false)
        }
    }

    const submitFacebookLogin = async (facebookResponse: ReactFacebookLoginInfo) => {
        setLoading(true);
        try {
            await loginContext.doFacebookLogin(facebookResponse.accessToken)
        } catch (e) {
            setLoading(false)
            notificationContext.setSnackbar({ showSnackbar: true, message: e.message, type: NotificationType.Info })
        } finally {
            setLoading(false)
        }
    }

    return (
        <LoginLayout>
            <SkipperLoginForm history={props.history} facebookLoginAction={submitFacebookLogin} normalLoginAction={submitLogin}></SkipperLoginForm>
            {loading && <LinearProgress className={styles.progressbar} />}
        </LoginLayout>
    )
}

export default SkipperLogin;