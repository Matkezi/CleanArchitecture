import React, { ChangeEvent, useState } from 'react';
import { LicenceEnum } from '../../../helpers/enums/LicenceEnum'
import { BoatTypeEnum } from '../../../helpers/enums/BoatEnum';
import { Grid, FormControl, CardMedia } from '@material-ui/core';
import * as Yup from "yup";
import PlusIcon from '../../../assets/img/icons/plus-icon-green-37.png';
import styles from './styles.module.scss';
import { Formik, Field } from 'formik';
import Select from 'react-select';
import TextInput from "../../ui/form/TextInput";
import { Boat } from '../../../types/Boat';

interface IProps {
    closeForm: () => void,
    saveBoat: (boat: Boat) => void,
    updateBoat?: (id: number, boat: Boat) => void,
    handleChange: (event: ChangeEvent<HTMLInputElement>) => void,
    photoURL: any,
    title: string,
    showIcon: boolean,
    data?: Boat
}

var fileInputRef: any;

const NewBoatForm: React.FC<IProps> = (props2: IProps) => {

    const [photo, setPhoto] = useState({
        photoData: { name: "" },
        photoURL: props2.data ? props2.data.boatPhotoUrl : "",
        readerData: ""
    });

    const handlePhotoChange = (event: any) => {
        event.persist();
        const reader = new FileReader();
        if (event.target.files[0] instanceof Blob) {
            reader.readAsDataURL(event.target.files[0]);
            reader.onloadend = () => {
                setPhoto({ photoData: event.target.files[0], photoURL: URL.createObjectURL(event.target.files[0]), readerData: reader.result as string });
            }
        }
    }

    function SelectBoatType(FieldProps: any) {
        return (
            <Select
                className={styles.autoCompleteSelect}
                value={FieldProps.type ? FieldProps.type.label : undefined}
                id="type"
                placeholder="Boat Type"
                options={BoatTypeEnum}
                {...FieldProps.field}
                onChange={option => FieldProps.form.setFieldValue(FieldProps.field.name, option)}
            />
        )
    }

    function SelectLicenceType(FieldProps: any) {
        return (
            <Select
                className={styles.autoCompleteSelect}
                value={FieldProps.minimalRequiredLicence ? FieldProps.minimalRequiredLicence.label : undefined}
                id="minimalRequiredLicence"
                placeholder="Minimal Required Licence"
                options={LicenceEnum}
                {...FieldProps.field}
                onChange={option => FieldProps.form.setFieldValue(FieldProps.field.name, option)}
            />
        )
    }

    return (
        <Grid container>
            {props2.showIcon &&
                <Grid item container xs={12}>
                    <Grid item container xs={11} className={styles.newBoatTitle}>
                        <span>{props2.title}</span>
                    </Grid>
                    <Grid item container xs={1} justify="flex-end">
                        <img alt="" onClick={() => props2.closeForm()} src={PlusIcon} className={styles.plusIcon + " " + styles.rotate} />
                    </Grid>
                </Grid>
            }
            <Grid item container xs={12} className={styles.formContainer} justify="center">
                <Grid item xs={12} className={styles.infoTitle}>
                    <span>Boat Information</span>
                </Grid>
                <Grid item xs={12} className={styles.form}>
                    <Formik
                        initialValues={{
                            name: props2.data ? props2.data.name : "",
                            manufacturer: props2.data ? props2.data.manufacturer : "",
                            model: props2.data ? props2.data.model : "",
                            type: props2.data ? props2.data.type : undefined,
                            length: props2.data ? props2.data.length : undefined,
                            minimalRequiredLicence: props2.data ? props2.data.minimalRequiredLicence : undefined,
                            boatPhotoUrl: props2.data ? props2.data.boatPhotoUrl : photo.photoURL,
                            charterId: ""
                        }}
                        onSubmit={(values: any) => !props2.showIcon ? props2.updateBoat!(props2.data!.id, values) : props2.saveBoat(values)}
                        validationSchema={Yup.object().shape({
                            name: Yup.string().required("Required"),
                            manufacturer: Yup.string().required("Required"),
                            model: Yup.string().required("Required"),
                            length: Yup.number().required("Required"),
                            type: Yup.string().required("Required"),
                            minimalRequiredLicence: Yup.string().required("Required"),
                            boatPhotoUrl: Yup.string().required("Required")
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
                                            <TextInput handleChange={handleChange} handleBlur={handleBlur} id='name' errors={errors} touched={touched}
                                                value={props.values && props.values.name} placeholder="Name" />
                                        </Grid>
                                        <Grid item xs={12} sm={4}>
                                            <TextInput handleChange={handleChange} handleBlur={handleBlur} id='manufacturer' errors={errors} touched={touched}
                                                value={props.values && props.values.manufacturer} placeholder="Manufacturer" />
                                        </Grid>
                                        <Grid item xs={12} sm={4}>
                                            <TextInput handleChange={handleChange} handleBlur={handleBlur} id='model' errors={errors} touched={touched}
                                                value={props.values && props.values.model} placeholder="Model" />
                                        </Grid>
                                    </Grid>

                                    <Grid container spacing={4}>
                                        <Grid item xs={12} sm={4}>
                                            <TextInput handleChange={handleChange} handleBlur={handleBlur} id='length' errors={errors} touched={touched}
                                                value={props.values && props.values.length} placeholder="Length" type="number" />
                                        </Grid>
                                        <Grid item xs={12} sm={4}>
                                            <FormControl className={errors["type"] && touched["type"] ? styles.formSelect + " " + styles.margin + " " + styles.error : styles.formSelect + " " + styles.margin}>
                                                <Field name='type' component={SelectBoatType} />
                                            </FormControl>
                                            {errors["type"] && touched["type"] && (
                                                <div className="input-feedback">{errors["type"]}</div>
                                            )}
                                        </Grid>
                                        <Grid item xs={12} sm={4}>
                                            <FormControl className={errors["minimalRequiredLicence"] && touched["minimalRequiredLicence"] ? styles.formSelect + " " + styles.margin + " " + styles.error : styles.formSelect + " " + styles.margin}>
                                                <Field name='minimalRequiredLicence' component={SelectLicenceType} />
                                            </FormControl>
                                            {errors["minimalRequiredLicence"] && touched["minimalRequiredLicence"] && (
                                                <div className="input-feedback">{errors["minimalRequiredLicence"]}</div>
                                            )}
                                        </Grid>
                                    </Grid>
                                    <Grid container spacing={4}>
                                        <Grid item xs={12}>
                                            <Grid item xs={12} container justify="center">
                                                <input type="file" id='boatPhotoUrl' style={{ display: 'none' }} ref={fileInput => fileInputRef = fileInput} onChange={(event) => { handleChange("boatPhotoUrl")(event.target.files![0].name); handlePhotoChange(event); }} />
                                                {photo.photoURL === "" ?
                                                    <div className={!props.values.boatPhotoUrl && touched["boatPhotoUrl"] && errors["boatPhotoUrl"] ? styles.noPhotoHolder + " " + styles.error : styles.noPhotoHolder}>
                                                        <div>Browse photos from your computer in .jpg, .jpeg. and .png format.</div>
                                                    </div> :
                                                    <CardMedia
                                                        component="img"
                                                        src={photo.photoURL}
                                                        alt=""
                                                        className={styles.photoHolder}
                                                    />}
                                            </Grid>
                                            {!props.values.boatPhotoUrl && touched["boatPhotoUrl"] && errors["boatPhotoUrl"] &&
                                                <Grid item xs={12} container justify="center" style={{ padding: 0 }}> <div className="input-feedback">{errors["boatPhotoUrl"]}</div></Grid>
                                            }
                                            <Grid xs={12} item container direction="row" justify="center">
                                                <Grid item>
                                                    <button type="button" disabled={isSubmitting} className={styles.uploadButton} onClick={() => fileInputRef.click()}>
                                                        <span>Select</span>
                                                    </button>
                                                </Grid>
                                            </Grid>
                                        </Grid>
                                    </Grid>
                                    <Grid container justify="flex-end">
                                        {!props2.showIcon &&
                                            <Grid item>
                                                <button disabled={isSubmitting} onClick={() => props2.closeForm()} className={styles.cancleBtn}>
                                                    <span>Cancel</span>
                                                </button>
                                            </Grid>
                                        }
                                        <Grid item>
                                            <button type="submit" disabled={isSubmitting} className={styles.saveBtn}>
                                                <span>{!props2.showIcon ? "Update" : "Save"}</span>
                                            </button>
                                        </Grid>
                                    </Grid>
                                </form>
                            );
                        }}
                    </Formik >
                </Grid>
            </Grid>
        </Grid >
    )
}

export default NewBoatForm;