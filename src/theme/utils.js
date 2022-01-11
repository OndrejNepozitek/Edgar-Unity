import React from "react";

export const Path = props => {
    let { path, type, par } = props;
    const colonIndex = path.indexOf(':');

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

    const parts = path.split('/');
    let result = parts.join(' \u2192 ');

    if (par) {
        result = '(' + result + ')';
    }

    return <i className="path">{result}</i>
};

Path.defaultProps = {
    par: false,
}