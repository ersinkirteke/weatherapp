module.exports = {
  devServer: {
    proxy: {
      "/one": {
        target: process.env.VUE_APP_DEVSERVER_WEATHER_API_PROXY,
        pathRewrite: { "^/one": "" },
      },
      "/two": {
        target: process.env.VUE_APP_DEVSERVER_IDENTITY_API_PROXY,
        pathRewrite: { "^/two": "" },
      },
    },
  },
};
