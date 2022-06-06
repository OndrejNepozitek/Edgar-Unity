import React from "react";
import MDXComponents from "@theme-original/MDXComponents";
import { Image, Gallery, GalleryImage } from "@theme/Gallery";
import { Path, Difference2D3D } from "@theme/utils";
import { FeatureUsage, ExampleFeatures } from "@theme/FeatureInfo";
import ExternalCode from "@theme/ExternalCode";

MDXComponents.Image = Image;
MDXComponents.Gallery = Gallery;
MDXComponents.GalleryImage = GalleryImage;
MDXComponents.Path = Path;
MDXComponents.FeatureUsage = FeatureUsage;
MDXComponents.ExampleFeatures = ExampleFeatures;
MDXComponents.ExternalCode = ExternalCode;
MDXComponents.Difference2D3D = Difference2D3D;

export default MDXComponents;