import React from 'react';
import { NotificationContext } from '../providers/notification';
import { NotificationProps } from '../types/NotificationProps';
import { AppLoading } from './../components/ui/AppLoading'

const LoadingConsumer: React.FC = () => {

    return (
        <NotificationContext.Consumer>
            {(notificationContext: NotificationProps) =>
                <React.Fragment>
                    <AppLoading showLoading={notificationContext.loading.showLoading} />
                </React.Fragment>}
        </NotificationContext.Consumer>
    );
}
export default LoadingConsumer;
