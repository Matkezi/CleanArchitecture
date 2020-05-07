import React from 'react';
import { Grid } from "@material-ui/core";
import Button from '@material-ui/core/Button';
import { Link } from 'react-router-dom'
import styles from '../styles.module.scss';

// a basic form
const CallToAction = () => {

  return (
    <Grid container xs={12} sm={12} lg={6}>
    <Grid item xs={12} sm={3}>
        You are a
        </Grid>
    <Grid item xs={12} sm={4}>
        <Link className={styles.button} to="/skipper/registration/step=1">Skipper</Link>
    </Grid>
    <Grid item xs={12} sm={5}>
        <Link className={styles.button + ' ' + styles.green} to="/login">Charter Agency</Link>
    </Grid>
</Grid>
  );
};

export default CallToAction;