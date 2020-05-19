import React, { useState, useEffect, useContext } from "react";
import DayPicker, { RangeModifier } from "react-day-picker";
import "react-day-picker/lib/style.css";
import SkipperApi from "../../services/api/skipper/skipperProfileApi";
import { getUserId } from '../../services/appService/authorizationService';
import styles from './styles.module.scss'
import { Grid } from "@material-ui/core";
import { dateHelper } from '../../helpers/dateHelper';
import { NotificationContext } from "../../providers/notification";
import { NotificationType } from "../../types/NotificationProps";

interface IProps {
  setActiveTab: (tab: number) => void
}

const SkipperAvalibility: React.FC<IProps> = (props: IProps) => {
  const [clickedFirst, setClickedFirst] = useState<Date>();
  const [before, setBefore] = useState<Date>();
  const [booked, setBooked] = useState<RangeModifier[]>([]);
  const [bookedStartDays, setBookedStartDays] = useState<Date[]>([]);
  const [bookedEndDays, setBookedEndDays] = useState<Date[]>([]);
  const [avaliable, setAvaliable] = useState<RangeModifier[]>([]);
  const [enteredTo, setEnteredTo] = useState<Date>();
  const [availableStartDays, setAvailableStartDates] = useState<Date[]>([]);
  const [availableEndDays, setAvailableEndDates] = useState<Date[]>([]);
  const [message, setMessage] = useState("Select days on calendar to mark them as available in order to get bookings or click on selected range to remove it.");
  const [markedDays, setMarkedDays] = useState(0);
  const notificationContext = useContext(NotificationContext);

  useEffect(() => {
    window.scrollTo(0, 0);
    props.setActiveTab(2);
    try {
      notificationContext.setLoading({ showLoading: true });
      getAvalibility();
    } catch (e) {
      notificationContext.setSnackbar({ showSnackbar: true, message: e.message, type: NotificationType.Error })
    } finally {
      notificationContext.setLoading({ showLoading: false });
    }
  }, []);

  const isFirstDayOfWeekOrMonth = (day: Date): boolean => {
    return day.getDay() === 0 || day.getDate() === 1;
  }

  const isLastDayOfWeekOrMonth = (day: Date): boolean => {
    return day.getDay() === 6 || (new Date(day.getTime() + 86400000).getDate() === 1);
  }

  const getBookedRange = (booked: RangeModifier[]) => {
    const bookedArray = booked.slice().reverse();

    var tempArray1: Date[] = [];
    var tempArray2: Date[] = [];
    bookedArray.map((range, i) => {
      var dates = getDates(range.from, range.to);
      tempArray1.push(dates[0]);
      tempArray2.push(dates[dates.length - 1]);
      dates.map((day: Date, i) => {
        if (isFirstDayOfWeekOrMonth(day)) {
          tempArray1.push(day);
        }
        else if (isLastDayOfWeekOrMonth(day)) {
          tempArray2.push(day);
        }
      })
      setBookedStartDays([...tempArray1]);
      setBookedEndDays([...tempArray2]);
    });
  }

  const getAvailableRange = (availableRange: RangeModifier[]) => {
    const avalibleArray = availableRange.slice();
    var tempArray1: Date[] = [];
    var tempArray2: Date[] = [];
    avalibleArray.map((range, i) => {
      var dates = getDates(range.from, range.to);
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
    });
  }

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

  const getAvalibility = async () => {
    var avalibilityData = await SkipperApi.getSkipperAvalibility(getUserId());
    setBooked(avalibilityData.booked.map<RangeModifier>(date => {
      var from = new Date(date.from);
      var to = new Date(date.to);
      return {
        from: new Date(from.getFullYear(), from.getMonth(), from.getDate()),
        to: new Date(to.getFullYear(), to.getMonth(), to.getDate())
      }
    }));
    setAvaliable(avalibilityData.available.map<RangeModifier>(date => {
      var from = new Date(date.from);
      var to = new Date(date.to);
      return {
        from: new Date(from.getFullYear(), from.getMonth(), from.getDate()),
        to: new Date(to.getFullYear(), to.getMonth(), to.getDate())
      }
    }));
    getBookedRange(avalibilityData.booked);
    getAvailableRange(avalibilityData.available);
  }

  const updateAvalibility = async () => {
    notificationContext.setLoading({ showLoading: true });
    try {
      await SkipperApi.updateSkipperAvalibility({
        available: avaliable,
        booked: booked
      });
      notificationContext.setLoading({ showLoading: false });
      notificationContext.setSnackbar({ showSnackbar: true, message: "Avalibility updated!", type: NotificationType.Success })
      setMessage("Select days on calendar to mark them as available in order to get bookings!");
    } catch (e) {
      notificationContext.setLoading({ showLoading: false });
      notificationContext.setSnackbar({ showSnackbar: true, message: e.message, type: NotificationType.Error })
    }
  };

  const removeDateRangeOfDay = (date: Date) => {
    setMarkedDays(0);
    var range: RangeModifier = avaliable.filter(range => (range.from!.setHours(0, 0, 0, 0) <= date.setHours(0, 0, 0, 0) &&
      range.to!.setHours(0, 0, 0, 0) >= date.setHours(0, 0, 0, 0)))[0];
    setMessage("You removed available range from " + dateHelper.formatOneDate(new Date(range.from)) + " to " + dateHelper.formatOneDate(new Date(range.to)) + ". Update calendar to remove it from your available dates.")
    setAvaliable(
      avaliable.filter(
        range =>
          !(
            range.from!.setHours(0, 0, 0, 0) <= date.setHours(0, 0, 0, 0) &&
            range.to!.setHours(0, 0, 0, 0) >= date.setHours(0, 0, 0, 0)
          )
      )
    );
    return true;
  };

  const isOverleapingRange = (
    range1: RangeModifier,
    range2: RangeModifier
  ): boolean => {
    return (
      range1.from!.setHours(0, 0, 0, 0) >= range2.from!.setHours(0, 0, 0, 0) &&
      range1.from!.setHours(0, 0, 0, 0) <= range2.to!.setHours(0, 0, 0, 0)
    );
  };

  const getOverleapingRange = (clickedRange: RangeModifier) => {
    return avaliable.filter(range => isOverleapingRange(range, clickedRange));
  };

  const handleDayClick = (day: Date, { selected, disabled }: any) => {
    if (disabled) {
      return;
    } else if (selected && clickedFirst === undefined) {
      removeDateRangeOfDay(day);
      setMarkedDays(0);
    } else if (clickedFirst === undefined) {
      setClickedFirst(day);
      setBefore(day);
    } else {
      const dateRange = { from: clickedFirst, to: day };
      setMarkedDays(markedDays + Math.round(Math.abs((dateRange.from.getTime() - dateRange.to.getTime()) / (24 * 60 * 60 * 1000)) + 1));
      setMessage((markedDays + Math.round(Math.abs((dateRange.from.getTime() - dateRange.to.getTime()) / (24 * 60 * 60 * 1000)) + 1)) + " days are marked. Update calendar to save changes.");

      const overleapingRange = getOverleapingRange(dateRange);
      if (overleapingRange.length > 0) {
        let oldRange = avaliable;
        oldRange = oldRange.filter(
          range =>
            !isOverleapingRange(range, {
              from: dateRange.from,
              to: overleapingRange[0].to
            })
        );
        setAvaliable([...oldRange, dateRange]);
        getAvailableRange([...oldRange, dateRange]);
      } else {
        setAvaliable([...avaliable, dateRange]);
        getAvailableRange([...avaliable, dateRange]);
      }
      clearSelection();
    }
  };

  const handleDayMouseEnter = (day: Date) => {
    if (clickedFirst !== undefined) {
      setEnteredTo(day);
    }
  };

  const clearSelection = () => {
    setClickedFirst(undefined);
    setEnteredTo(undefined);
    setBefore(undefined);
  };

  const modifiers = {
    highlighted: { from: clickedFirst!, to: enteredTo! },
    avaliable: avaliable,
    booked: booked,
    bookedStartDays: bookedStartDays,
    bookedEndDays: bookedEndDays,
    availableStartDays: availableStartDays,
    availableEndDays: availableEndDays,
    first: clickedFirst!,
    last: enteredTo!
  };

  const calendarStyle = `.DayPicker-Day--highlighted:not(.DayPicker-Day--outside) {
    background-color: #274675 !important;
    color: white;
    border-radius: 0px !important;
    outline: none;
}

.DayPicker-Day--first:not(.DayPicker-Day--outside) {
    background-color: #274574 !important;
    color: white;
    border-radius: 0px !important;
    border-top-left-radius: 50% !important;
    border-bottom-left-radius: 50% !important;
    outline: none;
}

.DayPicker-Day {
  color: black;
  border-radius: 0px !important;
  vertical-align: middle;
  text-align: center;
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

.DayPicker-Day--booked:not(.DayPicker-Day--outside) {
  color: black !important;
  border-radius: 0px !important;
  vertical-align: middle;
  text-align: center;
}

.DayPicker-Day--booked div {  
  background-color: #919191 !important;
  height: fit-content !important;
  padding: 8px;
  vertical-align: middle;
  text-align: center;
}

.DayPicker-Day--bookedStartDays div:not(.DayPicker-Day--outside) {
  border-top-left-radius: 50% !important;
  border-bottom-left-radius: 50% !important;
}

.DayPicker-Day--bookedEndDays div:not(.DayPicker-Day--outside) {
  border-top-right-radius: 50% !important;
  border-bottom-right-radius: 50% !important;
}

.DayPicker-Day--avaliable div {  
  background-color: #23395b !important;
  height: fit-content !important;
  padding: 8px;
  vertical-align: middle;
  text-align: center;
}

.DayPicker-Day--avaliable:not(.DayPicker-Day--outside) {
  color: black;
  background-color: white !important;
  border-radius: 0px !important;
  vertical-align: middle;
  text-align: center;
}

.DayPicker-Day--last:not(.DayPicker-Day--outside) {
  background-color: #274574 !important;  
  border-radius: 0px !important;
  color: white;
  border-top-right-radius: 50% !important;
  border-bottom-right-radius: 50% !important;
  outline: none;
}

.DayPicker-Day--availableStartDays:not(.DayPicker-Day--booked) div:not(.DayPicker-Day--outside) {
  border-top-left-radius: 50%;
  border-bottom-left-radius: 50%;
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
    <div className={styles.wrapper}>
      <Grid container>
        <Grid item xs={12}>
          <p className={styles.title}>Availability</p>
        </Grid>
        <Grid item md={12} justify="space-around" container className={styles.availabilityContainer}>
          <Grid item md={10} justify="flex-start" container>
            <Grid item xs={1} style={{ maxWidth: 28 }}>
              <div className={styles.availableSqr}>&nbsp;</div>
            </Grid>
            <Grid item xs={4} md={3}>
              <span> Available</span>
            </Grid>
            <Grid item xs={1} style={{ maxWidth: 28 }}>
              <div className={styles.bookedSqr}>&nbsp;</div>
            </Grid>
            <Grid item xs={4} md={3}>
              <span> Booked</span>
            </Grid>
          </Grid>
          <Grid item md={8} container justify="center">
            <style>{calendarStyle}</style>
            <DayPicker
              numberOfMonths={2}
              selectedDays={avaliable}
              disabledDays={[...booked, { before: before! }]}
              onDayClick={handleDayClick}
              onDayMouseEnter={handleDayMouseEnter}
              modifiers={modifiers}
              renderDay={renderDay}
            />
          </Grid>
          <Grid item container md={4} alignItems="center">
            <Grid item xs={12}>
              <p>{message}</p>
            </Grid>
            <Grid item xs={12} container justify="center">
              <button className={styles.updateBtn} onClick={updateAvalibility}><span>Update calendar</span></button>
            </Grid>
          </Grid>
        </Grid>
      </Grid>
    </div>
  );
};

export default SkipperAvalibility;
