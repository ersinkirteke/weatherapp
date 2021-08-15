module.exports = {
  devServer: {
    proxy: `${process.env.VUE_APP_DEVSERVER_PROXY}`,
  },
};
