const ESLintPlugin = require("eslint-webpack-plugin");

module.exports = {
  devServer: {
    port: 8080,
  },
  configureWebpack: {
    plugins: [
      new ESLintPlugin({
        extensions: ["js", "ts", "tsx", "vue"],
        failOnError: true,
      }),
    ],
  },
};
