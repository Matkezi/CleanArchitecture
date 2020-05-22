import React, { useContext, useState, useEffect } from 'react';
import { SkipperRegistrationContext } from '../../providers/skippers/registration';
import SkipperRegistrationFormStep1 from '../../components/skippers/registration/step1Form';
import SkipperRegistrationFormStep2 from '../../components/skippers/registration/step2Form';
import SkipperRegistrationFormStep3 from '../../components/skippers/registration/step3Form';
import SkipperRegistrationFormStep4 from '../../components/skippers/registration/step4Form';
import { StepperHelper } from '../../helpers/StepperHelper';

import { SkipperRegistrationStepData } from '../../types/SkipperRegistrationContextProps';
import { NotificationContext } from '../../providers/notification';
import { LanguageContext } from './../../providers/langugages';
import { LoginContext } from '../../providers/login';
import { NotificationType } from '../../types/NotificationProps';
import { ISkill } from '../../types/ISkill';
import { RouteComponentProps } from 'react-router-dom';
import styles from "./styles.module.scss"
import SkipperApi from '../../services/api/skipper/registrationApi'
import { CLIENT } from '../../constants/clientRoutes';

const SkipperRegistration: React.FC<RouteComponentProps> = (props: RouteComponentProps<any>) => {
    const skipperRegistrationContext = useContext(SkipperRegistrationContext);
    const notificationContext = useContext(NotificationContext);
    const loginContext = useContext(LoginContext);
    const languageContext = useContext(LanguageContext);

    const [photo, setPhoto] = useState({
        photoData: { name: "" },
        photoURL: "",
        readerData: ""
    });


    useEffect(() => {
        async function getPreregistration() {
            notificationContext.setLoading({ showLoading: true })
            try {
                var preRegistration = await SkipperApi.getPreRegistration(props.match.params.url);
                skipperRegistrationContext.setStepData({
                    ...skipperRegistrationContext.stepData,
                    firstName: preRegistration.firstName,
                    lastName: preRegistration.lastName,
                    email: preRegistration.email,
                    tos: false,
                    pp: false
                })
            } catch (e) {
                notificationContext.setLoading({ showLoading: false })
                notificationContext.setSnackbar({ showSnackbar: true, message: e.message, type: NotificationType.Error })
            } finally {
                notificationContext.setLoading({ showLoading: false })
            }

        }
        async function getSkills() {
            var skills: ISkill[] = await skipperRegistrationContext.getSkills();
            skipperRegistrationContext.setSkills(Array.from(skills));
        }
        async function getLanguages() {
            await languageContext.getLanguages();
        }
        async function getCountries() {
            await skipperRegistrationContext.getCountries();
        }
        getSkills();
        getLanguages();
        getCountries();
        getPreregistration();
    }, []);

    const handleChange = (event: any) => {
        event.persist();
        const reader = new FileReader();
        if (event.target.files[0] instanceof Blob) {
            reader.readAsDataURL(event.target.files[0]);
            reader.onloadend = () => {
                setPhoto({ photoData: event.target.files[0], photoURL: URL.createObjectURL(event.target.files[0]), readerData: reader.result as string });
            }
        }
    }

    const saveStep = async (values: SkipperRegistrationStepData) => {
        const UserPhoto = {
            UserPhoto: {
                Name: photo.photoData!.name,
                Data: photo.readerData
            }
        }
        skipperRegistrationContext.setStepData({ ...skipperRegistrationContext.stepData, ...values, ...UserPhoto });

        switch (StepperHelper.getStep()) {
            case 1:
                notificationContext.setLoading({ showLoading: true });
                try {
                    var registerResponse = await skipperRegistrationContext.skipperRegistration({ ...skipperRegistrationContext.stepData, ...values });
                    const id = registerResponse.id;
                    skipperRegistrationContext.setStepData({ ...skipperRegistrationContext.setStepData, ...values, id })
                    notificationContext.setLoading({ showLoading: false })
                    notificationContext.setSnackbar({ showSnackbar: true, message: "Registration was successful!", type: NotificationType.Success })
                    setTimeout(() => {
                        props.history.push(StepperHelper.increasedStepUrl());
                    }, 4100);
                } catch (err) {
                    var message = "";
                    if ((err.message as string).startsWith("User name")) message = (err.message as string).replace("User name", "Email");
                    else message = err.message;
                    notificationContext.setLoading({ showLoading: false })
                    notificationContext.setSnackbar({ showSnackbar: true, message: message, type: NotificationType.Error })
                }
                break;
            case 2:
            case 3:
                props.history.push(StepperHelper.increasedStepUrl());
                break;
            case 4:
                notificationContext.setLoading({ showLoading: true });
                try {
                    await skipperRegistrationContext.updateSkipper({ ...skipperRegistrationContext.stepData, ...values, });
                    notificationContext.setLoading({ showLoading: false })
                    notificationContext.setSnackbar({ showSnackbar: true, message: "You completed your profile!", type: NotificationType.Success })
                    setTimeout(async () => {
                        await loginContext.doLogin({ email: skipperRegistrationContext.stepData!.email, password: skipperRegistrationContext.stepData!.password, rememberMe: true });
                        props.history.push(CLIENT.SKIPPER.PROFILE);
                    }, 4100);
                } catch (err) {
                    notificationContext.setLoading({ showLoading: false })
                    notificationContext.setSnackbar({ showSnackbar: true, message: err.message, type: NotificationType.Error })
                }
                break;
        }

    }

    const goBack = () => {
        props.history.push(StepperHelper.decreasedStepUrl());
    }

    function getStepContent() {
        switch (StepperHelper.getStep()) {
            case 1:
                return <SkipperRegistrationFormStep1 saveStep1={saveStep} values={skipperRegistrationContext.stepData!} />
            case 2:
                return <SkipperRegistrationFormStep2 saveStep2={saveStep} values={skipperRegistrationContext.stepData!} handleChange={handleChange} countries={skipperRegistrationContext.countries} photoURL={photo.photoURL} />
            case 3:
                return <SkipperRegistrationFormStep3 saveStep3={saveStep} goBack={goBack} values={skipperRegistrationContext.stepData!} />
            case 4:
                return <SkipperRegistrationFormStep4 saveStep4={saveStep} goBack={goBack} skills={skipperRegistrationContext.skills} languages={languageContext.languages} values={skipperRegistrationContext.stepData!} />
        }
    }

    return (
        <React.Fragment>
            <div className={styles.wrapper}>
                {getStepContent()}
            </div>
        </React.Fragment>

    );
}
export default SkipperRegistration;


