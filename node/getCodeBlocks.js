/*

The goal of this file is to locate the source code of Edgar and find
all the marked code blocks. For each such code blocks, one txt file
is created.

A version positional argument can be given, default is the "Next" version.
Example format "2.0.0-beta.0".

*/
const edgarPath = "C:/Users/ondra/Projects/Unity/Edgar-Unity/Assets/Edgar";
const fs = require("fs");
const path = require("path");
const stripIndent = require("strip-indent");
const startOfRegionSequence = "#region";
const startOfBlockSequence = "#region codeBlock:";
const startOfHidden = "#region hide"
const endOfRegionSequence = "#endregion";
const myArgs = process.argv.slice(2);
const version = myArgs[0] || 'Next';

// Usage: npm run codeBlocks -- --keep-old
const keepOldFiles = myArgs.some(x => x === "--keep-old");

console.log(myArgs);

let outputPath = path.join(__dirname, "..", "docs", "code");
if (version !== 'Next' && !version.startsWith("--")) {
  const outputPathRoot = path.join(__dirname, "..", "versioned_docs", "version-" + version);

  if (!fs.existsSync(outputPathRoot)) {
    throw `Version ${version} is not recognized!`;
  }

  outputPath = path.join(outputPathRoot, "code");
}

// Read all csharp files in a given directory
function readCsharpFiles(dirname, onFileContent, onError) {
  fs.readdir(dirname, function (err, filenames) {
    if (err) {
      onError(err);
      return;
    }
    filenames.forEach(function (filename) {
      const filePath = path.join(dirname, filename);

      if (fs.lstatSync(filePath).isDirectory()) {
        readCsharpFiles(filePath, onFileContent, onError);
        return;
      }

      if (!filePath.endsWith(".cs")) {
        return;
      }

      fs.readFile(filePath, 'utf-8', function (err, content) {
        if (err) {
          onError(err);
          return;
        }
        onFileContent(filename, content);
      });
    });
  });
}

// Find all marked code blocks in a given file
function parseFile(filename, content) {
  const codeBlocks = [];
  const lines = content.split("\r\n");

  // Go through all the lines in the file, looking for #region and #endregion blocks
  for (const line of lines) {
    const trimmedLine = line.trimStart();    

    // Look for "#region codeBlock:codeBlockName"
    if (trimmedLine.startsWith(startOfBlockSequence)) {
      const blockName = trimmedLine.substring(startOfBlockSequence.length);
      codeBlocks.push({
        name: blockName,
        lines: [],
      });
      continue;
    // Look for "#region hide"
    } else if (trimmedLine.startsWith(startOfHidden)) {
      codeBlocks.push({
        isPlaceholder: true,
        isHidden: true,
      });

      const prefix = line.substring(0, line.length - trimmedLine.length);
      const substitute = "/* ... */"

      for (const codeBlock of codeBlocks) {
        if (!codeBlock.isPlaceholder) {
          codeBlock.lines.push(prefix + substitute);
        }
      }

      continue;
    // Look for "#region"
    } else if (trimmedLine.startsWith(startOfRegionSequence)) {
      codeBlocks.push({
        isPlaceholder: true,
      });
    // Look for "#endregion", create a new file for the block
    } else if (trimmedLine.startsWith(endOfRegionSequence)) {
      const codeBlock = codeBlocks.pop();

      if (!codeBlock.isPlaceholder) {
        console.log(codeBlock.name);
        const codeBlockPath = path.join(outputPath, codeBlock.name + ".txt");
        const codeBlockContent = stripIndent(codeBlock.lines.join("\r\n"));
        fs.writeFileSync(codeBlockPath, codeBlockContent);
      }

      continue;
    }

    // Ignore the current line if there is a hidden region
    let isHidden = false;
    for (const codeBlock of codeBlocks) {
      if (codeBlock.isHidden) {
        isHidden = true;
      }
    }
    if (isHidden) {
      continue;
    }

    // Otherwise append the line to all the regions
    for (const codeBlock of codeBlocks) {
      if (!codeBlock.isPlaceholder) {
        codeBlock.lines.push(line);
      }
    }
  }
}

if (!keepOldFiles) {
  fs.rmSync(outputPath, { recursive: true, force: true });
  fs.mkdirSync(outputPath, { recursive: true })
}

readCsharpFiles(edgarPath, parseFile, (err) => console.log(err));