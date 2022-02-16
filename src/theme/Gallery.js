import React from "react";
import useBaseUrl from "@docusaurus/useBaseUrl";
import logger from '@docusaurus/logger';
import { requireVersionedAsset, getImageSize } from '@theme/utils';

const gutter = 2;
const StyledImage = props => (
  <div
    style={{
      display: "inline-block",
      margin: gutter,
      overflow: "hidden",
      position: "relative",
      width: `calc(${100 / props.cols}% - ${gutter * 2}px)`,
      verticalAlign: "top",
      textAlign: "center",
    }}
  >
    {props.children}
  </div>
);

export const Gallery = props => (
  <div style={{ fontSize: "0px", margin: "20px 0" }}>
    {React.Children.map(props.children, child =>
      React.cloneElement(child, {
        cols: props.cols,
        fixedHeight: props.fixedHeight,
        isInsideGallery: true,
      })
    )}
  </div>
);

Gallery.defaultProps = {
    cols: 2,
    fixedHeight: true,
}

function getUrl(src, isGlobal) {
  try {
    return isGlobal ? useBaseUrl(src) : requireVersionedAsset(src);
  } catch (e) {
    logger.error('Image error: ' + e.message);
    return 'http://placehold.jp/150x150.png';
  }
}

export const GalleryImage = props => (
  <StyledImage cols={props.cols}>
    <a href={getUrl(props.src, props.isGlobal)} target="_blank">
      <img
        src={getUrl(props.src, props.isGlobal)}
        alt="image"
        width={props.width}
        height={props.height}
        style={{
          height: props.fixedHeight === true ? (800 / props.cols) + "px" : "auto",
          objectFit: props.fixedHeight === true ? "cover" : "initial",
        }}
      />
    </a>
    {props.caption !== undefined && <Caption>{props.caption}</Caption>}
  </StyledImage>
);

GalleryImage.defaultProps = {
  isGlobal: false,
}

const Caption = props => (
  <div
    style={{
      fontSize: 16,
      fontStyle: "italic",
      textAlign: "center",
      margin: "10px 0px 15px"
    }}
  >
    {props.children}
  </div>
);

export const Image = props => {
  const { src, caption, isInsideGallery, obsolete, width, height, ...otherProps } = props;

  let size = {
    width: "auto",
    height: "auto",
  }

  if (!props.isGlobal) {
    const computedSize = getImageSize(src);
    size = Object.assign(size, {
      width: computedSize.width,
      height: computedSize.height,
    });
    const ratio = size.width / size.height;

    if (height) {
      size.height = height;
      size.width = height * ratio;
    }

    if (width) {
      size.width = width;
      size.height = width / ratio;
    }
  }

  if (isInsideGallery) {
    return <GalleryImage {...size} {...props} />
  }

  return (
    <div style={{ textAlign: "center" }}>
      <div className="image-inner">
        <img src={getUrl(src, props.isGlobal)} {...size} {...otherProps} />
        {obsolete && <div className="obsolete-image">Note: This image was taken in an older version of the generator. Details may differ.</div>}
      </div>
      {props.caption !== undefined && <Caption>{props.caption}</Caption>}
    </div>
  );
};

Image.defaultProps = {
  isGlobal: false,
}