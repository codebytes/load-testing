const js = require("@eslint/js");
const vue = require("eslint-plugin-vue");
const ts = require("typescript-eslint");
const prettier = require("eslint-plugin-prettier/recommended");

module.exports = ts.config(
  { ignores: ["dist/**", "node_modules/**"] },
  js.configs.recommended,
  ...ts.configs.recommended,
  ...vue.configs["flat/essential"],
  prettier,
  {
    languageOptions: {
      globals: {
        module: "readonly",
        require: "readonly",
        process: "readonly",
        console: "readonly",
      },
      parserOptions: { parser: ts.parser },
    },
    rules: {
      "no-console": process.env.NODE_ENV === "production" ? "warn" : "off",
      "no-debugger": process.env.NODE_ENV === "production" ? "warn" : "off",
      "vue/multi-word-component-names": "off",
      "@typescript-eslint/no-explicit-any": "off",
    },
  },
  {
    files: ["**/*.js"],
    rules: { "@typescript-eslint/no-require-imports": "off" },
  },
);
