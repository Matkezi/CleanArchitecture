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
        try {
            notificationContext.setLoading({ showLoading: true })
            await boatsApi.saveBoat({
                ...boat,
                minimalRequiredLicence: (boat.minimalRequiredLicence as unknown as ILicenceType).value,
                type: (boat.type as unknown as IBoatType).value
            });
            notificationContext.setLoading({ showLoading: false })
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
            await boatsApi.updateBoat(id, {
                ...boat,
                id: id,
                minimalRequiredLicence: (boat.minimalRequiredLicence as unknown as ILicenceType).value,
                type: (boat.type as unknown as IBoatType).value
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
            await boatsApi.deleteBoat(id);
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
                <CustomLinearProgres />
            }
        </div>
    );
}

export default BoatContainer;