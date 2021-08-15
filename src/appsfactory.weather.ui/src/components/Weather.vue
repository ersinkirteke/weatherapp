<template>
  <div id="weather">
    <h1>Search</h1>
    <div>
      <span id="result"></span>
    </div>
    <div>
      <input type="text" id="city" placeholder="Filter By City" />
      <button id="searchcity" class="search-button" v-on:click="onCitySearch">
        Search
      </button>
      <input
        type="text"
        class="zipcode"
        id="zipCode"
        placeholder="Filter By Zip Code"
      />
      <button id="searchzip" class="search-button" v-on:click="onZipCodeSearch">
        Search
      </button>
    </div>
  </div>
  <ag-grid-vue
    style="width: 650px; height: 330px"
    class="ag-theme-alpine"
    id="weathergrid"
    :columnDefs="columnWeatherDefs"
    :rowData="rowWeatherData"
  >
  </ag-grid-vue>
  <h1 class="header-weather">History</h1>
  <ag-grid-vue
    style="width: 1150px; height: 600px; margin-top: 30px"
    class="ag-theme-alpine"
    id="historygrid"
    :columnDefs="columnDefs"
    :rowData="rowData"
  >
  </ag-grid-vue>
</template>

<script>
import { AgGridVue } from "ag-grid-vue3";

export default {
  name: "Weather",
  data() {
    return {
      columnWeatherDefs: null,
      rowWeatherData: null,
      rowData: null,
      columnDefs: null,
    };
  },
  components: {
    AgGridVue,
  },
  beforeMount() {
    this.columnWeatherDefs = [
      { field: "avgTemperature" },
      { field: "avgHumidity" },
      { field: "forecastDate" },
    ];

    this.columnDefs = [
      { field: "temperature", width: 150 },
      { field: "humidity", width: 100 },
      { field: "windSpeed", width: 150 },
      { field: "city", width: 150 },
      { field: "zipCode", width: 150 },
      { field: "forecastDate", width: 200 },
      { field: "createdDate", width: 200 },
    ];

    this.rowWeatherData = [];
    this.rowData = [];
    this.fetchHistory();
  },
  methods: {
    onCitySearch() {
      let city = document.getElementById("city").value;
      let result = document.getElementById("result");

      if (!city) return;

      fetch(`${process.env.VUE_APP_CITY_SEARCH_URL}${city}`)
        .then((resp) => resp.json())
        .then((data) => {
          document.getElementById("zipCode").value = "";
          if (data.forecasts) {
            this.rowWeatherData = data.forecasts;
            result.innerHTML = "";
            this.fetchHistory();
          } else {
            result.innerHTML = "No Result";
            this.rowWeatherData = [];
          }
        });
    },
    onZipCodeSearch() {
      let zipCode = document.getElementById("zipCode").value;
      let result = document.getElementById("result");

      if (!zipCode) return;

      fetch(`${process.env.VUE_APP_ZIPCODE_SEARCH_URL}${zipCode}`)
        .then((resp) => resp.json())
        .then((data) => {
          document.getElementById("city").value = "";
          if (data.forecasts) {
            this.rowWeatherData = data.forecasts;
            result.innerHTML = "";
            this.fetchHistory();
          } else {
            result.innerHTML = "No Result";
            this.rowWeatherData = [];
          }
        });
    },
    fetchHistory() {
      fetch(`${process.env.VUE_APP_HISTORY_URL}`)
        .then((resp) => resp.json())
        .then((data) => {
          if (data) this.rowData = data;
        });
    },
  },
};
</script>

<style lang="scss">
@import "../../node_modules/ag-grid-community/dist/styles/ag-grid.css";
@import "../../node_modules/ag-grid-community/src/styles/ag-theme-alpine/sass/ag-theme-alpine-mixin.scss";

.ag-theme-alpine {
  @include ag-theme-alpine(
    (
      odd-row-background-color: #ace,
    )
  );
}

#weather {
  h1 {
    text-align: left;
  }
  #result {
    color: red;
  }
  div {
    margin-bottom: 10px;
  }
}

#weather {
  .search-button {
    margin-left: 20px;
  }
  .zipcode {
    margin-left: 20px;
  }
}
</style>
