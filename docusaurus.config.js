const versions = require('./versions.json');
const getBookmarks = require("./src/bookmarks")
const [latestVersion] = require('./versions.json');
const remarkBookmarks = require('remark-bookmarks')
const remarkHelpers = require('@ondrej-nepozitek/remark-helpers/images');
const path = require('path');
const archiveVersions = [
  "2.0.0-alpha.9", "2.0.0-alpha.8", "2.0.0-alpha.7"
]


module.exports = {
  title: "Edgar - Unity",
  tagline:
    "Configurable 2D procedural level generator for Unity",
  url: "https://ondrejnepozitek.github.io",
  baseUrl: "/Edgar-Unity/",
  favicon: "img/favicon.ico",
  organizationName: "OndrejNepozitek", // Usually your GitHub org/user name.
  projectName: "Edgar-Unity", // Usually your repo name.
  trailingSlash: true,
  onBrokenLinks: "warn",
  onBrokenMarkdownLinks: "error",
  themeConfig: {
    algolia: {
      apiKey: 'b81526b8f4babcdebfa613315ee05014',
      indexName: 'edgar-unity',
      searchParameters: {
        facetFilters: [`version:${versions[0]}`],
      },
    },
    announcementBar: {
      id: 'support_us',
      content:
        'Check out the PRO version of the generator on the <a href="https://url.ondrejnepozitek.com/edgar-docs" target="_blank">Unity Asset Store</a>!',
      backgroundColor: '#fafbfc', // Defaults to `#fff`.
      textColor: '#091E42', // Defaults to `#000`.
    },
    navbar: {
      hideOnScroll: false,
      title: "Edgar - Unity",
      items: [
        { 
          to: "docs/introduction", 
          label: `v${latestVersion}`,
          position: "left" 
        },
        { 
          to: "docs/introduction", 
          label: `Docs`,
          position: "right" 
        },
        {
          label: 'Archive',
          position: 'right',
          activeBaseRegex: `docs/(?!next/(support|team|resources))`,
          items: [
            {
              label: versions[0],
              to: 'docs/introduction',
              activeBaseRegex: `docs/(?!${versions.join('|')}|next)`,
            },
            ...versions.slice(1, 3).map((version) => ({
              label: version,
              to: `docs/${version}/introduction`,
            })),
            {
              label: 'Master/Unreleased',
              to: 'docs/next/introduction',
              activeBaseRegex: `docs/next/(?!support|team|resources)`,
            },
          ],
        },
        {
          label: 'Docs (3D version beta)',
          to: 'docs/3d/introduction',
          position: 'right',
          activeBaseRegex: `docs/(?!next/(support|team|resources))`,
          items: [
            {
              label: versions[0],
              to: 'docs/3d/introduction',
              activeBaseRegex: `docs/(?!${versions.join('|')}|next)`,
            },
            ...versions.slice(1, 3).map((version) => ({
              label: version,
              to: `docs/3d/${version}/introduction`,
            })).filter(x => !["2.0.0-beta.0", "2.0.0", "2.0.2"].includes(x.label)),
            {
              label: 'Master/Unreleased',
              to: 'docs/next/3d/introduction',
              activeBaseRegex: `docs/next/(?!support|team|resources)`,
            },
          ],
        },
        {
          href: "https://github.com/OndrejNepozitek/Edgar-Unity/",
          label: "GitHub", 
          position: "right"
        },
        {
          href: "https://url.ondrejnepozitek.com/edgar-docs",
          label: "Buy Edgar PRO", 
          position: "left"
        }
      ]
    },
    prism: {
      defaultLanguage: "csharp",
      theme: require("./src/theme/prism-darcula"),
      additionalLanguages: ['csharp'],
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
            {
              label: "Discord",
              href: "https://discord.gg/syktZ6VWq9"
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
              href: "https://github.com/OndrejNepozitek/Edgar-Unity"
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
          sidebarCollapsible: false,
          sidebarPath: require.resolve("./sidebars.js"),
          editUrl: "https://github.com/OndrejNepozitek/Edgar-Unity/tree/docusaurus",
          beforeDefaultRemarkPlugins: [
            [
              remarkHelpers,
              {

              }
            ]
          ],
          remarkPlugins: [
            [
              remarkBookmarks, { 
                bookmarks: getBookmarks(),
              }
            ]
          ],
        },
        theme: {
          customCss: require.resolve("./src/css/custom.css"),
        },
        googleAnalytics: {
          trackingID: 'UA-31904365-17',
        },
      }
    ]
  ],
  plugins: [require.resolve("./src/customPlugin.js")],
};
