import React, { useState, useEffect, useContext } from "react";
import { Grid, Divider, Menu, MenuItem } from "@material-ui/core";
import styles from "./routes.module.scss";
import { Route, Redirect, Link } from "react-router-dom";
import ContentLayout from "./ContentLayout";
import { getUserId } from '../../services/appService/authorizationService';
import { isSkipper, getToken } from "../../services/appService/authorizationService";
import { SkipperProfileContext } from '../../providers/skippers/profile';
import { LoginContext } from "../../providers/login";
import Logo from '../../assets/img/icons/logo-blue.png';
import Footer from './Footer';
import Photo from '../../assets/img/icons/skipper-photo-empty.png'
import { CLIENT } from "../../constants/clientRoutes";

const SkipperRoute = ({ component: Component, history, ...rest }: any) => {
  const [activeTab, setActiveTab] = useState<number>(0);
  const skipperProfileContext = useContext(SkipperProfileContext);
  const loginContext = useContext(LoginContext);
  const [anchorEl, setAnchorEl] = React.useState(null);

  const handleClick = (event: any) => {
    setAnchorEl(anchorEl !== null ? null : event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const performLogout = () => {
    handleClose();
    window.localStorage.clear();
  }

  const goToSetting = () => {
    handleClose();
    history.push(CLIENT.SKIPPER.SETTINGS);
  }

  useEffect(() => {
    const getUserData = async () => {
      var userId = getUserId();
      if (userId !== null) {
        try {
          var skipperProfile = await skipperProfileContext.getSkipperById(userId);
          loginContext.setLoginData({
            token: getToken(),
            username: skipperProfile.email,
            id: userId,
            role: "Skipper"
          })
        } catch (e) { }
      }
    };
    getUserData();
    if (window.location.pathname === CLIENT.SKIPPER.DASHBOARD) setActiveTab(1);
    else if (window.location.pathname === CLIENT.SKIPPER.AVAILABILITY) setActiveTab(2);
    else if (window.location.pathname === CLIENT.SKIPPER.PROFILE) setActiveTab(3);
  }, []);
  return (
    <React.Fragment>
      <Grid style={{ padding: 10 }} container justify="space-between" alignItems="center">
        <Grid item xs={3} onClick={() => history.push(CLIENT.START_PAGE)}>
          <img
            className={styles.Logo}
            src={Logo}
            alt="logo"
          ></img>
        </Grid>
        <Grid container alignItems="center" justify="flex-end" item xs={9}>
          <Grid container item xs={12} md={4} alignItems="center" justify="space-around" >
            <Grid item className={activeTab === 1 ? styles.ActiveNavTab + " " + styles.NavTab : styles.NavTab}>
              <Link
                className={styles.Links}
                to={CLIENT.SKIPPER.DASHBOARD}
                onClick={() => setActiveTab(1)}
              >
                <span>Bookings</span>
              </Link>
            </Grid>
            <Grid item className={activeTab === 2 ? styles.ActiveNavTab + " " + styles.NavTab : styles.NavTab}>
              <Link
                className={styles.Links}
                to={CLIENT.SKIPPER.AVAILABILITY}
                onClick={() => setActiveTab(2)}
              >
                <span>Availability</span>
              </Link>
            </Grid>
            <Grid item className={activeTab === 3 ? styles.ActiveNavTab + " " + styles.NavTab : styles.NavTab}>
              <Link
                className={styles.Links}
                to={CLIENT.SKIPPER.PROFILE}
                onClick={() => setActiveTab(3)}
              >
                <span>Profile</span>
              </Link>
            </Grid>
            <Grid item className={styles.NavTab}>
              <div
                className={styles.Links}
                onClick={handleClick}
                style={{ cursor: "pointer" }}
              >
                <img
                  className={styles.Avatar}
                  src={skipperProfileContext.skipperData ? skipperProfileContext.skipperData.userPhotoUrl! : Photo}
                  alt="avatar"
                ></img>
              </div>
              <Menu
                id="simple-menu"
                anchorEl={anchorEl}
                keepMounted
                open={Boolean(anchorEl)}
                onClose={handleClose}
                className={styles.menuItem}
              >
                <MenuItem onClick={goToSetting}>Settings</MenuItem>
                <MenuItem onClick={performLogout}>Logout</MenuItem>
              </Menu>
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
            isSkipper() ? <Component {...props} setActiveTab={setActiveTab} /> : <Redirect to={CLIENT.APP.LOGIN} />
          }
        />
      </ContentLayout>
      <Footer history={history} />
    </React.Fragment>
  );
};

export default SkipperRoute;
