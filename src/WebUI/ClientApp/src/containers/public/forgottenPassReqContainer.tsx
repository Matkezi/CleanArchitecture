import React, { useState } from 'react';
import LoginLayout from './loginLayout';
import ForgottenPasswordRequest from '../../components/shared/login/forgottenPasswordReq';
import accountApi from '../../services/shared/accountApi';
import { LinearProgress } from '@material-ui/core';
import styles from './login/styles.module.scss';



const ForgottenPassReqContainer = () => {

    const [showLoading, setShowLoading] = useState(false);
    const [showSuccess, setShowSuccess] = useState(false);

    const changePasswordEmail = async (email: string) => {
        setShowLoading(true);
        try {
            await accountApi.sendResetPasswordEmail(email);
            setTimeout(() => {
                setShowLoading(false);
                setShowSuccess(true);
            }, 1500);
        } catch (err) {
            setShowLoading(false);
        }
    }

    return <LoginLayout>
        <ForgottenPasswordRequest disabled={showLoading} showSuccess={showSuccess} sendChangePasswordEmail={changePasswordEmail} />
        {showLoading && <LinearProgress className={styles.progressbar} />}
    </LoginLayout>
}

export default ForgottenPassReqContainer;