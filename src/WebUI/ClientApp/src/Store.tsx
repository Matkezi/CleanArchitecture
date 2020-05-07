import React from "react";
import { SkipperRegistrationProvider } from "./providers/skippers/registration";
import { LoginProvider } from "./providers/login";
import { TrustedSkippersProvider } from "./providers/skippers/trustedSkippers";
import { NotificationProvider } from "./providers/notification";
import { BookingContextProvider } from "./providers/booking/booking";
import { EditBookingContextProvider } from "./providers/booking/editBookingContext";
import { BoatContextProvider } from "./providers/boats/boatsContext";
import { SkipperProfileProvider } from "./providers/skippers/profile";
import { CharterProvider } from './providers/charter/charter';
import GuestBookingContextProvider from "./providers/booking/guestBooking";
import { LanguageProvider } from "./providers/langugages";

const Store = ({ children }: any) => {
  return (
    <SkipperRegistrationProvider>
      <LoginProvider>
        <NotificationProvider>
          <BookingContextProvider>
            <CharterProvider>
              <TrustedSkippersProvider>
                <EditBookingContextProvider>
                  <BoatContextProvider>
                    <SkipperProfileProvider>
                      <GuestBookingContextProvider>
                        <LanguageProvider>
                          {children}
                        </LanguageProvider>
                      </GuestBookingContextProvider>
                    </SkipperProfileProvider>
                  </BoatContextProvider>
                </EditBookingContextProvider>
              </TrustedSkippersProvider>
            </CharterProvider>
          </BookingContextProvider>
        </NotificationProvider>
      </LoginProvider>
    </SkipperRegistrationProvider>
  );
};

export default Store;
