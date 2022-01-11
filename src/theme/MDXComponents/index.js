import React from "react";
import MDXComponents from "@theme-original/MDXComponents";
import { Image, Gallery, GalleryImage } from "@theme/Gallery";
import { Path } from "@theme/utils";
import { FeatureUsage, ExampleFeatures } from "@theme/FeatureInfo";

MDXComponents.Image = Image;
MDXComponents.Gallery = Gallery;
MDXComponents.GalleryImage = GalleryImage;
MDXComponents.Path = Path;
MDXComponents.FeatureUsage = FeatureUsage;
MDXComponents.ExampleFeatures = ExampleFeatures;

export default MDXComponents;