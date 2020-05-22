import { api } from '../../api';

export default
    {
        sendResetPasswordEmail(email: string): Promise<any> {
            return api.post('Account/password-reset-email/' + email)
        },
        resetPassword(email: string, password: string, token: string): Promise<any> {
            return api.post('Account/password-reset/' + email + "/" + token + "/" + password);
        },
        sendChangeEmailRequest(email: string, newEmail: string): Promise<any> {
            return api.post('Account/email-reset-email/' + email + '/' + newEmail)
        },
        changeEmail(email: string, newEmail: string, token: string): Promise<any> {
            return api.post('Account/change-email/' + email + '/' + newEmail + '/' + token)
        },
        changePassword(email: string, currentPassword: string, newPassword: string): Promise<any> {
            return api.post('Account/change-password/' + email + '/' + currentPassword + '/' + newPassword)
        }
    };