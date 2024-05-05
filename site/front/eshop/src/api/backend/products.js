import { PRODUCTS } from "../../constants/urls/urlBackEnd";
import apiBackEnd from "./api.Backend";


export function products() {
  return apiBackEnd.get(PRODUCTS);
}

// export function getDestination(id) {
//   return apiBackEnd.get(PRODUCTS + id);
// }

// export function putDestination(id, values, token) {
//   return apiBackEnd.put(PRODUCTS + id, values, {headers: {"Authorization" : `Bearer ${token}`}});
// }

// export function putDestination(id, values) {
//   return apiBackEnd.put(URL_BACK_DESTINATIONS + id, values);
// }


// export function insertDestination(values, token) {
//   return apiBackEnd.post(PRODUCTS, values, {headers: {"Authorization" : `Bearer ${token}`}});
// }
