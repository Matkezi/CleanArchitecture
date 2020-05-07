export const example = "https://example.free.beeceptor.com"
export const assessorApiURL = (assessmentId?: number) => 'url neki' + (assessmentId ?
    "/" + assessmentId + example : example)

export const skipperRegisterAPI = "http://localhost:44333/api/Skipper"