import React, { useContext, useEffect, useState } from "react";
import SkipperProfileComponent from './../../components/skippers/profile/SkipperProfileComponent'
import { ISkipper } from './../../types/ISkipper';
import { SkipperProfileContext } from '../../providers/skippers/profile';
import { LanguageContext } from "../../providers/langugages";
import { getUserId, getSkipperIDFromUrl } from '../../services/appService/authorizationService';
import { NotificationContext } from "../../providers/notification";
import { NotificationType } from "../../types/NotificationProps";
import { RangeModifier } from "react-day-picker";
import SkipperApi from '../../services/skipperService/skipperProfileApi';

interface IProps {
    history: any,
    setActiveTab: (tab: number) => void
}

const SkipperProfile: React.FC<IProps> = (props: IProps) => {

    const skipperProfileContext = useContext(SkipperProfileContext);
    const languageContext = useContext(LanguageContext);
    const notificationContext = useContext(NotificationContext);
    const [booked, setBooked] = useState<RangeModifier[]>([]);
    const [bookedStartDays, setBookedStartDays] = useState<Date[]>([]);
    const [bookedEndDays, setBookedEndDays] = useState<Date[]>([]);
    const [avaliable, setAvaliable] = useState<RangeModifier[]>([]);
    const [availableStartDays, setAvailableStartDates] = useState<Date[]>([]);
    const [availableEndDays, setAvailableEndDates] = useState<Date[]>([]);
    const [skipperProfile, setSkipperProfile] = useState<ISkipper["skipperData"]>();

    const updateProfile = async (skipper: ISkipper) => {
        notificationContext.setLoading({ showLoading: true });
        try {
            var skipperData = await skipperProfileContext.updateSkipper(skipper);
            setSkipperProfile(skipperData);
            notificationContext.setSnackbar({ showSnackbar: true, message: "Profile updated!", type: NotificationType.Success })
        } catch (e) {
            notificationContext.setSnackbar({ showSnackbar: true, message: e.message, type: NotificationType.Error })
        } finally {
            notificationContext.setLoading({ showLoading: false });
        }

    }

    const getAvalibility = async () => {
        var skipperId = getSkipperIDFromUrl() ? getSkipperIDFromUrl() : getUserId();
        var avalibilityData = await SkipperApi.getSkipperAvalibility(skipperId);
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

    const isFirstDayOfWeekOrMonth = (day: Date): boolean => {
        return day.getDay() === 0 || day.getDate() === 1;
    }

    const isLastDayOfWeekOrMonth = (day: Date): boolean => {
        return day.getDay() === 6 || (new Date(day.getTime() + 86400000).getDate() === 1);
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


    useEffect(() => {
        async function getSkipperProfileData() {
            var skipperId = getSkipperIDFromUrl() ? getSkipperIDFromUrl() : getUserId();
            if (skipperId !== null) {
                notificationContext.setLoading({ showLoading: true });
                try {
                    const skipperdata = await skipperProfileContext.getSkipperById(skipperId);
                    setSkipperProfile(skipperdata);
                    await skipperProfileContext.getSkills();
                    await languageContext.getLanguages();
                } catch (e) {
                    notificationContext.setSnackbar({ showSnackbar: true, message: e.message, type: NotificationType.Error })
                } finally {
                    notificationContext.setLoading({ showLoading: false });
                }
            }
            await getAvalibility();
        }
        getSkipperProfileData();
    }, []);

    return skipperProfile && skipperProfile.listOfSkills ?
        <SkipperProfileComponent available={avaliable} booked={booked}
            bookedEndDays={bookedEndDays} bookedStartDays={bookedStartDays}
            availableEndDays={availableEndDays}
            availableStartDays={availableStartDays}
            setActiveTab={props.setActiveTab} history={props.history}
            updateProfile={updateProfile} skills={skipperProfileContext.skills}
            skipperProfile={skipperProfile}
            languages={languageContext.languages} />
        : null;

}

export default SkipperProfile;

