import React, { ChangeEvent } from "react";
import { Formik, Form, Field } from 'formik';
import * as Yup from "yup";
import TextInput from '../../ui/form/TextInput';
import { SkipperRegistrationStepData } from "../../../types/SkipperRegistrationContextProps";
import { Grid, CardMedia, FormControl } from "@material-ui/core";
import { DatePickerComponent } from '@syncfusion/ej2-react-calendars';
import Select from 'react-select';
import styles from './styles.module.scss';
import mainStyles from '../../ui/main.module.scss';
import "@syncfusion/ej2-base/styles/material.css";
import "@syncfusion/ej2-buttons/styles/material.css";
import "@syncfusion/ej2-inputs/styles/material.css";
import "@syncfusion/ej2-popups/styles/material.css";
import "@syncfusion/ej2-react-calendars/styles/material.css";
import DatePicker from 'react-date-picker';
import SkipperStepper from '../../shared/skipperStepper';
import { ICountry } from "../../../types/ICountry";

interface IProps {
  saveStep2: (values: any) => void,
  values: SkipperRegistrationStepData,
  handleChange: (event: ChangeEvent<HTMLInputElement>) => void,
  countries: ICountry[]
  photoURL: any
}

var fileInputRef: any;



export const SkipperRegistrationFormStep2: React.FC<IProps> = (props: IProps) => {

  function SelectField(FieldProps: any) {
    return (
      <Select
        className={styles.autoCompleteSelect}
        value={FieldProps.country ? FieldProps.country.label : undefined}
        id="country"
        placeholder="Country"
        options={props.countries}
        {...FieldProps.field}
        onChange={option => FieldProps.form.setFieldValue(FieldProps.field.name, option)}
      />
    )
  }

  return (
    <div>
      <span className={styles.headlineSmall}>Other relevant info.</span>
      <Formik
        initialValues={{
          oib: props.values ? props.values.oib : "",
          dateOfBirth: props.values ? props.values.dateOfBirth : "",
          address: props.values ? props.values.address : "",
          zipCode: props.values ? props.values.zipCode : "",
          city: props.values ? props.values.city : "",
          country: props.values ? props.values.country : undefined,
          photoURL: props.photoURL ? props.photoURL : "",
          phoneNumber: props.values ? props.values.phoneNumber : ""
        }}
        onSubmit={(values: any) => props.saveStep2(values)}
        validationSchema={Yup.object().shape({
          oib: Yup.string().required("Required"),
          dateOfBirth: Yup.date().required("Required"),
          address: Yup.string().required("Required"),
          zipCode: Yup.string().required("Required"),
          city: Yup.string().required("Required"),
          country: Yup.string().required("Required"),
          phoneNumber: Yup.string().required("Required"),
          photoURL: Yup.string().required("Select picture")
        })}
      >
        {(formikProps: any) => {
          const {
            values,
            touched,
            errors,
            isSubmitting,
            handleChange,
            handleBlur,
            handleSubmit,
            setFieldValue
          } = formikProps;
          return (
            <Form onSubmit={handleSubmit}>
              <Grid container direction="row" spacing={6}>
                <Grid item xs={12} sm={4}>
                  <span className={styles.pitchLine} style={{ marginLeft: 15 }}>Profile picture</span>
                  <Grid container spacing={4}>
                    <Grid item xs={12} container justify="center">
                      <input type="file" id='photoURL' style={{ display: 'none' }} ref={fileInput => fileInputRef = fileInput} onChange={(event) => { handleChange(event); props.handleChange(event) }} />
                      {props.photoURL === "" ?
                        <div className={!props.photoURL && touched["photoURL"] && errors["photoURL"] ? styles.noPhotoHolder + " " + styles.error : styles.noPhotoHolder}>
                          <span>Browse photos from your computer in .jpg, .jpeg. and .png format.</span>
                        </div> :
                        <CardMedia
                          component="img"
                          src={props.photoURL}
                          alt=""
                          className={styles.photoHolder}
                        />}
                    </Grid>
                    {!props.photoURL && touched["photoURL"] && errors["photoURL"] &&
                      <Grid item xs={12} container justify="center" style={{ padding: 0 }}> <div className="input-feedback">Select picture</div></Grid>
                    }
                    <Grid xs={12} item container direction="row" justify="center">
                      <Grid item>
                        <button type="button" disabled={isSubmitting} className={styles.uploadButton} onClick={() => fileInputRef.click()}>
                          Upload
                    </button>
                      </Grid>
                    </Grid>
                  </Grid>
                </Grid>
                <Grid item xs={12} sm={8} >
                  <Grid container item xs={12} justify="center" >
                    <Grid item xs={12} sm={6} className={styles.spacing}>
                      <TextInput handleChange={handleChange} handleBlur={handleBlur} id='oib' errors={errors} touched={touched}
                        value={props.values && props.values.oib} placeholder="OIB" />
                    </Grid>
                    <Grid item xs={12} sm={6} className={styles.spacing}>
                      <div id="dateOfBirth" className={errors["dateOfBirth"] && touched["dateOfBirth"] ? styles.dateOfBirth + " " + styles.error : styles.dateOfBirth}>
                        <DatePickerComponent
                          placeholder="Date of birth"
                          id="datepicker"
                          className={errors["dateOfBirth"] && touched["dateOfBirth"] ? styles.error : "none"}
                          change={(value: any) => setFieldValue("dateOfBirth", value.value !== null ? value.value.toDateString() : "")}
                          showTodayButton={false}
                          firstDayOfWeek={1}
                          format={"dd.MM.yyyy."}
                          value={values.dateOfBirth === undefined || values.dateOfBirth === "" ? undefined : new Date(values.dateOfBirth)}
                        />
                        {/* <DatePicker
                          className={errors["dateOfBirth"] && touched["dateOfBirth"] ? styles.datePicker + " " + styles.error : styles.datePicker}
                          value={values.dateOfBirth === undefined || values.dateOfBirth === "" ? undefined : new Date(values.dateOfBirth)}
                          clearIcon={null}
                          format={"dd.MM.yyyy."}
                          onChange={(value: any) => setFieldValue("dateOfBirth", value.toDateString())}
                          locale="hr-HR"
                          name="Date of birth"
                        /> */}
                      </div>
                      {errors["dateOfBirth"] && touched["dateOfBirth"] && (
                        <div className="input-feedback">{errors["dateOfBirth"]}</div>
                      )}
                    </Grid>
                  </Grid>
                  <Grid container item xs={12} justify="center">
                    <Grid item xs={12} sm={6} className={styles.spacing}>
                      <TextInput handleChange={handleChange} handleBlur={handleBlur} id='address' errors={errors} touched={touched}
                        value={props.values && props.values.address} placeholder="Address" />
                    </Grid>
                    <Grid item xs={12} sm={6} className={styles.spacing}>
                      <TextInput handleChange={handleChange} handleBlur={handleBlur} id='zipCode' errors={errors} touched={touched}
                        value={props.values && props.values.zipCode} placeholder="Zip code" />
                    </Grid>
                  </Grid>
                  <Grid container item xs={12} justify="center">
                    <Grid item xs={12} sm={6} className={styles.spacing}>
                      <TextInput handleChange={handleChange} handleBlur={handleBlur} id='city' errors={errors} touched={touched}
                        value={props.values && props.values.city} placeholder="City" />
                    </Grid>
                    <Grid item xs={12} sm={6} className={styles.spacing}>
                      <FormControl className={errors["country"] && touched["country"] ? styles.formSelect + " " + styles.margin + " " + styles.error : styles.formSelect + " " + styles.margin}>
                        <Field name='country' options={props.countries} component={SelectField} />
                      </FormControl>
                      {errors["country"] && touched["country"] && (
                        <div className="input-feedback">{errors["country"]}</div>
                      )}
                    </Grid>
                  </Grid>
                  <Grid container item xs={12} justify="flex-start">
                    <Grid item xs={12} sm={6} className={styles.spacing}>
                      <TextInput handleChange={handleChange} handleBlur={handleBlur} id='phoneNumber' errors={errors} touched={touched}
                        value={props.values && props.values.phoneNumber} placeholder="Phone number" />
                    </Grid>
                  </Grid>
                </Grid>
                <SkipperStepper>
                  <Grid container justify="flex-end">
                    <Grid item>
                      <button type="submit" disabled={isSubmitting} className={mainStyles.submitButton} style={{ marginTop: 13, marginBottom: 15 }}>
                        Next
                  </button>
                    </Grid>
                  </Grid>
                </SkipperStepper>
              </Grid>
            </Form>
          );
        }}
      </Formik>
    </div >);
}

export default SkipperRegistrationFormStep2;