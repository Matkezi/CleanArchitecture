export class dateHelper {
    static formatDates = (startDate: Date, endDate: Date): string => {
        var startDateFormatted = (new Date(startDate.toString()))
            .toLocaleDateString("hr-HR")
            .replace('. ', '.')
            .split(' ')[0]
        toString();
        var endDateFormatted = (new Date(endDate.toString()))
            .toLocaleDateString("hr-HR")
            .replace('. ', '.')
            .replace('. ', '.')
            .toString();
        return startDateFormatted + " - " + endDateFormatted;
    }

    static formatOneDate = (date: Date): string => {
        return (new Date(date.toString()))
            .toLocaleDateString("hr-HR")
            .replace('. ', '.')
            .replace('. ', '.')
            .toString();
    }
}