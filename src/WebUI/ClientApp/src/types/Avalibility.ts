
export interface DateRange {
    from: Date;
    to: Date;
}

export interface AvalibilityCalendar {
    booked: DateRange[];
    available: DateRange[];
}