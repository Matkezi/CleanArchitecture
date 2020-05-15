import React from 'react'
import styles from './styles.module.scss';
import Header from './elements/Header';
import { Grid } from '@material-ui/core';

const AboutPage = () => {
    return <>
        <Header />
        <div className={styles.aboutPicture} />
        <Grid className={styles.aboutContainer} container item xs={12}>
            <Grid container item xs={12} md={6}>
                <Grid item xs={12}><div>FOR SKIPPERS</div></Grid>
                <Grid item xs={12}>
                    <div>Make better money working as a skipper.<br />
Reach new skipper opportunities.<br />
Increase your value to guests and agencies.<br />
                    </div></Grid>
            </Grid>
        </Grid>
    </>
}

export default AboutPage;