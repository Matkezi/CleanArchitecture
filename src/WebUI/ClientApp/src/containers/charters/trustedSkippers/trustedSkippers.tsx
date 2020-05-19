import React, { useContext, useState, useEffect } from "react";
import TrustedSkippersFooter from "../../../components/charter/trustedSkippers/trustedSkippersFooter";
import SkippersHeader from "../../../components/shared/skippersHeader";
import TrustedSkippersProfileCard from "../../../components/charter/trustedSkippers/trustedSkippersProfileCard";
import { TrustedSkippersAction } from "../../../types/TrustedSkippers";
import { SkipperStatus } from "../../../types/SkipperStatus";
import wrapperStlyes from "./../../../components/skippers/registration/styles.module.scss";
import { TrustedSkippersContext } from "../../../providers/skippers/trustedSkippers";
import Grid from "@material-ui/core/Grid";
import { Divider, LinearProgress, withStyles } from "@material-ui/core";
import trustedSkippersApi from "../../../services/api/charter/trustedSkippersApi";
import styles from "./../../../components/charter/trustedSkippers/trustedSkippers.module.scss";
import { NotificationContext } from "../../../providers/notification";
import { NotificationType } from "../../../types/NotificationProps";
import { CLIENT } from "../../../constants/clientRoutes";

interface IProps {
  history: any,
  setActiveTab: (tab: number) => void
}

const CustomLinearProgres = withStyles({
  colorPrimary: {
    backgroundColor: '#26806b',
  },
  barColorPrimary: {
    backgroundColor: '#B2DFDB',
  }
})((props: any) => <LinearProgress {...props} className={styles.linearProgress} />);

const useStateWithLocalStorage = (localStorageKey: string) => {
  const [value, setValue] = useState(localStorage.getItem(localStorageKey) || '');
  React.useEffect(() => {
    localStorage.setItem(localStorageKey, value);
  }, [value]);
  return [value, setValue];
};

