import React, { useContext } from "react";
import SkipperPreRegistrationForm from "../../components/skippers/registration/skipperPreRegistrationForm";
import { NotificationContext } from "../../providers/notification";
import RegistrationApi from "../../services/skipperService/registrationApi"
import { SkipperPreRegistration } from "../../types/SkipperRegistrationContextProps";
import { NotificationType } from "../../types/NotificationProps";

const SkipperPreregistration: React.FC = () => {

    const notificationContext = useContext(NotificationContext);

    const savePreRegistration = async (data: SkipperPreRegistration) => {
        notificationContext.setLoading({ showLoading: true });
        try {
            await RegistrationApi.preRegisterSkipper(data);
            notificationContext.setLoading({ showLoading: false })
            notificationContext.setSnackbar({ showSnackbar: true, message: "Skipper preristered!", type: NotificationType.Success })
        } catch (err) {
            var message = "";
            if ((err.message as string).startsWith("User name")) message = (err.message as string).replace("User name", "Email");
            else message = err.message;
            notificationContext.setLoading({ showLoading: false })
            notificationContext.setSnackbar({ showSnackbar: true, message: message, type: NotificationType.Error })
        }
    }
    return (
        <React.Fragment>
            <SkipperPreRegistrationForm savePreregistration={savePreRegistration}></SkipperPreRegistrationForm>       
        </React.Fragment>
    )
}

export default SkipperPreregistration;