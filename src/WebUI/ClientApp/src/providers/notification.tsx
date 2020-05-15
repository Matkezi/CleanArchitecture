import React, { useState } from 'react';
import { NotificationProps, NotificationType } from '../types/NotificationProps';

export const NotificationContext = React.createContext<NotificationProps>({
    loading: {
        showLoading: false
    },
    snackbar: {
        showSnackbar: false,
        message: "",
        type: NotificationType.Info
    },
    setLoading: () => null,
    setSnackbar: () => null
});

export const NotificationProvider: React.ComponentType<React.ReactNode> = props => {
    const [loading, setLoading] = useState({ showLoading: false });
    const [snackbar, setSnackbar] = useState({
        showSnackbar: false,
        message: "",
        type: NotificationType.Info,
    })

    return (
        <NotificationContext.Provider
            value=
            {{
                loading, setLoading,
                snackbar, setSnackbar
            }}>
            {props.children}
        </NotificationContext.Provider>
    );
};