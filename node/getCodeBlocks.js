const fs = require("fs");
const path = require("path");
const stripIndent = require("strip-indent");
const edgarPath = "C:/Users/ondra/Projects/Unity/Edgar-Unity/Assets/Edgar";
const startOfRegionSequence = "#region";
const startOfBlockSequence = "#region codeBlock:";
const endOfRegionSequence = "#endregion";
const myArgs = process.argv.slice(2);
const version = myArgs[0] || 'Next';

let outputPath = path.join(__dirname, "..", "docs", "code");
if (version !== 'Next') {
  outputPath = path.join(__dirname, "..", "versioned_docs", "version-" + version, "code");
}

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

function parseFile(filename, content) {
  const codeBlocks = [];
  const lines = content.split("\r\n");

  for (const line of lines) {
    const trimmedLine = line.trim();
    if (trimmedLine.startsWith(startOfBlockSequence)) {
      const blockName = trimmedLine.substring(startOfBlockSequence.length);
      codeBlocks.push({
        name: blockName,
        lines: [],
      });

      continue;
    } else if (trimmedLine.startsWith(startOfRegionSequence)) {
      codeBlocks.push({
        isPlaceholder: true,
      });
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

    for (const codeBlock of codeBlocks) {
      if (!codeBlock.isPlaceholder) {
        codeBlock.lines.push(line);
      }
    }
  }
}

fs.rmSync(outputPath, { recursive: true, force: true });
fs.mkdirSync(outputPath, { recursive: true })
readCsharpFiles(edgarPath, parseFile, (err) => console.log(err));