const TrustedSkippers: React.FC<IProps> = (props: IProps) => {
  const trustedSkippersContext = useContext(TrustedSkippersContext);
  const notificationContext = useContext(NotificationContext);
  const [pendingSkippers, setPendingSkippers] = useStateWithLocalStorage('pending');
  const [approvedSkippers, setApprovedSkippers] = useStateWithLocalStorage('approved');
  const [declinedSkippers, setDeclinedSkippers] = useStateWithLocalStorage('declined');
  const [loading, setLoading] = useState(false);

  useEffect(() => {
    props.setActiveTab(2);
    updateInitialSkippersFromBackend(true);
    putInContextFromLocalstorage();
  }, []);

  const updateInitialSkippersFromBackend = async (showLoading: boolean) => {
    setLoading(showLoading);
    try {
      trustedSkippersContext.setPendingSkippers([]);
      trustedSkippersContext.setAcceptedSkippers([]);
      trustedSkippersContext.setDeclinedSkippers([]);

      var pending = await trustedSkippersContext.getInitialPendingSkippers();
      trustedSkippersContext.setInitialPendingSkippers([...pending]);
      if (
        trustedSkippersContext.currentlySelectedSkipperStatus ===
        SkipperStatus.Pending
      ) {
        trustedSkippersContext.setSkippersToRender([...pending]);
      }
      var accepted = await trustedSkippersContext.getInitialAcceptedSkippers();
      trustedSkippersContext.setInitialAcceptedSkippers([...accepted]);
      if (
        trustedSkippersContext.currentlySelectedSkipperStatus ===
        SkipperStatus.Approved
      ) {
        trustedSkippersContext.setSkippersToRender([...accepted]);
      }
      var declined = await trustedSkippersContext.getInitialDeclinedSkippers();
      trustedSkippersContext.setInitialDeclinedSkippers([...declined]);
      if (
        trustedSkippersContext.currentlySelectedSkipperStatus ===
        SkipperStatus.Declined
      ) {
        trustedSkippersContext.setSkippersToRender([...declined]);
      }
      setLoading(false);
    } catch (e) {
      setLoading(false);
      notificationContext.setSnackbar({
        showSnackbar: true,
        message: e.message,
        type: NotificationType.Error
      });
    }
  };


  const putInContextFromLocalstorage = () => {
    var pendSkippers = pendingSkippers.toString().split(";");
    trustedSkippersContext.setPendingSkippers(
      [...trustedSkippersContext.pendingSkippers,
      ...(pendSkippers.filter(s => (s.length > 0 && !trustedSkippersContext.pendingSkippers.includes(s))))
      ]);

    var appSkippers = approvedSkippers.toString().split(";");
    trustedSkippersContext.setAcceptedSkippers(
      [...trustedSkippersContext.acceptedSkippers,
      ...(appSkippers.filter(s => (s.length > 0 && !trustedSkippersContext.acceptedSkippers.includes(s))))
      ]);

    var declSkippers = declinedSkippers.toString().split(";");
    trustedSkippersContext.setDeclinedSkippers(
      [...trustedSkippersContext.declinedSkippers,
      ...(declSkippers.filter(s => (s.length > 0 && !trustedSkippersContext.declinedSkippers.includes(s))))
      ]);

  }

  // Called from header
  const updateTrustedSkipperStatus = async (
    updatedSkipperStatus: SkipperStatus
  ) => {
    trustedSkippersContext.setCurrentlySelectedSkipperStatus(
      updatedSkipperStatus
    );
    switch (updatedSkipperStatus) {
      case SkipperStatus.Pending:
        trustedSkippersContext.setSkippersToRender([
          ...trustedSkippersContext.initialPendingSkippers
        ]);
        break;
      case SkipperStatus.Approved:
        trustedSkippersContext.setSkippersToRender([
          ...trustedSkippersContext.initialAcceptedSkippers
        ]);
        break;
      case SkipperStatus.Declined:
        trustedSkippersContext.setSkippersToRender([
          ...trustedSkippersContext.initialDeclinedSkippers
        ]);
        break;
    }
  };

  const addSkipperSelected = (skipperId: string, skipperStatus: SkipperStatus) => {
    switch (skipperStatus) {
      case SkipperStatus.Pending:
        const newPending = pendingSkippers.length > 0 ? pendingSkippers + ";" + skipperId : skipperId;
        (setPendingSkippers as React.Dispatch<React.SetStateAction<string>>)(newPending);
        break;
      case SkipperStatus.Approved:
        const newApproved = approvedSkippers.length > 0 ? approvedSkippers + ";" + skipperId : skipperId;
        (setApprovedSkippers as React.Dispatch<React.SetStateAction<string>>)(newApproved);
        break;
      case SkipperStatus.Declined:
        const newDeclined = declinedSkippers.length > 0 ? declinedSkippers + ";" + skipperId : skipperId;
        (setDeclinedSkippers as React.Dispatch<React.SetStateAction<string>>)(newDeclined);
        break;
    }
  }

  const removeSkipperSelected = (skipperId: string, skipperStatus: SkipperStatus) => {
    switch (skipperStatus) {
      case SkipperStatus.Pending:
        const newPending = pendingSkippers.toString().replace(skipperId, "");
        (setPendingSkippers as React.Dispatch<React.SetStateAction<string>>)(newPending);
        break;
      case SkipperStatus.Approved:
        const newApproved = approvedSkippers.toString().replace(skipperId, "");
        (setApprovedSkippers as React.Dispatch<React.SetStateAction<string>>)(newApproved);
        break;
      case SkipperStatus.Declined:
        const newDeclined = declinedSkippers.toString().replace(skipperId, "");
        (setDeclinedSkippers as React.Dispatch<React.SetStateAction<string>>)(newDeclined);
        break;
    }
  }

  // Called from profile cards
  const updateSkipperSelected = async (
    skipperSelectedId: string,
    checked: boolean
  ) => {
    if (checked) {
      addSkipperSelected(skipperSelectedId, trustedSkippersContext.currentlySelectedSkipperStatus);
      // Add to list if checked
      switch (trustedSkippersContext.currentlySelectedSkipperStatus) {
        case SkipperStatus.Pending:
          trustedSkippersContext.setPendingSkippers([
            ...trustedSkippersContext.pendingSkippers,
            skipperSelectedId
          ]);
          break;
        case SkipperStatus.Approved:
          trustedSkippersContext.setAcceptedSkippers([
            ...trustedSkippersContext.acceptedSkippers,
            skipperSelectedId
          ]);
          break;
        case SkipperStatus.Declined:
          trustedSkippersContext.setDeclinedSkippers([
            ...trustedSkippersContext.declinedSkippers,
            skipperSelectedId
          ]);
          break;
      }
    } else {
      // Remove from list if not checked
      removeSkipperSelected(skipperSelectedId, trustedSkippersContext.currentlySelectedSkipperStatus);
      switch (trustedSkippersContext.currentlySelectedSkipperStatus) {
        case SkipperStatus.Pending:
          trustedSkippersContext.setPendingSkippers(
            trustedSkippersContext.pendingSkippers.filter(
              id => id !== skipperSelectedId
            )
          );
          break;
        case SkipperStatus.Approved:
          trustedSkippersContext.setAcceptedSkippers(
            trustedSkippersContext.acceptedSkippers.filter(
              id => id !== skipperSelectedId
            )
          );
          break;
        case SkipperStatus.Declined:
          trustedSkippersContext.setDeclinedSkippers(
            trustedSkippersContext.declinedSkippers.filter(
              id => id !== skipperSelectedId
            )
          );
          break;
      }
    }
  };

  // Called from footer
  const updateTrustedSkippersAction = async (
    trustedSkippersAction: TrustedSkippersAction
  ) => {
    (setPendingSkippers as React.Dispatch<React.SetStateAction<string>>)("");
    (setApprovedSkippers as React.Dispatch<React.SetStateAction<string>>)("");
    (setDeclinedSkippers as React.Dispatch<React.SetStateAction<string>>)("");
    switch (trustedSkippersAction) {
      case TrustedSkippersAction.UnmarkAll:
        switch (trustedSkippersContext.currentlySelectedSkipperStatus) {
          case SkipperStatus.Pending:
            trustedSkippersContext.setPendingSkippers([]);
            break;
          case SkipperStatus.Approved:
            trustedSkippersContext.setAcceptedSkippers([]);
            break;
          case SkipperStatus.Declined:
            trustedSkippersContext.setDeclinedSkippers([]);
            break;
        }
        break;
      case TrustedSkippersAction.Accept:
        setLoading(true);
        try {
          switch (trustedSkippersContext.currentlySelectedSkipperStatus) {
            case SkipperStatus.Pending:
              await trustedSkippersApi.updateTrustedSkippers(
                trustedSkippersContext.pendingSkippers
              );
              break;
            case SkipperStatus.Declined:
              await trustedSkippersApi.updateTrustedSkippers(
                trustedSkippersContext.declinedSkippers
              );
              break;
          }

          setLoading(false);
          notificationContext.setSnackbar({ showSnackbar: true, message: "Trusted skippers updated!", type: NotificationType.Success })
        } catch (e) {
          notificationContext.setSnackbar({ showSnackbar: true, message: e.message, type: NotificationType.Error })
        }
        updateInitialSkippersFromBackend(false);
        break;
      case TrustedSkippersAction.Decline:
        setLoading(true);
        try {
          switch (trustedSkippersContext.currentlySelectedSkipperStatus) {
            case SkipperStatus.Pending:
              await trustedSkippersApi.updateUnTrustedSkippers(
                trustedSkippersContext.pendingSkippers
              );
              break;
            case SkipperStatus.Approved:
              await trustedSkippersApi.updateUnTrustedSkippers(
                trustedSkippersContext.acceptedSkippers
              );
              break;
          }
          setLoading(false);
          notificationContext.setSnackbar({ showSnackbar: true, message: "Trusted skippers updated!", type: NotificationType.Success })
        } catch (e) {
          notificationContext.setSnackbar({ showSnackbar: true, message: e.message, type: NotificationType.Error })
        }
        updateInitialSkippersFromBackend(false);
        break;
    }
  };

  const isSkipperChecked = (skipperId: string): boolean => {
    return (
      pendingSkippers.toString().includes(skipperId) || approvedSkippers.toString().includes(skipperId) || declinedSkippers.toString().includes(skipperId)
    );
  };

  const viewSkipperProfile = (skipperId: string) => {
    props.history.push(CLIENT.CHARTER.SKIPPER_PROFILE(skipperId));
  }

  return (
    <div className={wrapperStlyes.wrapper}>
      <Grid container>
        <Grid container item xs={12} spacing={4} alignItems="stretch">
          <Grid item xs={12} style={{ minWidth: 1000, marginTop: 20 }}>
            <SkippersHeader
              color="#26816A"
              showDeclined={true}
              updateSkipperStatus={updateTrustedSkipperStatus}
            ></SkippersHeader>
            <Divider style={{ marginTop: 20 }} />
          </Grid>
          <Grid
            container
            item
            spacing={4}
            alignItems="center"
            justify="center"
            className={styles.cardContainer}
          >
            {loading ?
              <CustomLinearProgres />
              :
              <>
                {trustedSkippersContext.skippersToRender.map(skipper => (
                  <Grid item className={styles.card} key={skipper.id}>
                    <TrustedSkippersProfileCard
                      checked={isSkipperChecked(skipper.id)}
                      trustedSkipperProfile={skipper}
                      viewSkipperProfile={viewSkipperProfile}
                      updateSkipperSelected={updateSkipperSelected}
                    ></TrustedSkippersProfileCard>
                  </Grid>
                ))}
              </>
            }
          </Grid>
          <Grid item>
            <TrustedSkippersFooter
              updateTrustedSkippersAction={updateTrustedSkippersAction}
            ></TrustedSkippersFooter>
          </Grid>
        </Grid>
      </Grid>
    </div>
  );
};

export default TrustedSkippers;
