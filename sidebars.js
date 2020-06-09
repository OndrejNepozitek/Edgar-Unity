/**
 * Copyright (c) 2017-present, Facebook, Inc.
 *
 * This source code is licensed under the MIT license found in the
 * LICENSE file in the root directory of this source tree.
 */

/*module.exports = {
  "docs": {
    "Introduction": ["introduction", "motivation", "installation"],
    "Basics": ["basics", "roomTemplates", "levelGraphs", "generatorSetup", "performanceTips"],
    "Examples": ["example1", "example2", "platformers"],
    "Generator pipeline": ["generatorPipeline", "pipelinePayload", "pipelineTasks", "runPipeline"],
    "Pipeline tasks": ["fixedInput", "graphBasedGenerator", "payloadInitializer"],
    "Guides": ["addingDoorTiles", "proceduralLevelGraphs", "corridorsCorrection"],
    "Other": ["tilemapLayers"]
  },
}
;*/

module.exports = {
  docs: [
    {
      type: "category",
      label: "Introduction",
      items: ["introduction", "motivation", "installation"]
    },
    {
      type: "category",
      label: "Basics",
      items: ["basics/introduction", "basics/room-templates", "basics/level-graphs", "basics/generator-setup", "basics/generated-level-info", "basics/performance-tips"]
    },
    {
      type: "category",
      label: "Generators",
      items: ["generators/dungeon-generator", "generators/platformer-generator", "generators/custom-input", "generators/post-process"]
    },
    {
      type: "category",
      label: "Examples",
      items: ["examples/example-1", "examples/example-2", "examples/platformer-1", "examples/isometric-1", "examples/enter-the-gungeon", "examples/dead-cells"]
    },
    {
      type: "category",
      label: "Guides",
      items: ["guides/room-template-customization", "guides/current-room-detection", "guides/procedural-level-graphs", "guides/fog-of-war", "guides/benchmarks"]
    },
    {
      type: "category",
      label: "Other",
      items: ["other/migration-v1-v2"]
    }
  ]
};
