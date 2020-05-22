import React, { useContext, useEffect, useState } from 'react';
import { Boat } from '../../types/Boat';
import boatsApi from '../../services/api/charter/boats';
import { IBoatType } from '../../types/BoatType';
import { ILicenceType } from '../../types/LicenceType';
import AddBoatComponent from '../../components/charter/boats/addBoat';
import styles from './styles.module.scss';
import { NotificationContext } from '../../providers/notification';
import { NotificationType } from '../../types/NotificationProps';
import { BoatContext } from '../../providers/boats/boatsContext';
import BoatsList from '../../components/charter/boats/boatsList';
import { LinearProgress, withStyles } from '@material-ui/core';

interface IProps {
    setActiveTab: (tab: number) => void
}

const CustomLinearProgres = withStyles({
    colorPrimary: {
        backgroundColor: '#26806b',
    },
    barColorPrimary: {
        backgroundColor: '#B2DFDB',
    }
})((props: any) => <LinearProgress {...props} />);

const BoatContainer: React.FC<IProps> = (props: IProps) => {

    const notificationContext = useContext(NotificationContext);
    const boatContext = useContext(BoatContext);
    const [loading, setLoading] = useState(false);

    const getBoats = async () => {
        setLoading(true);
        await boatContext.setCharterBoats();
        setLoading(false);
    }

    useEffect(() => {
        props.setActiveTab(3);
        getBoats();
    }, [])

    const saveBoat = async (boat: Boat) => {
        const boatModel = boat;
        delete boatModel.boatPhotoUrl;
        try {
            notificationContext.setLoading({ showLoading: true })
            await boatContext.saveBoat({
                ...boatModel,
                minimalRequiredLicense: (boat.minimalRequiredLicense as unknown as ILicenceType).id,
                type: (boat.type as unknown as IBoatType).id
            });
            notificationContext.setLoading({ showLoading: false })
            boatContext.setShowForm(false);
            notificationContext.setSnackbar({ showSnackbar: true, message: "Boat saved!", type: NotificationType.Success })
            getBoats();
        } catch (err) {
            notificationContext.setLoading({ showLoading: false })
            notificationContext.setSnackbar({ showSnackbar: true, message: err.message, type: NotificationType.Error })
        }
    }

    const updateBoat = async (id: number, boat: Boat) => {
        try {
            notificationContext.setLoading({ showLoading: true })
            await boatContext.updateBoat(id, {
                ...boat,
                id: id,
                minimalRequiredLicense: (boat.minimalRequiredLicense as unknown as ILicenceType).id,
                type: (boat.type as unknown as IBoatType).id
            });
            notificationContext.setLoading({ showLoading: false })
            notificationContext.setSnackbar({ showSnackbar: true, message: "Boat updated!", type: NotificationType.Success })
            getBoats();
        } catch (err) {
            notificationContext.setLoading({ showLoading: false })
            notificationContext.setSnackbar({ showSnackbar: true, message: err.message, type: NotificationType.Error })
        }
    }

    const deleteBoat = async (id: number) => {
        try {
            notificationContext.setLoading({ showLoading: true })
            await boatContext.deleteBoat(id);
            notificationContext.setLoading({ showLoading: false })
            notificationContext.setSnackbar({ showSnackbar: true, message: "Boat deleted!", type: NotificationType.Success })
            getBoats();
        } catch (err) {
            notificationContext.setLoading({ showLoading: false })
            notificationContext.setSnackbar({ showSnackbar: true, message: err.message, type: NotificationType.Error })
        }
    }

    return (
        <div className={styles.wrapper}>
            <AddBoatComponent saveBoat={saveBoat} />
            {!loading ?
                <BoatsList boats={boatContext.charterBoats} saveBoat={saveBoat} updateBoat={updateBoat} deleteBoat={deleteBoat} />
                :
                <CustomLinearProgres className={styles.linearProgress} />
            }
        </div>
    );
}

export default BoatContainer;