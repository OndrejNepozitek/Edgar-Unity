const yaml = require('js-yaml');
const fs   = require('fs');

const apiReferenceBaseUrl = "https://ondrejnepozitek.github.io/ProceduralLevelGenerator-UnityApiDocs/";

function getBookmarks() {
    const bookmarks = {};
    const doc = yaml.safeLoad(fs.readFileSync('apiUrls_dev.yaml', 'utf8'));
    doc.references.forEach(function(element){
        var absoluteUrl = apiReferenceBaseUrl + element.href.replace("dev/", "master/");
        bookmarks[element.nameWithType] = absoluteUrl;
        bookmarks[element.nameWithType + "#properties"] = absoluteUrl + "#properties";
        bookmarks[element.nameWithType + "#methods"] = absoluteUrl + "#methods";
        bookmarks[element.nameWithType + "#fields"] = absoluteUrl + "#fields";
    });

    return bookmarks;
}

module.exports = getBookmarks;