import axios from "axios";

const apiBackEnd = axios.create({
  baseURL: "https://localhost:7090",
});
export default apiBackEnd;
