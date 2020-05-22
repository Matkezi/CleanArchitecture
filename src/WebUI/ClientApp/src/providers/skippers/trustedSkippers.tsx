import React, { useState } from 'react';
import trustedSkippersApi from '../../services/api/charter/trustedSkippersApi';
import { TrustedSkipperProfile, TrustedSkippersAction, TrustedSkippersProps } from '../../types/TrustedSkippers'
import { SkipperStatus } from "../../types/SkipperStatus";


export const TrustedSkippersContext = React.createContext<TrustedSkippersProps>({
    currentlySelectedSkipperStatus: SkipperStatus.Pending,
    initialPendingSkippers: [],
    initialAcceptedSkippers: [],
    initialDeclinedSkippers: [],
    pendingSkippers: [],
    acceptedSkippers: [],
    declinedSkippers: [],
    skippersToRender: [],
    setCurrentlySelectedSkipperStatus: () => null,
    setInitialPendingSkippers: () => null,
    setInitialAcceptedSkippers: () => null,
    setInitialDeclinedSkippers: () => null,
    setPendingSkippers: () => null,
    setAcceptedSkippers: () => null,
    setDeclinedSkippers: () => null,
    getInitialPendingSkippers: () => null,
    getInitialAcceptedSkippers: () => null,
    getInitialDeclinedSkippers: () => null,
    setSkippersToRender: () => null
});

export const TrustedSkippersProvider: React.ComponentType<React.ReactNode> = props => {
    const [currentlySelectedSkipperStatus, setCurrentlySelectedSkipperStatus] = useState<SkipperStatus>(SkipperStatus.Pending);
    const [initialPendingSkippers, setInitialPendingSkippers] = useState<TrustedSkipperProfile[]>([]);
    const [initialAcceptedSkippers, setInitialAcceptedSkippers] = useState<TrustedSkipperProfile[]>([]);
    const [initialDeclinedSkippers, setInitialDeclinedSkippers] = useState<TrustedSkipperProfile[]>([]);
    const [pendingSkippers, setPendingSkippers] = useState<string[]>([]);
    const [acceptedSkippers, setAcceptedSkippers] = useState<string[]>([]);
    const [declinedSkippers, setDeclinedSkippers] = useState<string[]>([]);
    const [skippersToRender, setSkippersToRender] = useState<TrustedSkipperProfile[]>([]);

    const getInitialPendingSkippers = async () => {
        return await trustedSkippersApi.getPendingSkippers();
    }

    const getInitialAcceptedSkippers = async () => {
        return await trustedSkippersApi.getAcceptedSkippers();
    }

    const getInitialDeclinedSkippers = async () => {
        return await trustedSkippersApi.getDeclinedSkippers();
    }

    return (
        <TrustedSkippersContext.Provider
            value={{
                currentlySelectedSkipperStatus, setCurrentlySelectedSkipperStatus,
                initialPendingSkippers, initialAcceptedSkippers, initialDeclinedSkippers,
                pendingSkippers, acceptedSkippers, declinedSkippers, skippersToRender,
                setInitialPendingSkippers, setInitialAcceptedSkippers, setInitialDeclinedSkippers,
                setPendingSkippers, setAcceptedSkippers, setDeclinedSkippers, setSkippersToRender,
                getInitialPendingSkippers, getInitialAcceptedSkippers, getInitialDeclinedSkippers
            }}>
            {props.children}
        </TrustedSkippersContext.Provider>
    );
};