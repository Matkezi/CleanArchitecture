import React from "react";
import { Grid, Divider, Avatar } from "@material-ui/core";
import styles from "./routes.module.scss";
import { Route } from "react-router-dom";
import ContentLayout from "./ContentLayout";
import Logo from '../../assets/img/icons/logo-blue.png';
import Footer from "./Footer";

const SkipperRegisterRoute = ({ component: Component, history, ...rest }: any) => {

    return (
        <React.Fragment>
            <Grid style={{ padding: 10 }} container justify="space-between" alignItems="center">
                <Grid item xs={3}>
                    <img
                        className={styles.Logo}
                        src={Logo}
                        alt="logo"
                    ></img>
                </Grid>
                <Grid container alignItems="center" justify="flex-end" item xs={9}>
                    <Grid container item xs={12} md={4} alignItems="center" justify="space-around" >
                        <Grid item className={styles.ActiveNavTab + " " + styles.NavTab}>
                            <div
                                className={styles.Links}
                            >
                                <span>Registration</span>
                            </div>
                        </Grid>
                        <Grid item className={styles.NavTab}>
                            <div
                                className={styles.Links}
                            >
                                <Avatar aria-label="recipe" className={styles.Avatar}>
                                    S
                                </Avatar>
                            </div>
                        </Grid>
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

export default SkipperRegisterRoute;
