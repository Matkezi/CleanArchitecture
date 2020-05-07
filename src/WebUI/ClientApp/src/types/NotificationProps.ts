export interface NotificationProps {
    loading: {
        showLoading: boolean
    },
    snackbar: ISnackbarData,
    setLoading: Function,
    setSnackbar: Function
}

export interface ISnackbarData {
    showSnackbar: boolean,
    message?: string,
    type?: NotificationType
}

export enum NotificationType {
    Info = "Info", Error = "Error", Success = "Success"
}