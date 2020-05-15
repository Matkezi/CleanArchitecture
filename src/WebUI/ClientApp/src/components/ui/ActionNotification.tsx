import React, { useState, useEffect, useContext } from 'react'
import styles from './actionNotification.module.scss';
import { SnackbarContent, Snackbar } from '@material-ui/core';
import CheckCircleIcon from '@material-ui/icons/CheckCircle';
import ErrorIcon from '@material-ui/icons/Error';
import InfoIcon from '@material-ui/icons/Info';
import { ISnackbarData, NotificationType } from '../../types/NotificationProps';
import { NotificationContext } from './../../providers/notification';

interface IProps extends ISnackbarData {
    close?: () => void
}

export const ActionNotification: React.FC<IProps> = (props: IProps) => {
    const [open, setOpen] = useState(props.showSnackbar);
    const notificationContext = useContext(NotificationContext);

    useEffect(() => {
        setOpen(props.showSnackbar)
    }, [props])


    function getIcon() {
        switch (props.type) {
            case NotificationType.Success: return <CheckCircleIcon className={styles.Icon} />
            case NotificationType.Error: return <ErrorIcon className={styles.Icon} />
            case NotificationType.Info: return <InfoIcon className={styles.Icon} />
        }
    }

    return open ? <div className={styles.Root}>
        <Snackbar
            anchorOrigin={{
                vertical: 'top',
                horizontal: 'center',
            }}
            open={open}
            autoHideDuration={4000}
            onClose={() => {
                setOpen(false);
                notificationContext.setSnackbar(false)
            }}
        >
            <SnackbarContent
                className={props.type && styles[props.type]}
                aria-describedby="client-snackbar"
                message={
                    <span id="client-snackbar" className={styles.Message}>
                        {getIcon()}
                        {props.message}
                    </span>
                }
            />

        </Snackbar>
    </div> : null;
}