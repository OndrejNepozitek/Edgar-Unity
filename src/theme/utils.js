import React from "react";
import {useActiveVersion} from '@docusaurus/plugin-content-docs/client';

export const Path = props => {
    let { path, type, par } = props;
    const colonIndex = path.indexOf(':');
    let joinSequence = ' \u2192 ';

    if (colonIndex !== -1) {
        type = path.substring(0, colonIndex);
        path = path.substring(colonIndex + 1);
    }

    if (type === '2d') {
        path = 'Create/Edgar/' + path;
    }

    if (type === '3d') {
        path = 'Create/Edgar (Grid3D)/' + path;
    }

    if (type === '2de') {
        path = 'Examples/Grid2D/' + path;
        joinSequence = '/';
    }

    if (type === '3de') {
        path = 'Examples/Grid3D/' + path;
        joinSequence = '/';
    }

    const parts = path.split('/');
    let result = parts.join(joinSequence);

    if (par) {
        result = '(' + result + ')';
    }

    return <i className="path">{result}</i>
};

Path.defaultProps = {
    par: false,
}

function importAll(r) {
    r.keys().forEach(r);
}

const nextImages = require.context(
    '@site/docs/',
    true,
    /(\.png|\.gif)$/
);

const versionedImages = require.context(
    '@site/versioned_docs/',
    true,
    /(\.png|\.gif)$/
);

const nextImagesSizes = require.context(
    '!!image-dimensions-loader!@site/docs/',
    true,
    /(\.png|\.gif)$/
);

const versionedImagesSizes = require.context(
    '!!image-dimensions-loader!@site/versioned_docs//',
    true,
    /(\.png|\.gif)$/
);

export function requireVersionedAsset(src) {
    const activeVersion = useActiveVersion('default');
    const label = activeVersion.label;

    if (label === 'Next') {
        return nextImages('./assets/' + src).default;
    } else {
        const path = `./version-${label}/assets/` + src;
        return versionedImages(path).default;
    }
}

export function getImageSize(src) {
    const activeVersion = useActiveVersion('default');
    const label = activeVersion.label;

    if (label === 'Next') {
        return nextImagesSizes('./assets/' + src);
    } else {
        const path = `./version-${label}/assets/` + src;
        return versionedImagesSizes(path);
    }
}

export function requireVersionedCode(name) {
    const activeVersion = useActiveVersion('default');
    const label = activeVersion.label;

    if (label === 'Next') {
        const path = 'code/' + name;
        return require('!!raw-loader!@site/docs/' + path + '.txt').default;
    } else {
        const path = `version-${label}/code/` + name
        return require('!!raw-loader!@site/versioned_docs/' + path + '.txt').default;
        // throw 'Uncomment above when at least a single version exists'
    }
}

export function Difference2D3D() {
    return (
        <blockquote>
            <b>2D vs 3D docs:</b> In order to keep the docs maintainable and up-to-date, I decided to have parts of the documentation only available for the 2D version of Edgar. This happens only when the concepts are very similar in both versions.
            <br /><br />
            As a rule of thumb, if you see a class with the <code>Grid2D</code> suffix, there is a high chance that you can replace the suffix with <code>Grid3D</code> to get a class withs a very similar API.
            <br /><br />
            If you have any questions regarding the 3D version od Edgar, come to our Discord or contact me directly via email (<i>ondra@nepozitek.cz</i>).
        </blockquote>
    );
}