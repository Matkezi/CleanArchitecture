import React, { useContext, useState } from 'react';
import LoginLayout from '../public/loginLayout';
import ChangeEmailComponent from '../../components/skippers/ChangeEmailComponent';
import accountApi from '../../services/shared/accountApi';
import { LinearProgress } from '@material-ui/core';
import { NotificationContext } from '../../providers/notification';
import styles from './styles.module.scss'

interface IProps {
    match: {
        params: {
            email: string,
            newEmail: string,
            token: string
        }
    },
    history: any
}

const ChangeEmail: React.FC<IProps> = (props: IProps) => {

    const notificationContext = useContext(NotificationContext);
    const { email, newEmail, token } = props.match.params;
    const [loading, setLoading] = useState(false);
    const [isSuccessful, setIsSuccessful] = useState(false);

    const changeEmail = async () => {
        try {
            setLoading(true);
            await accountApi.changeEmail(email, newEmail, token);
            setLoading(false);
            setIsSuccessful(true);
            setTimeout(() => {
                props.history.push("/login")
            }, 4000);
        } catch (error) {
            setLoading(false);
            notificationContext.setSnackbar({ showSnackbar: true, message: "Can't change your email. Please try again." })
        }
    }

    return <LoginLayout>
        <ChangeEmailComponent loading={loading} newEmail={newEmail} changeEmail={changeEmail} isSuccessful={isSuccessful} />
        {loading && <LinearProgress className={styles.progressbar} />}
    </LoginLayout>
}

export default ChangeEmail;