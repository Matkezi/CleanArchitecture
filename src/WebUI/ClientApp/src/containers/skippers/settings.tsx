import React, { useContext, useState } from 'react';
import SettingsComponent from '../../components/skippers/SettingsComponent';
import { SkipperProfileContext } from '../../providers/skippers/profile';
import accountApi from '../../services/api/account/accountApi';
import { NotificationContext } from '../../providers/notification';
import { NotificationType } from '../../types/NotificationProps';

interface IProps { }

const SkipperSettings: React.FC<IProps> = (props: IProps) => {

    const skipperProfileContext = useContext(SkipperProfileContext);
    const email = skipperProfileContext.skipperData ? skipperProfileContext.skipperData.email! : "";
    const notificationContext = useContext(NotificationContext);

    const requestEmailChange = async (newEmail: string) => {
        try {
            await accountApi.sendChangeEmailRequest(email, newEmail);
            notificationContext.setSnackbar({ showSnackbar: true, message: "Instructions sent to your email!", type: NotificationType.Success })
        } catch (error) {
            notificationContext.setSnackbar({ showSnackbar: true, message: "Could not send change email request", type: NotificationType.Error })
        }
    }

    const changePassword = async (currentPassword: string, newPassword: string) => {
        try {
            await accountApi.changePassword(email, currentPassword, newPassword);
            notificationContext.setSnackbar({ showSnackbar: true, message: "Successfully changed your password.", type: NotificationType.Success })
        } catch (error) {
            notificationContext.setSnackbar({ showSnackbar: true, message: "Could not change your password", type: NotificationType.Error })
        }
    }

    return <SettingsComponent changePassword={changePassword} email={email} requestEmailChange={requestEmailChange} />
}

export default SkipperSettings;