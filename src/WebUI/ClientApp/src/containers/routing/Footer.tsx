import React, { ReactNode } from 'react';
import { Grid } from '@material-ui/core';
import styles from './routes.module.scss'
import { isCharter, isSkipper } from '../../services/appService/authorizationService';
import { CLIENT } from '../../constants/clientRoutes';

interface IProps {
    secondaryColor?: boolean,
    history: any
};

const Footer: React.FC<IProps> = (props: IProps) => {

    return (
        <Grid container
            direction="row"
            alignItems="center"
            className={styles.Footer}
            item xs={12}>
            <Grid item xs={6} md={9} className={props.secondaryColor === true ? styles.FooterTextSecondary : styles.FooterText}>©{(new Date()).getFullYear()} Skipper Booking Inc.</Grid>
            <Grid item xs={6} md={3} justify="space-between" alignItems="center" container>
                <Grid item xs={6} className={props.secondaryColor === true ? styles.FooterTextSecondary : styles.FooterText}><div onClick={() => props.history.push(isCharter() ? CLIENT.PUBLIC.TOS.CHARTER : isSkipper() ? CLIENT.PUBLIC.TOS.SKIPPER : CLIENT.PUBLIC.TOS.GUEST)}>Terms of service</div></Grid>
                <Grid item xs={6} className={props.secondaryColor === true ? styles.FooterTextSecondary : styles.FooterText}><div onClick={() => props.history.push(CLIENT.PUBLIC.PRIVACY_POLICY)}>Privacy policy</div></Grid>
            </Grid>
        </Grid>
    );
};

export default Footer;