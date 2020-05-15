import React from "react";
import { Formik } from "formik";
import * as Yup from "yup";
import TextInput from '../../ui/form/TextInput';
import { SkipperPreRegistration } from "../../../types/SkipperRegistrationContextProps";
import Grid from '@material-ui/core/Grid';
import styles from './styles.module.scss';
import mainStyles from '../../ui/main.module.scss';
interface IProps {
  savePreregistration: (values: any) => void,
  values?: SkipperPreRegistration
}

export const SkipperPreRegistrationForm: React.FC<IProps> = (props) => (
  <div className={styles.wrapper}>
    <span className={styles.headline}>Enter Skipper data to invite them to sign up!</span>
    <span className={styles.pitchLine}>Great! We'll need some more information from you to get started.</span>
    <span className={styles.formName}>Personal Details</span>
    <Formik
      initialValues={{
        firstName: props.values ? props.values.firstName : "",
        lastName: props.values ? props.values.lastName : "",
        email: props.values ? props.values.email : "",
      }}
      onSubmit={(values: any) => props.savePreregistration(values)}
      validationSchema={Yup.object().shape({
        firstName: Yup.string().required("Required"),
        lastName: Yup.string().required("Required"),
        email: Yup.string().email().required("Required"),
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
                <TextInput handleChange={handleChange} handleBlur={handleBlur} id='firstName' errors={errors} touched={touched}
                  value={props.values && props.values.firstName} placeholder="First Name" />
              </Grid>

              <Grid item xs={12} sm={4}>
                <TextInput handleChange={handleChange} handleBlur={handleBlur} id='lastName' errors={errors} touched={touched}
                  value={props.values && props.values.lastName} placeholder="Last Name" />
              </Grid>

              <Grid item xs={12} sm={4}></Grid>

            </Grid>

            <Grid container spacing={4}>
              <Grid item xs={12} sm={4}>
                <TextInput handleChange={handleChange} handleBlur={handleBlur} id='email' errors={errors} touched={touched}
                  value={props.values && props.values.email} placeholder="Email" />
              </Grid>
            </Grid>

            <button type="submit" disabled={isSubmitting} style={{ marginTop: 15, marginBottom: 15 }} className={mainStyles.submitButton}>
              Create a Profile
            </button>

          </form>
        );
      }}
    </Formik>
  </div>
);

export default SkipperPreRegistrationForm;