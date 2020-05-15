import React, { useState } from "react";
import TextInput from "../../ui/form/TextInput";
import { SkipperRegistrationStepData } from "../../../types/SkipperRegistrationContextProps";
import { LicenceEnum } from "./../../../helpers/enums/LicenceEnum";
import styles from "./styles.module.scss";
import Select from "react-select";
import mainStyles from "../../ui/main.module.scss";
import { Grid, FormControl, CardMedia } from "@material-ui/core";
import DatePicker from "react-date-picker";
import SkipperStepper from "../../shared/skipperStepper";

interface IProps {
  saveStep3: (values: any) => void;
  values: SkipperRegistrationStepData;
  goBack: () => void;
}

var fileInputRef: any;

export const SkipperRegistrationFormStep3: React.FC<IProps> = (props) => {
  const [dateOfIssue, setDateOfIssue] = useState<Date>(new Date());
  const [validUntilDate, setValidUntilDate] = useState<Date>(new Date());
  const [price, setPrice] = useState<number>(0);
  const [licenceName, setLicence] = useState({ label: "", value: "" });
  const [licencePDF, setLicencePDF] = useState({ data: { name: "" } });
  const [licenceImage, setLicenceImage] = useState({
    data: { name: "" },
    url: props.values ? props.values.licenceURL : "",
  });
  const [blob, setBlob] = useState<string>();

  const handleSubmit = (event: any) => {
    event.preventDefault();
    const saveData = {
      UserLicence: {
        dateOfIssue: dateOfIssue.toDateString(),
        validTo: validUntilDate.toDateString(),
        licenceType: licenceName.value,
        data: blob,
        name: licencePDF.data.name
          ? licencePDF.data.name
          : licenceImage.data.name,
      },
      licenceURL: licenceImage.url,
      price: price,
    };
    props.saveStep3(saveData);
  };

  const handleChange = (event: any) => {
    if (
      event.target.files[0] &&
      (event.target.files[0].type.endsWith("jpeg") ||
        event.target.files[0].type.endsWith("png") ||
        event.target.files[0].type.endsWith("jpeg"))
    ) {
      setLicenceImage({
        data: event.target.files[0],
        url: URL.createObjectURL(event.target.files[0]),
      });
      setLicencePDF({ data: { name: "" } });
    } else {
      setLicencePDF({ data: event.target.files[0] });
      setLicenceImage({ data: { name: "" }, url: "" });
    }
    const reader = new FileReader();
    if (event.target.files[0] instanceof Blob) {
      reader.readAsDataURL(event.target.files[0]);
      reader.onloadend = () => {
        setBlob(reader.result as string);
      };
    }
  };

  const setPriceFunction = (price: number) => {
    if (price <= 150) {
      setPrice(price);
    }
  };

  const handleLicenceNameChange = (event: any) => {
    setLicence(event);
  };

  const handleDateChange = (event: any, dateType: string) => {
    if (dateType === "DATE_OF_ISSUE") {
      setDateOfIssue(event);
    } else {
      setValidUntilDate(event);
    }
  };

  function getImageHolder() {
    if (licenceImage.url || (licencePDF.data && licencePDF.data.name)) {
      if (licenceImage.url) {
        return (
          <CardMedia
            component="img"
            src={licenceImage.url}
            alt=""
            className={styles.licenceHolder}
          />
        );
      } else {
        return <div className={styles.pdfFile}>{licencePDF.data.name}</div>;
      }
    } else {
      return (
        <div className={styles.licenceFile}>
          <span>Chose licence in .pdf or image format</span>
        </div>
      );
    }
  }

  return (
    <div>
      <span className={styles.headlineSmall}>Upload your files.</span>
      <form onSubmit={(e) => handleSubmit(e)}>
        <Grid container direction="row" spacing={4}>
          <Grid item xs={12} sm={6}>
            <span className={styles.pitchLine}>Skipper licence</span>

            <Grid container item spacing={4}>
              <Grid container justify="center" alignItems="center">
                {getImageHolder()}
              </Grid>
              <input
                type="file"
                id="photoURL"
                style={{ display: "none" }}
                ref={(fileInput) => (fileInputRef = fileInput)}
                onChange={(event) => handleChange(event)}
              />
              <Grid xs={12} item container direction="row" justify="center">
                <Grid item>
                  <button
                    type="button"
                    className={styles.uploadButton}
                    onClick={() => fileInputRef.click()}
                  >
                    Chose file
                  </button>
                </Grid>
              </Grid>
            </Grid>
          </Grid>
          <Grid item xs={12} sm={6}>
            <span className={styles.pitchLine}>Document details</span>
            <Grid container spacing={4}>
              <Grid
                item
                xs={12}
                sm={6}
                className={styles.formSelect}
                container
                direction="row"
              >
                <Grid item xs={12}>
                  <span style={{ display: "inline" }}> Date of issue</span>
                </Grid>
                <Grid item xs={12}>
                  {/* <DatePicker
                    className={styles.datePicker}
                    value={dateOfIssue}
                    clearIcon={null}
                    format={"dd.MM.yyyy."}
                    onChange={(e) => handleDateChange(e, "DATE_OF_ISSUE")}
                    locale="hr-HR"
                    name="Date of issue"
                  /> */}
                </Grid>
              </Grid>
              <Grid
                item
                xs={12}
                sm={6}
                className={styles.formSelect}
                container
                direction="row"
              >
                <Grid item xs={12}>
                  <span style={{ display: "inline" }}> Valid until</span>
                </Grid>
                <Grid item xs={12}>
                  {/* <DatePicker
                    className={styles.datePicker}
                    value={validUntilDate}
                    clearIcon={null}
                    format={"dd.MM.yyyy."}
                    onChange={(e) => handleDateChange(e, "VALID_UNTIL_DATE")}
                    locale="hr-HR"
                  /> */}
                </Grid>
              </Grid>
            </Grid>
            <Grid container spacing={4}>
              <Grid item xs={12}>
                <FormControl
                  className={styles.formSelect}
                  style={{ marginTop: 30 }}
                >
                  <Select
                    isClearable={true}
                    isSearchable={false}
                    isMulti={false}
                    placeholder="Select a licence..."
                    options={LicenceEnum}
                    onChange={(e) => handleLicenceNameChange(e)}
                  ></Select>
                </FormControl>
              </Grid>
            </Grid>
            <Grid container spacing={4}>
              <Grid item xs={12}>
                <span className={styles.sectionName}>Pricing</span>
              </Grid>
              <Grid item xs={12}>
                <span className={styles.description}>
                  Expected salary. Please be notified that the VAT will be
                  excluded from the amount.
                </span>
              </Grid>
              <Grid item container direction="row" spacing={2}>
                <Grid item xs={9} sm={6}>
                  <TextInput
                    handleChange={(e) =>
                      setPriceFunction((e.target.value as unknown) as number)
                    }
                    errors={[]}
                    id="price"
                    value={(price as unknown) as string}
                    type="number"
                    placeholder="Salary"
                  />
                </Grid>
                <Grid item xs={3} sm={6}>
                  <h5>€/day</h5>
                </Grid>
              </Grid>
            </Grid>
          </Grid>

          <Grid item xs={12} />
        </Grid>
        <SkipperStepper>
          <Grid
            container
            item
            xs={12}
            justify="flex-end"
            style={{ marginTop: 13 }}
          >
            <Grid item xs={6}>
              <button
                type="button"
                onClick={props.goBack}
                className={mainStyles.backButton}
                style={{ float: "left" }}
              >
                Back
              </button>
            </Grid>
            <Grid item xs={6}>
              <button type="submit" className={mainStyles.submitButton}>
                Next
              </button>
            </Grid>
          </Grid>
        </SkipperStepper>
      </form>
    </div>
  );
};

export default SkipperRegistrationFormStep3;
