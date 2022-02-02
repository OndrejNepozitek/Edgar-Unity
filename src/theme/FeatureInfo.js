import React from "react";
import Link from "@docusaurus/Link";

const features = [
  {
    id: "outline-override",
    url: "../basics/room-templates#outline-override",
    name: "Outline override",
    usages: [
      {
        id: "corridors",
        description: "Outline override is used to properly handle corridors",
      },
    ],
  },
  {
    id: "custom-rooms-and-connections",
    url: "../basics/level-graphs#pro-custom-rooms-and-connections",
    name: "Custom rooms",
    usages: [
      {
        id: "room-type",
        description: "The definition of rooms is enhanced with a custom type",
      },
    ],
  },
  {
    id: "custom-post-processing",
    url: "../generators/post-process#custom-post-processing",
    name: "Custom post-processing",
    usages: [
      {
        id: "enemies",
        description: "A custom post-processing task is used to spawn enemies after a level is generated",
      },
      {
        id: "player-spawn",
        description: "Move the player to the spawn position of the level",
      },
    ],
  },
];

const examples = [
  {
    id: "example-1",
    name: "Example 1",
    url: "../examples/example-1",
    features: [
      {
        id: "custom-post-processing",
        usage: "enemies",
        anchor: "enemies",
      },
    ],
  },
  {
    id: "example-2",
    name: "Example 2",
    url: "../examples/example-2",
    features: [
      {
        id: "outline-override",
        usage: "corridors",
        anchor: "vertical-corridors",
      },
    ],
  },
  {
    id: "dead-cells",
    name: "Dead Cells",
    url: "../examples/dead-cells",
    features: [
      {
        id: "custom-post-processing",
        usage: "enemies",
        anchor: "enemies",
      },
      {
        id: "custom-post-processing",
        usage: "player-spawn",
        anchor: "spawn-position",
      },
      {
        id: "custom-rooms-and-connections",
        usage: "room-type",
        anchor: "custom-room-and-connection-types",
      },
    ],
  },
];

function getUrl(path) {
    /*if (typeof window !== 'undefined') {
        const currentUrl = window.location.href;
        const splitBySlash = currentUrl.split("/");
        const lastSplit = splitBySlash[splitBySlash.length - 1];;
    
        if (lastSplit.startsWith("#") || currentUrl.endsWith("/")) {
            path = "../" + path;
        }
    }*/
    path = "../" + path;

    return path;
}

export const FeatureUsage = (props) => {
  const feature = features.find((x) => x.id === props.id);
  const usages = feature.usages.map((x) => ({ ...x, examples: [] }));

  examples.forEach((example) => {
    example.features.forEach((x) => {
      if (x.id === props.id) {
        const featureUsage = usages.find((y) => y.id === x.usage);
        featureUsage.examples.push({ example, feature: x });
      }
    });
  });

  return (
    <div className="featureUsage">
      <div className="featureUsage__title">Where is this feature used?</div>
      <div className="featureUsage__description">
        Below is the list of examples/tutorials where we use this feature. Feel
        free to check them out if you're not sure how something works or if you
        just want to see this feature used in action.
      </div>
      <table>
        <tbody>
          {usages.map((x, id) => (
            <tr key={id}>
              <td>
                {x.examples.map((example, index) => (
                  <span>
                    {index ? ", " : ""}
                    <Link
                      to={getUrl(example.example.url + "#" + example.feature.anchor)}
                    >
                      {example.example.name}
                    </Link>
                  </span>
                ))}
              </td>
              <td>{x.description}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export const ExampleFeatures = (props) => {
  const example = examples.find((x) => x.id === props.id);
  const usages = [];

  example.features.forEach((feature) => {
    const featureInfo = features.find((x) => x.id === feature.id);
    const featureUsage = featureInfo.usages.find((x) => x.id === feature.usage);
    usages.push({
      ...feature,
      feature: featureInfo,
      featureUsage,
    });
  });

  return (
    <div className="featureUsage">
      <div className="featureUsage__title">List of used features</div>
      <div className="featureUsage__description">
        Below is a list of features that are used in this example.
      </div>
      <table>
        <tbody>
          {usages.map((x, id) => (
            <tr key={id}>
              <td>
                <Link to={getUrl(x.feature.url)}>{x.feature.name}</Link>
              </td>
              <td>{x.featureUsage.description}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};
