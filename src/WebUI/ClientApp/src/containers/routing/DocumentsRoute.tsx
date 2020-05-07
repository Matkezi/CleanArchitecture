import React from "react";
import { Grid } from "@material-ui/core";
import styles from "./routes.module.scss";
import { Route } from "react-router-dom";
import ContentLayout from "./ContentLayout";
import Logo from '../../assets/img/icons/logo-white.png';
import Footer from "./Footer";

const DocumentRoute = ({ component: Component, history, ...rest }: any) => {

    return (
        <div className={styles.background} style={{ height: "fit-content" }}>
            <React.Fragment>
                <Grid className={styles.container} container justify="space-between" alignItems="center">
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
                        justify="flex-end"
                        item
                        xs={9}
                        sm={4}
                    >
                        <div className={styles.agency}>SKIPPER AGENCY</div>
                    </Grid>
                </Grid>
                <ContentLayout document={true}>
                    <Route
                        {...rest}
                        render={(props: any) =>
                            <Component {...props} />
                        }
                    />
                </ContentLayout>
                <Footer history={history} secondaryColor={rest.secondaryColor} />
            </React.Fragment>
        </div>
    );
};

export default DocumentRoute;