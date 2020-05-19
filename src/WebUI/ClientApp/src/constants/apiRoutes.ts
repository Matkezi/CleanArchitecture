
export const API = {
    ACCOUNT: {
        LOGIN: `Account/login`,
        FACEBOOK_LOGIN: `Account/facebook-login`,
        PASSWORD_RESET_REQUEST: (email: string): string => `Account/password-reset-email/${email}`,
        RESET_PASSWORD: (email: string, password: string, token: string): string => `Account/password-reset/${email}/${token}/${password}`,
        CHANGE_EMAIL_REQUEST: (email: string, newEmail: string): string => `Account/email-reset-email/${email}/${newEmail}`,
        CHANGE_EMAIL: (email: string, newEmail: string, token: string): string => `Account/change-email/${email}/${newEmail}/${token}`,
        CHANGE_PASSWORD: (email: string, currentPassword: string, newPassword: string): string => `Account/change-password/${email}/${currentPassword}/${newPassword}`
    },
    CHARTER: {
        GET_CHARTER_BY_ID: (id: string): string => `Charter/${id}`,
        SKIPPERS: {
            PENDING: `TrustedSkipper/pending`,
            TRUSTED: `TrustedSkipper/trusted`,
            UNTRUSTED: `TrustedSkipper/untrusted`,
            UPDATE_TRUSTED: 'TrustedSkipper/trusted',
            UPDATE_UNTRUSTED: `TrustedSkipper/untrusted`
        },
        BOOKINGS: {
            GET_ALL: `CharterBooking`,
            CREATE: `CharterBooking`,
            DELETE: (id: string): string => `CharterBooking/${id}`
        },
        BOATS: {
            GET_CHARTER_BOATS: `Boat/charter`,
            SAVE_BOAT: `Boat`,
            UPDATE_BOAT: (id: number): string => `Boat/${id}`,
            DELETE_BOAT: (id: number): string => `Boat/${id}`
        }
    },
    SKIPPER: {
        GET_ALL: `Skipper`,
        GET: (id: string): string => `Skipper/${id}`,
        BOOKING: {
            PENDING: `SkipperBooking/pending`,
            ACCEPTED: `SkipperBooking/accepted`,
            ACCEPT: `SkipperBooking/accept`,
            DECLINE: `SkipperBooking/decline`,
        },
        PROFILE: {
            GET_BY_ID: (id: string): string => `Skipper/${id}`,
        },
        AVAILABILITY: {
            GET_AVAILABILITY: (id: string): string => `Availability/${id}`,
            UPDATE_AVAILABILITY: `Availability`
        },
        REGISTER: `Skipper`,
        UPDATE: (id: string): string => `Skipper/${id}`,
        DELETE: (id: string): string => `Skipper/${id}`,
        PREREGISTER: `Skipper/preregister`,
        GET_PREREGISTRATION: (URL: string): string => `Skipper/preregister/${URL}`

    },
    GUEST: {
        BOOKING: {
            GET: (URL: string): string => `GuestBooking/url/${URL}`,
            GET_AVAILABLE_SKIPPERS: `GuestBooking/available-skippers`,
            REQUEST: `GuestBooking/request`
        }
    },
    META: {
        GET_LANGUAGES: `Meta/languages`,
        GET_COUNTRIES: `Meta/countries`
    },
    SKILLS: {
        GET_ALL: `Skill`,
        GET_BY_ID: (id: number): string => `Skill/${id}`
    }
}