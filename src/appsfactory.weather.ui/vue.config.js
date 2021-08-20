module.exports = {
  devServer: {
    proxy: {
      "/api/weather": {
        target: process.env.VUE_APP_DEVSERVER_WEATHER_API_PROXY,
        pathRewrite: { '^/api/weather': '' },
      },
      "/api/identity": {
        target: process.env.VUE_APP_DEVSERVER_IDENTITY_API_PROXY,
        pathRewrite: { '^/api/identity': '' },
      },
    },
  },
};
