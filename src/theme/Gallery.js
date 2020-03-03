import React from "react";
import useBaseUrl from "@docusaurus/useBaseUrl";

const gutter = 2;
const StyledImage = props => (
  <div
    style={{
      display: "inline-block",
      margin: gutter,
      overflow: "hidden",
      position: "relative",
      width: `calc(${100 / props.cols}% - ${gutter * 2}px)`,
      verticalAlign: "top"
    }}
  >
    {props.children}
  </div>
);

export const Gallery = props => (
  <div style={{ fontSize: "0px", margin: "20px 0" }}>
    {React.Children.map(props.children, child =>
      React.cloneElement(child, {
        cols: props.cols || 4,
        fixedHeight: props.fixedHeight
      })
    )}
  </div>
);

export const GalleryImage = props => (
  <StyledImage cols={props.cols}>
    <a href={useBaseUrl(props.src)} target="_blank">
      <img
        src={useBaseUrl(props.src)}
        alt="result"
        style={{
          height: props.fixedHeight === true ? "400px" : "auto",
          objectFit: props.fixedHeight === true ? "cover" : "initial"
        }}
      />
    </a>
    {props.caption !== undefined && <Caption>{props.caption}</Caption>}
  </StyledImage>
);

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
  const { src, caption, ...otherProps } = props;

  return (
    <div style={{ textAlign: "center" }}>
      <img src={useBaseUrl(props.src)} {...otherProps} />
      {props.caption !== undefined && <Caption>{props.caption}</Caption>}
    </div>
  );
};
