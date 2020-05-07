import React from 'react';
import { NotificationContext } from '../providers/notification';
import { NotificationProps } from '../types/NotificationProps';
import { ActionNotification } from './../components/ui/ActionNotification'

const SnackbarConsumer: React.FC = () => {

    return (
        <NotificationContext.Consumer>
            {(notificationContext: NotificationProps) =>
                <React.Fragment>
                    <ActionNotification
                        showSnackbar={notificationContext.snackbar.showSnackbar}
                        message={notificationContext.snackbar.message}
                        type={notificationContext.snackbar.type}
                    />
                </React.Fragment>}
        </NotificationContext.Consumer>
    );
}
export default SnackbarConsumer;
