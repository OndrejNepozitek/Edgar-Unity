module.exports = function (context, options) {
  return {
    name: "custom-plugin",
    injectHtmlTags() {
      return {};
    },
  };
};
