import React, { useState } from "react";
import { Grid } from "@material-ui/core";
import styles from "./styles.module.scss";
import { Link } from "react-router-dom";
import Logo from '../../../assets/img/icons/logo-white.png';
import { CLIENT } from "../../../constants/clientRoutes";

const PublicHeader = ({ component: Component, ...rest }: any) => {
    const [activeTab, setActiveTab] = useState<number>(1);
    return (
        <React.Fragment>
            <Grid className={styles.headerContainer} container justify="space-between" alignItems="center">
                <Grid item xs={3}>
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
                    <Grid item className={activeTab === 1 ? styles.LoginActiveNavTab + " " + styles.LoginNavTab : styles.LoginNavTab}>
                        <Link
                            className={styles.LoginLinks}
                            to={CLIENT.START_PAGE}
                            onClick={() => setActiveTab(1)}
                        >
                            <span>Landing</span>
                        </Link>
                    </Grid>
                    <Grid item className={activeTab === 2 ? styles.LoginActiveNavTab + " " + styles.LoginNavTab : styles.LoginNavTab}>
                        <Link
                            className={styles.LoginLinks}
                            to={CLIENT.PUBLIC.ABOUT}
                            onClick={() => setActiveTab(2)}
                        >
                            <span>About</span>
                        </Link>
                    </Grid>
                    <Grid item className={activeTab === 3 ? styles.LoginActiveNavTab + " " + styles.LoginNavTab : styles.LoginNavTab}>
                        <Link
                            className={styles.LoginLinks}
                            to={CLIENT.APP.LOGIN}
                            onClick={() => setActiveTab(3)}
                        >
                            <span>Login</span>
                        </Link>
                    </Grid>
                </Grid>
            </Grid>
        </React.Fragment>
    );
};

export default PublicHeader;