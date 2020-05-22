import React from 'react';

const urlPrefix = ':url';
const stepNumberPrefix = ':stepNumber';
const emailPrefix = ':email';
const newEmailPrefix = ':newEmail';
const tokenPrefix = ':token';
const skipperIdPrefix = ':skipperId';
const bookingUrlPrefix = ':bookingUrl';

export const CLIENT = {
    START_PAGE: `/`,
    PUBLIC: {
        ABOUT: `/public/about`,
        TOS: {
            GUEST: `public/tos-guest`,
            CHARTER: `public/tos-charter`,
            SKIPPER: `public/tos-skipper`
        },
        PRIVACY_POLICY: `public/privacy-policy`
    },
    APP: {
        FORGOTTEN_PASSWORD: `/login/forgotten-password`,
        LOGIN: `/login`,
        PASSWORD_RESET: (email: string = emailPrefix, token: string = tokenPrefix): string => `/password-reset/email=${email}/token=${token}`,
        CHANGE_EMAIL: (email: string = emailPrefix, newEmail: string = newEmailPrefix, token: string = tokenPrefix): string => `/change-email/email=${email}/newEmail=${newEmail}/token=${token}`
    },
    CHARTER: {
        BOATS: `/charter/boats`,
        DASHBOARD: `/charter/dashboard`,
        SETTINGS: `/charter/settings`,
        SKIPPER_PREREGISTRATION: `/charter/preregistration`,
        TRUSTED_SKIPPERS: `/charter/trusted-skippers`,
        SKIPPER_PROFILE: (skipperId: string = skipperIdPrefix): string => `/charter/skipper-profile/${skipperId}`
    },
    SKIPPER: {
        AVAILABILITY: `/skipper/availability`,
        DASHBOARD: `/skipper/dashboard`,
        PROFILE: `/skipper/profile`,
        SETTINGS: `/skipper/settings`,
        REGISTRATION: (URL: string = urlPrefix, stepNumber: string = stepNumberPrefix): string => `/skipper/registration/${URL}/${stepNumber}`,
    },
    GUEST: {
        BOOKING: (bookingUrl: string = bookingUrlPrefix, stepNumber: string = stepNumberPrefix): string => `/guest/booking/${bookingUrl}/step${stepNumber}`,
        SKIPPER_PROFILE: (skipperId: string = skipperIdPrefix): string => `/guest/skipper-profile/${skipperId}`
    }
}