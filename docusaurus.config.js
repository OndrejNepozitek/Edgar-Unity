const [latestVersion] = require('./versions.json');

module.exports = {
  title: "Procedural level generator - Unity",
  tagline:
    "A configurable Unity plugin for procedural generation of 2D dungeons",
  url: "https://ondrejnepozitek.github.io",
  baseUrl: "/ProceduralLevelGenerator-Unity/",
  favicon: "img/favicon.ico",
  organizationName: "OndrejNepozitek", // Usually your GitHub org/user name.
  projectName: "ProceduralLevelGenerator-Unity", // Usually your repo name.
  themeConfig: {
    navbar: {
      title: "Procedural level generator - Unity",
      links: [
        { to: "versions", label: `v${latestVersion}`, position: "left" },
        { to: "docs/introduction", label: "Docs", position: "right" },
        {
          href: "https://github.com/OndrejNepozitek/ProceduralLevelGenerator-Unity/",
          label: "GitHub", 
          position: "right"
        }
      ]
    },
    prism: {
      defaultLanguage: "csharp",
      theme: require("./src/theme/prism-darcula")
    },
    footer: {
      style: "dark",
      links: [
        {
          title: "Docs",
          items: [
            {
              label: "Introduction",
              to: "docs/introduction"
            },
          ]
        },
        {
          title: "Community",
          items: [
            {
              label: "Twitter",
              href: "https://twitter.com/OndrejNepozitek"
            },
          ]
        },
        {
          title: "Social",
          items: [
            {
              label: "Blog",
              href: "https://ondra.nepozitek.cz/blog/"
            },
            {
              label: "GitHub",
              href: "https://github.com/OndrejNepozitek/ProceduralLevelGenerator-Unity"
            },
          ]
        }
      ],
      copyright: "Copyright © " + new Date().getFullYear() + " Ondřej Nepožitek"
    }
  },
  presets: [
    [
      "@docusaurus/preset-classic",
      {
        docs: {
          sidebarPath: require.resolve("./sidebars.js"),
          editUrl: "https://github.com/OndrejNepozitek/ProceduralLevelGenerator-Unity/tree/docusaurus"
        },
        theme: {
          customCss: require.resolve("./src/css/custom.css")
        }
      }
    ]
  ]
};
