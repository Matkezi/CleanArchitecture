import React, { useContext } from 'react';
import SettingsComponent from '../../components/skippers/SettingsComponent';
import { CharterContext } from '../../providers/charter/charter';
import accountApi from '../../services/shared/accountApi';
import { NotificationContext } from '../../providers/notification';
import { NotificationType } from '../../types/NotificationProps';

interface IProps { }

const CharterSetting: React.FC<IProps> = (props: IProps) => {

    const charterContext = useContext(CharterContext);
    const email = charterContext.charterData ? charterContext.charterData.email! : "";
    const notificationContext = useContext(NotificationContext);

    const requestEmailChange = async (newEmail: string) => {
        try {
            await accountApi.sendChangeEmailRequest(email, newEmail);
        } catch (error) {
            notificationContext.setSnackbar({ showSnackbar: true, message: "Could not send change email request", type: NotificationType.Error })
        }
    }

    const changePassword = async (currentPassword: string, newPassword: string) => {
        try {
            await accountApi.changePassword(email, currentPassword, newPassword);
        } catch (error) {
            notificationContext.setSnackbar({ showSnackbar: true, message: "Could not change your password", type: NotificationType.Error })
        }
    }

    return <SettingsComponent changePassword={changePassword} email={email} requestEmailChange={requestEmailChange} />
}

export default CharterSetting;