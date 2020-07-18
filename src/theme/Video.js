import React from "react";
import useBaseUrl from "@docusaurus/useBaseUrl";

const Source = props => <>
  <source src={useBaseUrl(props.src)} type="video/mp4" />
</>;

export class Video extends React.Component {
  constructor(props) {
    super(props);
    this.video = React.createRef();
  }

  componentDidMount() {
    this.video.current.play();
  }

  onEnded(e) {
    const video = this.video;

    if (this.props.timeout) {
      setTimeout(function () {
        video.current.play();
      }, this.props.timeout);
    } else {
      video.current.play();
    }
  }

  render() {
    const { src, ...rest } = this.props;

    return (
      <video
        ref={this.video}
        width="100%"
        onEnded={this.onEnded.bind(this)}
        muted
        {...rest}
      >
        <Source src={src} />
        Your browser does not support the video tag.
      </video>
    );
  }
}
