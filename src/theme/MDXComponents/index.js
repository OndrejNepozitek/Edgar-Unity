import React from "react";
import MDXComponents from "@theme-original/MDXComponents";
import { Image, Gallery, GalleryImage } from "@theme/Gallery";

MDXComponents.Image = Image;
MDXComponents.Gallery = props => <Gallery cols="2" {...props} />;
MDXComponents.GalleryImage = GalleryImage;

export default MDXComponents;