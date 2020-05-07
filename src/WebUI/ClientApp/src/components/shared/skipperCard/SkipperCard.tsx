import { Skipper } from "../../../types/Skipper";
import React from "react";
import { Grid, Chip } from "@material-ui/core";
import styles from "./styles.module.scss";
import YearsOfSailing from '../../../assets/img/icons/years-of-sailing-icon-blue-34.png';
import LanguageIcon from '../../../assets/img/icons/language-icon-blue-34.png';

export interface IProps {
  skipper: Skipper;
  requestSkipper: (skipper: Skipper) => void;
  viewSkipperProfile?: (skipperId: string) => void;
}

const SkipperCard: React.FC<IProps> = (props: IProps) => {
  return (
    <React.Fragment>
      <Grid container direction="row" className={styles.skipperCard}>
        <Grid item xs={12} md={3} container justify="center" alignItems="center" className={styles.photoDesktop}>
          <img src={props.skipper.userPhotoUrl} className={styles.skipperimg} alt=""></img>
        </Grid>
        <Grid item xs={12} md={6} container direction="row">
          <Grid item xs={12} container>
            <Grid item xs={7} justify="center" container className={styles.photoMobile}>
              <img src={props.skipper.userPhotoUrl} className={styles.skipperimg} alt=""></img>
            </Grid>
            <Grid item container xs={5} md={12} justify="flex-start" alignItems="center">
              <p className={styles.name}>{props.skipper.firstName}</p><button className={styles.viewProfile} onClick={() => props.viewSkipperProfile!(props.skipper.id)}><span>View profile</span></button>
            </Grid>
            <Grid item xs={12} container>
              <Grid item xs={11} md={8} container>
                <Grid item xs={2}>
                  <img alt="lng" style={{ marginBottom: 5 }} src={LanguageIcon} height={34} width={34} />
                </Grid>
                <Grid item xs={8}>
                  {props.skipper.listOfLanguages.map((language, i) => (
                    <p key={i} className={styles.lng}>{language.label}{i < props.skipper.listOfLanguages.length - 1 ? ",  " : ""}</p>))}
                </Grid>
              </Grid>
              <Grid item container xs={11} md={4}>
                <Grid item xs={2}>
                  <img alt="" src={YearsOfSailing} height={34} width={34} />
                </Grid>
                <Grid item xs={8} container alignItems="flex-start">
                  <p className={styles.years}>{(new Date(Date.now()).getFullYear() - props.skipper.yearOfFirstLicence) 
                  === 1 ? <>1 year</> : <>(new Date(Date.now()).getFullYear() - props.skipper.yearOfFirstLicence).toString() years</>}</p>
                </Grid>
              </Grid>
            </Grid>
            <Grid item xs={12}>
              {props.skipper.listOfSkills.map(skill =>
                <Chip icon={<i className={skill.icon}></i>} className={styles.chip} style={{ backgroundColor: "#4f5b8b" }} key={skill.name} label={skill.name} />)}
            </Grid>
          </Grid>
        </Grid>
        <Grid item container xs={12} md={3} className={styles.btnAndPrice}>
          <Grid item container xs={7} md={12} justify="flex-end" alignItems="center" className={styles.btnCnt}>
            <button className={styles.reqBtn} onClick={() => props.requestSkipper(props.skipper)}><span>Request this skipper</span></button>
          </Grid>
          <Grid item container xs={5} md={12} justify="center">
            <span className={styles.price}> {props.skipper.price} </span><span className={styles.valute}> €/day</span>
          </Grid>
        </Grid>
      </Grid>
    </React.Fragment>
  );
};

export default SkipperCard;
