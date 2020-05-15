import React, { useContext } from 'react'
import CircularProgress from '@material-ui/core/CircularProgress';
import { Modal } from '@material-ui/core';
import styles from './appLoading.module.scss';
import { LoginContext } from '../../providers/login';
import { isCharter, isSkipper } from "../../services/appService/authorizationService";

interface IProps {
    showLoading: boolean
}

export const AppLoading: React.FC<IProps> = (props: IProps) => {
    const loginContext = useContext(LoginContext);

    return props.showLoading ? <Modal open={props.showLoading} disableBackdropClick={true}>
        <CircularProgress className={isCharter() ? styles.RootCharter : isSkipper ?  styles.RootSkipper : styles.Root} />
    </Modal> : null
}