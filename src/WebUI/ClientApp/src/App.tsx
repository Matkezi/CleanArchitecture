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
          <Route exact path="/" component={LandingPage} />
          <Route exact path="/public/about" component={AboutPage} />
          <LoginRoute history={history} exact path="/login" component={SkipperLogin} secondaryColor={true} />
          <LoginRoute history={history} exact path="/login/forgotten-password" component={ForgottenPassReqContainer} />
          <LoginRoute history={history} exact path="/password-reset/email=:email/token=:token" component={ForgottenPassword} />
          <LoginRoute history={history} exact path="/change-email/email=:email/newEmail=:newEmail/token=:token" component={ChangeEmail} />
          <CharterRoute history={history} exact path="/charter/trusted-skippers" component={TrustedSkippers} />
          <CharterRoute history={history} exact path="/charter/settings" component={CharterSettings} />
          <CharterRoute history={history} exact path="/charter/boats" component={BoatContainer} />
          <CharterRoute history={history} exact path="/charter/dashboard" component={CharterDashboard} />
          <CharterRoute history={history} exact path="/charter/skipper-profile/:skipperId" component={SkipperProfile} />
          <CharterRoute history={history} exact path="/charter/preregistration" component={SkipperPreregistration} />
          <SkipperRegisterRoute history={history} exact path="/skipper/registration/:url?/:stepNumber?" component={SkipperRegistration} />
          <SkipperRoute history={history} exact path="/skipper/profile" component={SkipperProfile} />
          <SkipperRoute history={history} exact path="/skipper/settings" component={SkipperSettings} />
          <SkipperRoute history={history} exact path="/skipper/availability" component={SkipperAvalibility} />
          <SkipperRoute history={history} exact path="/skipper/dashboard" component={SkipperBooking} />
          <GuestRoute history={history} exact path="/guest/booking/:bookingUrl/step:stepNumber?" component={GuestBookingContainer} />
          <GuestRoute history={history} exact path="/guest/skipper-profile/:skipperId" component={SkipperProfile} />
          <DocumentRoute history={history} exact path="/public/tos-guest" component={GuestTos} />
          <DocumentRoute history={history} exact path="/public/tos-skipper" component={SkipperTos} />
          <DocumentRoute history={history} exact path="/public/tos-charter" component={CharterTos} />
          <DocumentRoute history={history} exact path="/public/privacy-policy" component={PrivacyPolicy} />
        </Switch>
      </Store>
    </Router>
  );
};

export default App;
