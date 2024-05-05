import {
    URL_BACK_PROFIL,
    URL_BACK_UPD_PASSWORD,
    URL_BACK_GOOGLE_LOGIN,
    URL_BACK_UPD_EMAIL
}from "../../constants/urls/urlBackEnd";
import apiBackEnd from "./api.Backend";
import { PROFIL } from "../../constants/urls/urlBackEnd";


export function profilGet(id) {
    return apiBackEnd.get(PROFIL + id);
}

