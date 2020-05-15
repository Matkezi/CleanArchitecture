import React, { useState, useEffect, useContext } from "react";
import { Grid, Divider } from "@material-ui/core";
import styles from './routes.module.scss';
import { Route, Link } from "react-router-dom";
import ContentLayout from "./ContentLayout";
import { GuestBookingContext } from "../../providers/booking/guestBooking";
import Logo from '../../assets/img/icons/logo-blue.png';
import Footer from "./Footer";

const SkipperRoute = ({ component: Component, history, ...rest }: any) => {
    const [activeTab, setActiveTab] = useState<number>(1);
    const guestBookingContext = useContext(GuestBookingContext);

    useEffect(() => {

    }, []);
    return (
        <React.Fragment>
            <Grid style={{ padding: 10 }} container justify="space-between" alignItems="center">
                <Grid item xs={3} onClick={() => history.push("/")}>
                    <img
                        className={styles.Logo}
                        src={Logo}
                        alt="logo"
                    ></img>
                </Grid>
                <Grid
                    container
                    alignItems="center"
                    justify="space-around"
                    item
                    xs={9}
                    sm={4}
                >
                    <Grid item className={activeTab === 1 ? styles.ActiveNavTab + " " + styles.NavTab : styles.NavTab}>
                        <Link
                            className={styles.Links}
                            to={"/guest/booking/" + guestBookingContext.booking.bookingURL! + "/step=1"}
                            onClick={() => setActiveTab(1)}
                        >
                            <span>My booking</span>
                        </Link>
                    </Grid>
                    <Grid item className={activeTab === 2 ? styles.ActiveNavTab + " " + styles.NavTab : styles.NavTab}>
                        <Link
                            className={styles.Links}
                            to="#"
                            onClick={() => setActiveTab(2)}
                        >
                            <span>About</span>
                        </Link>
                    </Grid>
                    <Grid item className={activeTab === 3 ? styles.ActiveNavTab + " " + styles.NavTab : styles.NavTab}>
                        <div>
                            <img
                                className={styles.Avatar}
                                src="https://music-box.hr/wp-content/uploads/2017/03/Avatar-770x439_c.jpg"
                                alt="avatar"
                            ></img>
                        </div>
                    </Grid>
                </Grid>
                <Grid item xs={12}>
                    <Divider />
                </Grid>
            </Grid>
            <ContentLayout>
                <Route
                    {...rest}
                    render={(props: any) =>
                        <Component {...props} />
                    }
                />
            </ContentLayout>
            <Footer history={history} />
        </React.Fragment>
    );
};

export default SkipperRoute;
