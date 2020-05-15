import React from 'react';
import { Grid } from "@material-ui/core";
import Button from '@material-ui/core/Button';
import styles from './styles.module.scss';

// a basic form
const SubscribeForm = ({ status, message, onValidated }: any) => {
  let email: any;

  const submit = () =>
    email &&
    email.value.indexOf("@") > -1 &&
    onValidated({
      EMAIL: email.value,
    });

  return (
    <React.Fragment>
      <Grid container item justify="space-around" alignItems="center">
        <Grid item md={7} xs={12} container justify="center">
          <input
            style={{ marginRight: 15 }}
            className={styles.inputForm}
            ref={node => (email = node)}
            type="email"
            placeholder="Your e-mail"
          />
        </Grid>
        <Grid item md={5} xs={12} container justify="center">
          <button className={styles.sendEmailButton} onClick={submit}>Request access</button>
        </Grid>

        {status === "sending" && <div className={styles.infoBox}>Sending request...</div>}
        {status === "error" && (<div className={styles.infoBox} dangerouslySetInnerHTML={{ __html: message }} />)}
        {status === "success" && (<div className={styles.infoBox} dangerouslySetInnerHTML={{ __html: message }} />)}

        <br />
      </Grid>
    </React.Fragment>
  );
};

export default SubscribeForm;