import React from "react";
import { Switch, Route, Router } from "react-router-dom";
import { createBrowserHistory } from "history";
import Store from "./Store";
import LandingPage from "./components/publicWebPage/landingPage";
import SkipperRegistration from "./containers/skippers/skipperRegistration";
import TrustedSkippers from "./containers/charters/trustedSkippers/trustedSkippers";
import SkipperProfile from "./containers/skippers/skipperProfile";
import LoadingConsumer from "./consumers/loadingConsumer";
import SnackbarConsumer from "./consumers/snackbarConsumer";
import SkipperLogin from "./containers/public/login/login";
import SkipperBooking from "./containers/skippers/skipperBooking";
import CharterDashboard from "./containers/charters/dashboard/charterDashboard";
import GuestBookingContainer from "./containers/guests/guestBookingContainer";
import CharterRoute from "./containers/routing/CharterRoute";
import SkipperRoute from "./containers/routing/SkipperRoute";
import GuestRoute from './containers/routing/GuestRoute';
import LoginRoute from './containers/routing/LoginRoute';
import DocumentRoute from './containers/routing/DocumentsRoute';
import SkipperAvalibility from "./containers/skippers/skipperAvaliability";
import SkipperRegisterRoute from './containers/routing/SkipperRegisterRoute';
import SkipperPreregistration from './containers/charters/skipperPreregistration'
import ForgottenPassword from './containers/public/forgottenPassword';
import ForgottenPassReqContainer from './containers/public/forgottenPassReqContainer';
import SkipperSettings from './containers/skippers/settings';
import ChangeEmail from './containers/skippers/changeEmail';
import AboutPage from './components/publicWebPage/aboutPage';
import GuestTos from '././components/shared/tos&pp/guestTos';
import SkipperTos from './components/shared/tos&pp/skipperTos';
import CharterTos from './components/shared/tos&pp/charterTos';
import PrivacyPolicy from './components/shared/tos&pp/privacyPolicy';
import CharterSettings from './containers/charters/charterSettings';
import BoatContainer from './containers/charters/boats';
import { CLIENT } from './constants/clientRoutes';

const history = createBrowserHistory();

const App = () => {
  return (
    <Router history={history}>
      <Store>
        <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" />
        <link href="//cdn-images.mailchimp.com/embedcode/horizontal-slim-10_7.css" rel="stylesheet" type="text/css" />
        <LoadingConsumer />
        <SnackbarConsumer />
        <Switch>
          <Route exact path={CLIENT.START_PAGE} component={LandingPage} />
          <Route exact path={CLIENT.PUBLIC.ABOUT} component={AboutPage} />
          <LoginRoute history={history} exact path={CLIENT.APP.LOGIN} component={SkipperLogin} secondaryColor={true} />
          <LoginRoute history={history} exact path={CLIENT.APP.FORGOTTEN_PASSWORD} component={ForgottenPassReqContainer} />
          <LoginRoute history={history} exact path={CLIENT.APP.PASSWORD_RESET()} component={ForgottenPassword} />
          <LoginRoute history={history} exact path={CLIENT.APP.CHANGE_EMAIL()} component={ChangeEmail} />
          <CharterRoute history={history} exact path={CLIENT.CHARTER.TRUSTED_SKIPPERS} component={TrustedSkippers} />
          <CharterRoute history={history} exact path={CLIENT.CHARTER.SETTINGS} component={CharterSettings} />
          <CharterRoute history={history} exact path={CLIENT.CHARTER.BOATS} component={BoatContainer} />
          <CharterRoute history={history} exact path={CLIENT.CHARTER.DASHBOARD} component={CharterDashboard} />
          <CharterRoute history={history} exact path={CLIENT.CHARTER.SKIPPER_PROFILE()} component={SkipperProfile} />
          <CharterRoute history={history} exact path={CLIENT.CHARTER.SKIPPER_PREREGISTRATION} component={SkipperPreregistration} />
          <SkipperRegisterRoute history={history} exact path={CLIENT.SKIPPER.REGISTRATION()} component={SkipperRegistration} />
          <SkipperRoute history={history} exact path={CLIENT.SKIPPER.PROFILE} component={SkipperProfile} />
          <SkipperRoute history={history} exact path={CLIENT.SKIPPER.SETTINGS} component={SkipperSettings} />
          <SkipperRoute history={history} exact path={CLIENT.SKIPPER.AVAILABILITY} component={SkipperAvalibility} />
          <SkipperRoute history={history} exact path={CLIENT.SKIPPER.DASHBOARD} component={SkipperBooking} />
          <GuestRoute history={history} exact path={CLIENT.GUEST.BOOKING()} component={GuestBookingContainer} />
          <GuestRoute history={history} exact path={CLIENT.GUEST.SKIPPER_PROFILE()} component={SkipperProfile} />
          <DocumentRoute history={history} exact path={CLIENT.PUBLIC.TOS.GUEST} component={GuestTos} />
          <DocumentRoute history={history} exact path={CLIENT.PUBLIC.TOS.SKIPPER} component={SkipperTos} />
          <DocumentRoute history={history} exact path={CLIENT.PUBLIC.TOS.CHARTER} component={CharterTos} />
          <DocumentRoute history={history} exact path={CLIENT.PUBLIC.PRIVACY_POLICY} component={PrivacyPolicy} />
        </Switch>
      </Store>
    </Router>
  );
};

export default App;
