import React, { useState, useContext, useEffect } from "react";
import { Grid, Divider, Menu, MenuItem } from "@material-ui/core";
import styles from "./routes.module.scss";
import { Route, Redirect, Link } from "react-router-dom";
import ContentLayout from "./ContentLayout";
import { isCharter, getToken } from "../../services/appService/authorizationService";
import { CharterContext } from '../../providers/charter/charter';
import Logo from '../../assets/img/icons/logo-green.png';
import CharterProfileIcon from '../../assets/img/icons/charter-profile-icon.png';
import Footer from "./Footer";
import { LoginContext } from "../../providers/login";
import { getUserId } from '../../services/appService/authorizationService';

const CharterRoute = ({ component: Component, history, ...rest }: any) => {
  const [activeTab, setActiveTab] = useState<number>(1);
  const [anchorEl, setAnchorEl] = React.useState(null);
  const charterContext = useContext(CharterContext);
  const loginContext = useContext(LoginContext);

  const handleClick = (event: any) => {
    setAnchorEl(anchorEl !== null ? null : event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const goToSetting = () => {
    handleClose();
    history.push("/charter/settings")
  }

  const performLogout = () => {
    setAnchorEl(null);
    window.localStorage.clear();
  }

  useEffect(() => {
    const getUserData = async () => {
      var userId = getUserId();
      if (userId !== null) {
        var charterData = await charterContext.getCharterById(userId);
        loginContext.setLoginData({
          token: getToken(),
          username: charterData.email,
          id: userId,
          role: "Charter"
        })
      }
    };
    getUserData();
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
              to="/charter/dashboard"
              onClick={() => setActiveTab(1)}
            >
              <span>Bookings</span>
            </Link>
          </Grid>
          <Grid item className={activeTab === 2 ? styles.ActiveNavTab + " " + styles.NavTab : styles.NavTab}>
            <Link
              className={styles.Links}
              to="/charter/trusted-skippers"
              onClick={() => setActiveTab(2)}
            >
              <span>Skippers</span>
            </Link>
          </Grid>
          <Grid item className={activeTab === 3 ? styles.ActiveNavTab + " " + styles.NavTab : styles.NavTab}>
            <Link
              className={styles.Links}
              to="/charter/boats"
              onClick={() => setActiveTab(3)}
            >
              <span>Boats</span>
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
                src={CharterProfileIcon}
                height={45}
                width={45}
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
        <Grid item xs={12}>
          <Divider />
        </Grid>
      </Grid>
      <ContentLayout>
        <Route
          {...rest}
          render={(props: any) =>
            isCharter() ? <Component {...props} setActiveTab={setActiveTab} /> : <Redirect to="/login" />
          }
        />
      </ContentLayout>
      <Footer history={history} />
    </React.Fragment>
  );
};

export default CharterRoute;
