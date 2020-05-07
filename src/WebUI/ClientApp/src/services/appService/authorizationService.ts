import jwt_decode from 'jwt-decode';
import { Roles } from '../../types/Roles';

export const roleProp = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

export function isAdmin() {
    return getUserRole() === Roles.Admin && isAuthenticated();
}

export function getSkipperIDFromUrl() {
    const url = window.location.href;
    const skipperId = url.split('skipper-profile/')[1];
    return skipperId;
}


export function isCharter() {
    return (getUserRole() === Roles.Charter || getUserRole() === Roles.Admin) && isAuthenticated();
}
export function isSkipper() {
    return (getUserRole() === Roles.Skipper || getUserRole() === Roles.Admin) && isAuthenticated();
}

export function getToken(): any | null {
    let token: any = localStorage.getItem('authToken');
    if (token) {
        if (token) return token;
    }
    return null;
}

export function isAuthenticated(): boolean {
    var token = getToken();
    if (token !== null) {
        const tokenDec: any = jwt_decode(getToken());
        var dateNow = new Date().getTime().valueOf() / 1000;
        return tokenDec.exp > dateNow;
    }
    return false;
}

export function getUserRole() {
    const token = getToken();
    if (!token) return null;
    var payload: any = jwt_decode(token);
    var role = payload[roleProp]
    return role;
}

export function getUserId() {
    const token = getToken();
    if (!token) return null;
    var payload: any = jwt_decode(token);
    var userId = payload["UserId"];
    return userId;
}
