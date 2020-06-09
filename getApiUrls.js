const https = require('https');
const fs = require('fs');

function downloadUrls(branch) {
    const file = fs.createWriteStream("apiUrls_" + branch + ".yaml");
    const request = https.get("https://ondrejnepozitek.github.io/ProceduralLevelGenerator-UnityApiDocs/" + branch + ".xrefmap.yml", function(response) {
      response.pipe(file);
    });
}

downloadUrls("master")
downloadUrls("dev")