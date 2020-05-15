import React from "react";
import { TrustedSkipperProfile } from "../../../types/TrustedSkippers";
import Checkbox from '@material-ui/core/Checkbox';
import styles from './trustedSkippers.module.scss'
import Grid from '@material-ui/core/Grid';
import { withStyles } from "@material-ui/core";
import CheckBoxOutlinedIcon from '@material-ui/icons/CheckBoxOutlined';
import CheckBoxOutlineBlankOutlinedIcon from '@material-ui/icons/CheckBoxOutlineBlankOutlined';
import LicenceIcon from '../../../assets/img/icons/licence-icon-green-30.png'
import LanguageIcon from '../../../assets/img/icons/language-icon-green-32.png'

interface IProps {
  trustedSkipperProfile: TrustedSkipperProfile,
  updateSkipperSelected: (skipperSelectedId: string, checked: boolean) => void,
  checked: boolean,
  viewSkipperProfile: (skipperId: string) => void
}

const GreenCheckbox = withStyles({
  root: {
    color: "#26816A",
    '&$checked': {
      color: "#26816A",
    },
  },
  checked: {},
})((props: any) => <Checkbox color="default" {...props} />);

const TrustedSkippersProfileCard: React.FC<IProps> = (props: IProps) => {
  return (
    <>
      <Grid container item xs={12}>
        <Grid container item xs={12}>
          <Grid item xs={1}>
          </Grid>
          <Grid container item xs={10} direction="column" alignItems="center">
            <Grid item>
              <img
                className={styles.avatar}
                src={props.trustedSkipperProfile.imageURL}
                title="Skipper Profile"
                alt="" />
            </Grid>
            <Grid item>
              <span className={styles.cardProperty + " " + styles.skipperName}>
                {props.trustedSkipperProfile.firstName}
              </span>
            </Grid>
            <Grid container item direction="column" alignItems="flex-start" justify="center">
              <Grid item container direction="row" >
                <Grid item>
                  <img alt="" src={LicenceIcon} />
                </Grid>
                <Grid item>
                  <span className={styles.cardProperty + " " + styles.skipperYearOfExperience}>
                    {props.trustedSkipperProfile.yearOfFirstLicence}
                  </span>
                </Grid>
              </Grid>

              <Grid item container direction="row" >
                <Grid item xs={2}>
                  <img alt="" src={LanguageIcon} />
                </Grid>
                <Grid item xs={10} style={{ wordBreak: "break-word" }}>
                  {props.trustedSkipperProfile.listOfLanguages.map((lng, i) =>
                    <div key={i} className={styles.cardProperty + " " + styles.skipperLanguage}>{lng.label}{i < props.trustedSkipperProfile.listOfLanguages.length - 1 ? "," : ""}</div>
                  )}
                </Grid>
              </Grid>
            </Grid>
          </Grid>
          <Grid item xs={1}>
            <GreenCheckbox
              checked={props.checked}
              onChange={(event: any) => props.updateSkipperSelected(props.trustedSkipperProfile.id, event.target.checked)}
              value="primary"
              inputProps={{ 'aria-label': 'primary checkbox' }}
              size="medium"
              icon={<CheckBoxOutlineBlankOutlinedIcon />}
              checkedIcon={<CheckBoxOutlinedIcon />}
              className={styles.checkbox}
            />
          </Grid>
        </Grid>
        <Grid item container justify="center" alignItems="center">
          <button className={styles.viewProfileBtn} onClick={() => props.viewSkipperProfile(props.trustedSkipperProfile.id)}><span>View profile</span></button>
        </Grid>
      </Grid>
    </>
  );
};

export default TrustedSkippersProfileCard;
