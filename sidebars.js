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
      items: ["basics/introduction", "basics/room-templates", "basics/level-graphs", "basics/generator-setup", "basics/performance-tips"]
    },
    {
      type: "category",
      label: "Examples",
      items: ["examples/example-1", "examples/example-2", "examples/platformers"]
    },
    {
      type: "category",
      label: "Generator pipeline",
      items: ["generator-pipeline/introduction", "generator-pipeline/pipeline-payload", "generator-pipeline/pipeline-tasks", "generator-pipeline/run-pipeline"]
    },
    {
      type: "category",
      label: "Pipeline tasks",
      items: ["pipeline-tasks/fixed-input", "pipeline-tasks/graph-based-generator", "pipeline-tasks/payload-initializer"]
    },
    {
      type: "category",
      label: "Guides",
      items: ["guides/adding-door-tiles", "guides/procedural-level-graphs", "guides/corridors-correction"]
    },
    {
      type: "category",
      label: "Other",
      items: ["other/tilemap-layers"]
    }
  ]
};
