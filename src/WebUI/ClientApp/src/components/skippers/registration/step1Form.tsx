import React from "react";
import { Formik } from "formik";
import * as Yup from "yup";
import TextInput from '../../ui/form/TextInput';
import { SkipperRegistrationStepData } from "../../../types/SkipperRegistrationContextProps";
import Grid from '@material-ui/core/Grid';
import styles from './styles.module.scss';
import mainStyles from '../../ui/main.module.scss';
import SkipperStepper from '../../shared/skipperStepper';
interface IProps {
  saveStep1: (values: any) => void,
  values: SkipperRegistrationStepData
}

export const SkipperRegistrationFormStep1: React.FC<IProps> = (props) => (

  <div>
    <span className={styles.headline}>Sign up to become a Skipper.</span>
    <span className={styles.pitchLine}>Great! We'll need some more information from you to get started.</span>
    <span className={styles.formName}>Personal Details</span>
    <Formik
      enableReinitialize={true}
      initialValues={{
        firstName: props.values ? props.values.firstName : "",
        lastName: props.values ? props.values.lastName : "",
        email: props.values ? props.values.email : "",
        tos: props.values ? props.values.tos : false,
        pp: props.values ? props.values.pp : false,
        password: props.values ? props.values.password : "",
        repeatPassword: props.values ? props.values.repeatPassword : ""
      }}
      onSubmit={(values: any) => props.saveStep1(values)}
      validationSchema={Yup.object().shape({
        firstName: Yup.string().required("Required"),
        lastName: Yup.string().required("Required"),
        email: Yup.string().email().required("Required"),
        tos: Yup.bool().oneOf([true], "Prihvatite TOS").required(),
        pp: Yup.bool().oneOf([true], "Prihvatite Pravila Privatnosti").required(),
        password: Yup.string().required("Required").min(8, 'Password is to short, 8 character minimum'),
        repeatPassword: Yup.string().test('password-match', 'Passwords do not match', function (value) {
          const { password } = this.parent;
          return password === value;
        })
      })}
    >
      {(props: any) => {
        const {
          touched,
          errors,
          isSubmitting,
          handleChange,
          handleBlur,
          handleSubmit
        } = props;
        return (
          <form onSubmit={handleSubmit}>
            <Grid container spacing={4} className={styles.firstFormRow}>
              <Grid item xs={12} sm={4} >
                <TextInput disabled={true} handleChange={handleChange} handleBlur={handleBlur} id='firstName' errors={errors} touched={touched}
                  value={props.values && props.values.firstName} placeholder="First Name" />
              </Grid>

              <Grid item xs={12} sm={4}>
                <TextInput disabled={true} handleChange={handleChange} handleBlur={handleBlur} id='lastName' errors={errors} touched={touched}
                  value={props.values && props.values.lastName} placeholder="Last Name" />
              </Grid>

              <Grid item xs={12} sm={4}></Grid>

            </Grid>

            <Grid container spacing={4}>
              <Grid item xs={12} sm={4}>
                <TextInput disabled={true} handleChange={handleChange} handleBlur={handleBlur} id='email' errors={errors} touched={touched}
                  value={props.values && props.values.email} placeholder="Email" />
              </Grid>

              <Grid item xs={12} sm={4}>
                <TextInput handleChange={handleChange} handleBlur={handleBlur} id='password' errors={errors} touched={touched}
                  value={props.values && props.values.password} placeholder="Password" type="password" />
              </Grid>

              <Grid item xs={12} sm={4}>
                <TextInput handleChange={handleChange} handleBlur={handleBlur} id='repeatPassword' errors={errors} touched={touched}
                  value={props.values && props.values.repeatPassword} placeholder="Repeat password" type="password" />
              </Grid>
              <Grid item xs={12} sm={12} style={{ marginBottom: -20 }}>
                <input type="checkbox" id='tos' checked={props.values.tos} onChange={handleChange} className={mainStyles.checkbox} />
                <span className={mainStyles.checkBoxLabel}>Prihvaćam uvjete korištenja</span>
                {errors['tos'] && touched['tos'] && <div className={mainStyles.checkboxAccept}>{errors['tos']}</div>}
              </Grid>
              <Grid item xs={12} sm={12}>
                <input type="checkbox" id='pp' checked={props.values.pp} onChange={handleChange} className={mainStyles.checkbox} />
                <span className={mainStyles.checkBoxLabel}>Prihvaćam pravila privatnosti</span>
                {errors['pp'] && touched['pp'] && <div className={mainStyles.checkboxAccept}>{errors['pp']}</div>}
              </Grid>
            </Grid>

            <SkipperStepper>
              <Grid item container justify="flex-end">
                <button type="submit" disabled={isSubmitting} style={{ marginTop: 15, marginBottom: 15 }} className={mainStyles.submitButton}>
                  Create a Profile
                  </button>
              </Grid>
            </SkipperStepper>
          </form>
        );
      }}
    </Formik >
  </div >
);

export default SkipperRegistrationFormStep1;