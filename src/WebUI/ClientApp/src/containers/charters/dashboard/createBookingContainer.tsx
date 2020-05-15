import React, { useState, useContext, useEffect } from "react";
import { Button, Divider, Grid, FormControl } from "@material-ui/core";
import { EditBookingContext } from "../../../providers/booking/editBookingContext";
import BoatRow from "../../../components/shared/boatRow/BoatRow";
import EditGuest from "../../../components/charter/editGuest/EditGuest";
import { BoatContext } from "../../../providers/boats/boatsContext";
import { Boat } from "../../../types/Boat";
import styles from "./styles.module.scss";
import Helmet from "react-helmet";
import DayPicker, { DateUtils, RangeModifier } from "react-day-picker";
import { dateHelper } from "../../../helpers/dateHelper";
import CloseIcon from "../../../assets/img/icons/x-icon-green-34.png";
import { Element } from "react-scroll";
import { ICountry } from "../../../types/ICountry";

export interface IProps {
  saveBooking: Function;
  countries: ICountry[]
}

interface IBoatType {
  value: string;
  label: string;
}

const CreateBookingContainer: React.FC<IProps> = (props: IProps) => {
  const editBookingContext = useContext(EditBookingContext);
  const charterBoatsContext = useContext(BoatContext);
  const [boatSearch, setBoatSearch] = useState<string>("");
  const [date, setDate] = useState<RangeModifier>();
  const [enteredTo, setEnteredTo] = useState<Date>();
  const [availableStartDays, setAvailableStartDates] = useState<Date[]>([]);
  const [availableEndDays, setAvailableEndDates] = useState<Date[]>([]);

  const isFirstDayOfWeekOrMonth = (day: Date): boolean => {
    return day.getDay() === 0 || day.getDate() === 1;
  }

  const isLastDayOfWeekOrMonth = (day: Date): boolean => {
    return day.getDay() === 6 || (new Date(day.getTime() + 86400000).getDate() === 1);
  }

  useEffect(() => {
    setAvaliableBoats();
    if (editBookingContext.booking.id !== undefined) {
      var from = new Date(editBookingContext.booking?.bookedFrom!);
      var to = new Date(editBookingContext.booking?.bookedTo!);
      setDate({
        ...date,
        from: new Date(from.getFullYear(), from.getMonth(), from.getDate()),
        to: new Date(to.getFullYear(), to.getMonth(), to.getDate())
      });
    }
    editBookingContext.setBooking({
      ...editBookingContext.booking,
      onboardingLocation: "Marina Mandalina - Šibenik"
    })
  }, []);

  const closeEditBooking = () => {
    console.log("Clikcked")
    editBookingContext.setBooking({});
    editBookingContext.setShowEditBooking(false);
    editBookingContext.setShowNewBooking(false);
}

  const handleDayClick = (day: Date, { selected, disabled }: any) => {
    if (selected && date?.from!) { handleResetClick(); }
    else {
      const range = DateUtils.addDayToRange(day, date!);
      var dates = getDates(range.from, range.to);
      var tempArray1: Date[] = [];
      var tempArray2: Date[] = [];
      tempArray1.push(new Date(range.from));
      tempArray2.push(new Date(range.to));
      dates.map((day: Date, i) => {
        if (isFirstDayOfWeekOrMonth(day)) {
          tempArray1.push(day);
        }
        else if (isLastDayOfWeekOrMonth(day)) {
          tempArray2.push(day);
        }
      })
      setAvailableStartDates([...tempArray1]);
      setAvailableEndDates([...tempArray2]);
      setDate(range);
      setEnteredTo(day);
      editBookingContext.setBooking({
        ...editBookingContext.booking,
        BookedTo: range.to,
        BookedFrom: range.from
      });
    }
  };

  function getDates(startDate: Date, stopDate: Date): Date[] {
    var dateArray = new Array();
    var currentDate = new Date(startDate).getTime();
    var stpDae = new Date(stopDate).getTime();
    while (currentDate <= stpDae) {
      dateArray.push(new Date(currentDate));
      currentDate = currentDate + 24 * 60 * 60 * 1000;
    }
    return dateArray;
  }

  const handleResetClick = () => {
    setDate({ from: undefined!, to: undefined! });
    setEnteredTo(undefined);
  };

  const setAvaliableBoats = (type?: string) => {
    charterBoatsContext.setCharterBoats(type);
  };

  const choseBoat = (boat: Boat) => {
    editBookingContext.setBooking({
      ...editBookingContext.booking,
      boat: boat,
      boatId: boat.id
    });
  };
  const deselectBoat = () => {
    editBookingContext.setBooking({
      ...editBookingContext.booking,
      boat: undefined,
      boatId: undefined
    });
    setBoatSearch("");
  };

  const handleDayMouseEnter = (day: Date) => {
    if (date?.from! !== undefined && (date?.to! === null || date?.to! === undefined)) {
      var dates = getDates(date?.from!, day);
      var tempArray1: Date[] = [];
      var tempArray2: Date[] = [];
      dates.map((day: Date, i) => {
        if (isFirstDayOfWeekOrMonth(day)) {
          tempArray1.push(day);
        }
        else if (isLastDayOfWeekOrMonth(day)) {
          tempArray2.push(day);
        }
      })
      setAvailableStartDates([...tempArray1]);
      setAvailableEndDates([...tempArray2]);
      setEnteredTo(day);
    }
  };

  const modifiers = {
    highlighted: { from: date?.from!, to: enteredTo! },
    first: date?.from,
    last: enteredTo!,
    selected: date,
    availableStartDays: availableStartDays,
    availableEndDays: availableEndDays
  };

  const calendarStyle = `
  .DayPicker-Day--highlighted div {  
    background-color: #3a9882;
    height: fit-content !important;
    padding: 8px;
    vertical-align: middle;
    text-align: center;
  }
  
  .DayPicker-Day--highlighted:not(.DayPicker-Day--outside) {
    color: white;
    background-color: white !important;
    border-radius: 0px !important;
    vertical-align: middle;
    text-align: center;
  }
  


.DayPicker-Day--first div:not(.DayPicker-Day--outside) {
    background-color: #26806b !important;
    color: white;
    border-radius: 0px !important;
    border-top-left-radius: 50% !important;
    border-bottom-left-radius: 50% !important;
    outline: none;
}

.DayPicker-Day {
  padding-left: 0px !important;
  padding-right: 0px !important;
  padding-bottom: 0px;
  padding-top: 0px;
  height: 43px;
  outline: none !important;
}

.DayPicker-Day div {
  height: fit-content !important;
  padding: 8px;
  vertical-align: middle;
  text-align: center;
}


.DayPicker-Caption:not(.DayPicker-Day--outside) {
  font-size: 18px;
  font-weight: normal;
  font-stretch: normal;
  font-style: normal;
  line-height: 1.36;
  letter-spacing: normal;
  text-align: center;
  color: #000000;
}

.DayPicker-Weekdays:not(.DayPicker-Day--outside) {
  font-weight: normal;
  font-stretch: normal;
  font-style: normal;
  line-height: 1.36;
  letter-spacing: normal;
  text-align: center;
  color: #000000;
}

.DayPicker-Day--selected div {  
  background-color: #26806b;
  height: fit-content !important;
  padding: 8px;
  vertical-align: middle;
  text-align: center;
}

.DayPicker-Day--selected:not(.DayPicker-Day--outside) {
  color: black;
  background-color: white !important;
  border-radius: 0px !important;
  vertical-align: middle;
  text-align: center;
}

.DayPicker-Day--last div:not(.DayPicker-Day--outside) {
  background-color: #26806b !important;  
  border-radius: 0px !important;
  color: white;
  border-top-right-radius: 50% !important;
  border-bottom-right-radius: 50% !important;
  outline: none;
}

.DayPicker-Day--availableStartDays:not(.DayPicker-Day--booked) div:not(.DayPicker-Day--outside) {
  border-top-left-radius: 50% !important;
  border-bottom-left-radius: 50% !important;
}

.DayPicker-Day--availableEndDays:not(.DayPicker-Day--booked) div:not(.DayPicker-Day--outside) {
  border-top-right-radius: 50%;
  border-bottom-right-radius: 50%;
}

// this covers when avalible comes right after booking, we do not need it now
//.DayPicker-Day--booked + .DayPicker-Day--avaliable:not(.DayPicker-Day--booked) > div:not(.DayPicker-Day--outside){
//  border-top-left-radius: 50%;
//  border-bottom-left-radius: 50%;
//}

// this covers when avalible comes right before booking, and is not supported in most browsers
//.DayPicker-Week:has(.DayPicker-Day--avaliable + .DayPicker-Day--booked > div:not(.DayPicker-Day--outside)) .DayPicker-Day--avaliable > div:not(.DayPicker-Day--outside){
//  border-top-right-radius: 50%;
 // border-bottom-right-radius: 50%;
}
`;


  function renderDay(day: Date) {
    const date = day.getDate();
    return (
      <div>{date}</div>
    );
  }

  return (
    <React.Fragment>
      <Grid container item xs={12}>
        <div className={styles.BookingEditContainer}>
          <Grid container style={{ marginTop: 10, marginBottom: 10 }}>
            <Grid item xs={1}>
              <p className={styles.SubTitles}>Boat</p>
            </Grid>
            {editBookingContext.booking.boat === undefined ? (
              <>
                <Grid container item xs={8} alignItems="center">
                  <Grid item xs={12}>
                    <Grid item xs={4}>
                      <FormControl className={styles.formSelect}>
                        <input
                          className={styles.textInput}
                          placeholder="Type boat name to filter..."
                          onChange={e => setBoatSearch(e.target.value)}
                        ></input>
                      </FormControl>
                    </Grid>
                  </Grid>
                </Grid>
                <Grid xs={12} item>
                  <Element className={styles.boatPicker} name="pickBoatScroll">
                    {charterBoatsContext.charterBoats
                      .filter(boat =>
                        boat.name.toLowerCase().includes(boatSearch.toLowerCase())
                      ).length > 0 ?
                      <>
                        {charterBoatsContext.charterBoats
                          .filter(boat =>
                            boat.name.toLowerCase().includes(boatSearch.toLowerCase())
                          ).map(boat => (
                            <Element name={boat.name} key={boat.id}>
                              <Grid item className={styles.boatRow} key={boat.id}>
                                <BoatRow
                                  boat={boat}
                                  boatSelected={false}
                                  choseBoat={choseBoat}
                                ></BoatRow>
                              </Grid>
                            </Element>
                          ))}
                      </>
                      :
                      <div className={styles.noBoats}>No boats matching your search "{boatSearch}".</div>
                    }
                  </Element>
                </Grid>
              </>
            ) : (
                <Grid item className={styles.boatRow}>
                  <BoatRow
                    boat={editBookingContext.booking.boat!}
                    boatSelected={true}
                    deselectBoat={deselectBoat}
                  ></BoatRow>
                </Grid>
              )}
          </Grid>
          <Divider />
          <p className={styles.SubTitles}>Period of Booking</p>
          <Grid container>
            <Grid item xs={12}>
              <style>{calendarStyle}</style>
              <DayPicker
                onDayClick={handleDayClick}
                onDayMouseEnter={handleDayMouseEnter}
                modifiers={modifiers}
                numberOfMonths={2}
                selectedDays={date}
                renderDay={renderDay}
              />
            </Grid>
            <Grid item xs={12}>
              <p style={{ marginLeft: 20 }}>
                {!date?.from && !date?.to && "Please select the first day."}
                {date?.from &&
                  !date?.to &&
                  "First day is " +
                  dateHelper.formatOneDate(date.from) +
                  ". Please select the last day."}
                {date?.from &&
                  date?.to &&
                  `Selected from ${dateHelper.formatOneDate(date.from)} to
                ${dateHelper.formatOneDate(date.to)}`}{" "}
                {date?.from && date?.to && (
                  <button
                    onClick={handleResetClick}
                    className={styles.resetBtn}
                  >
                    <span>Reset</span>
                  </button>
                )}
              </p>
            </Grid>
          </Grid>
          <Helmet>
            <style>{`

          `}</style>
          </Helmet>
          <Divider />
          <p className={styles.SubTitles}>Guest</p>
          <EditGuest countries={props.countries}></EditGuest>
          <Grid container justify="space-between">
            <Grid item xs={6}>
              <button className={styles.previousBtn} onClick={() => closeEditBooking()}>
                <span>Cancel</span>
              </button>
            </Grid>
            <Grid item xs={6}>
              <Button
                className={styles.SaveButton}
                onClick={() => props.saveBooking()}
              >
                <span className={styles.SaveButtonText}>
                  Save & Send to Guest
                </span>
              </Button>
            </Grid>
          </Grid>
        </div>
      </Grid>
    </React.Fragment>
  );
};

export default CreateBookingContainer;
