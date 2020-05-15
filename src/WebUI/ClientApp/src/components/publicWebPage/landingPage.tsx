import React from 'react';
import styles from './styles.module.scss';
import Grid from '@material-ui/core/Grid';
import MailchimpSubscribe from "react-mailchimp-subscribe"
import SubscribeForm from "./SubscribeForm";
import FeatureCard from './elements/FeatureCard';
import PublicHeader from './elements/Header';
import feature1 from '../../assets/img/app-screenshots/skipperProfile.jpg';
import feature2 from '../../assets/img/app-screenshots/skipperRequests.jpg';
import feature3 from '../../assets/img/app-screenshots/skipperAvailability.png';
import { Divider } from '@material-ui/core';
import Footer from '../../containers/routing/Footer';

interface IProps {
    history: any
}

const LandingPage: React.FC<IProps> = (props: IProps) => {

    const url = "https://brain-it.us19.list-manage.com/subscribe/post?u=ac8fed5043dc78e2a23f5461e&amp;id=6ff572e353";

    return (
        <React.Fragment>
            <div className={styles.landingPicture} />
            <PublicHeader />
            <div className={styles.pitchText}>
                <span>Skipper Bookings. Made Easy.</span>
                <MailchimpSubscribe
                    url={url}
                    render={({ subscribe, status, message }) => (
                        <SubscribeForm
                            status={status}
                            message={message}
                            onValidated={(formData: any) => subscribe(formData)}
                        />
                    )}
                />
            </div>
            <Grid container className={styles.wrapper}>
                <Grid item xs={12} sm={12} md={12} lg={12} className={styles.title}>
                    <span className={styles.title}>Skipper App overview</span>
                </Grid>
                <Grid item xs={12} sm={4} container justify="center" className={styles.cardContainer}>
                    <FeatureCard img={feature1} name="Skipper Profile" desc="Great profile -> more bookings" text="Better profile you have, more chance of you getting booked ..." />
                </Grid>
                <Grid item xs={12} container className={styles.divider}>
                    <Divider />
                </Grid>
                <Grid item xs={12} sm={4} container justify="center">
                    <FeatureCard img={feature2} name="Manage Bookings" desc="Recieve booking requests" text="List of bookings waiting for your response.." />
                </Grid>
                <Grid item xs={12} container className={styles.divider}>
                    <Divider />
                </Grid>
                <Grid item xs={12} sm={4} container justify="center" style={{ marginBottom: 30 }}>
                    <FeatureCard img={feature3} name="Skipper Availability" desc="Fill your availability -> more bookings" text="If you constantly fill your availabiliy, more chance of you getting booked ..." />
                </Grid>
            </Grid>
            <Footer history={props.history} />
        </React.Fragment>
    )
}

export default LandingPage;