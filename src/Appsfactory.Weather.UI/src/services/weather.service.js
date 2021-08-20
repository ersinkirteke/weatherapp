import axios from "axios";
import authHeader from "./auth-header";

class WeatherService {
  getByCity(city) {
    return axios.get(process.env.VUE_APP_Forecast_URL + "?city=" + city, {
      headers: authHeader(),
    });
  }

  getByZipCode(zipCode) {
    return axios.get(process.env.VUE_APP_Forecast_URL + "?zipCode=" + zipCode, {
      headers: authHeader(),
    });
  }

  getHistory() {
    return axios.get(process.env.VUE_APP_HISTORY_URL, {
      headers: authHeader(),
    });
  }
}

export default new WeatherService();
