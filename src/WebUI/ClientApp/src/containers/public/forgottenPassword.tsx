import React, { useState, useContext } from 'react';
import { Grid } from '@material-ui/core';
import ForgottenPasswordComponent from '../../components/shared/login/forgottenPassword';
import accountApi from '../../services/api/account/accountApi';
import LoginLayout from './loginLayout';
import { NotificationContext } from '../../providers/notification';
import { NotificationType } from '../../types/NotificationProps';
import { CLIENT } from '../../constants/clientRoutes';

interface IProps {
    match: {
        params: {
            email: string,
            token: string
        }
    },
    history: any
}

interface IData {
    password: string,
    repeatPassword: string
}

const ForgottenPassword: React.FC<IProps> = (props: IProps) => {

    const [isSuccessful, setIsSuccessful] = useState(false);
    const notificationContext = useContext(NotificationContext);

    const changePassword = async (data: IData) => {
        try {
            await accountApi.resetPassword(props.match.params.email, data.password, props.match.params.token);
            setIsSuccessful(true);
            setTimeout(() => {
                props.history.push(CLIENT.APP.LOGIN)
            }, 4000);
        } catch (err) {
            notificationContext.setSnackbar({ showSnackbar: true, message: "Can't change your password. Please, try again.", type: NotificationType.Error });
        }
    }

    return (
        <LoginLayout>
            <ForgottenPasswordComponent isSuccessful={isSuccessful} email={props.match.params.email} changePassword={changePassword} />
        </LoginLayout>)
}

export default ForgottenPassword;