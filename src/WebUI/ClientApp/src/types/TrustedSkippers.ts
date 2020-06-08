import { SkipperStatus } from "./SkipperStatus";
import { ILanguage } from "./ILanguage";

export enum TrustedSkippersAction {
    UnmarkAll,
    Accept,
    Decline
}

export interface TrustedSkipperProfile {
    id: string,
    imageUrl: string,
    firstName: string,
    yearOfFirstLicense: number,
    listOfLanguages: ILanguage[]
}

export interface TrustedSkippersProps {
    currentlySelectedSkipperStatus: SkipperStatus,
    initialPendingSkippers: TrustedSkipperProfile[],
    initialAcceptedSkippers: TrustedSkipperProfile[],
    initialDeclinedSkippers: TrustedSkipperProfile[],
    pendingSkippers: string[],
    acceptedSkippers: string[],
    declinedSkippers: string[],
    skippersToRender: TrustedSkipperProfile[],
    setCurrentlySelectedSkipperStatus: Function;
    setInitialPendingSkippers: Function,
    setInitialAcceptedSkippers: Function,
    setInitialDeclinedSkippers: Function,
    setPendingSkippers: Function,
    setAcceptedSkippers: Function,
    setDeclinedSkippers: Function,
    getInitialPendingSkippers: Function,
    getInitialAcceptedSkippers: Function,
    getInitialDeclinedSkippers: Function,
    setSkippersToRender: Function
}