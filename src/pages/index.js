import React from "react";
import classnames from "classnames";
import Layout from "@theme/Layout";
import Link from "@docusaurus/Link";
import {Video} from "@theme/Video";
import useDocusaurusContext from "@docusaurus/useDocusaurusContext";
import useBaseUrl from "@docusaurus/useBaseUrl";
import styles from "./styles.module.css";

const features = [
  {
    title: <>Easy to use</>,
    description: (
      <>
        Well, it's not a one-click setup, but after you read about the basic concepts of the generator, you should be ready to easily generate your first level. 
      </>
    ),
  },
  {
    title: <>Packed with features</>,
    description: (
      <>
        The generator gives you complete control over the structure of levels and look of individual rooms. Generate dungeons, platformers or even isometric levels.
      </>
    ),
  },
  {
    title: <>Easy to customize</>,
    description: (
      <>
        It's easy to customize generated levels. Add enemies, treasures, secret rooms. Almost anything is possible!
      </>
    ),
  },
];

function Feature({ imageUrl, title, description }) {
  const imgUrl = useBaseUrl(imageUrl);
  return (
    <div className={classnames("col col--4", styles.feature)}>
      {imgUrl && (
        <div className="text--center">
          <img className={styles.featureImage} src={imgUrl} alt={title} />
        </div>
      )}
      <h3>{title}</h3>
      <p className="text--justify">{description}</p>
    </div>
  );
}

function Home() {
  const context = useDocusaurusContext();
  const { siteConfig = {} } = context;
  return (
    <Layout title={`${siteConfig.title}`} description={`${siteConfig.tagline}`}>
      <header className={classnames("hero hero--primary", styles.heroBanner)}>
        <div className="container">
          <h1 className="hero__title">{siteConfig.title}</h1>
          <p className="hero__subtitle">{siteConfig.tagline}</p>
          <div className={styles.buttons}>
            <Link
              className={classnames(
                "button button--outline button--secondary button--lg",
                styles.getStarted
              )}
              to={useBaseUrl("docs/introduction")}
            >
              Get Started
            </Link>
          </div>
        </div>
      </header>
      <main>
        {features && features.length && (
          <section className={classnames(
            "features",
            styles.features
          )}>
            <div className="container">
              <div className="row">
                {features.map((props, idx) => (
                  <Feature key={idx} {...props} />
                ))}
              </div>
            </div>
          </section>
        )}
        <div className="quickOverview__outter">
          <div className="container quickOverview">
            <div className="row">
              <div className="col col--5 quickOverview__description">
                <h2>1. Create room templates</h2>
                <p>
                  The first step is to create so-called room templates which
                  describe how individual rooms look and how they can be
                  connected to each other. The basic visuals are created with
                  Unity Tilemaps but it is also possible to include game objects
                  like enemies, loot, etc.
                </p>
              </div>
              <div className="col col--6 col--offset-1">
                <Video src="videos/room_template.mp4" timeout={5000} />
              </div>
            </div>
          </div>
        </div>

        <div className="quickOverview__outter">
          <div className="container quickOverview">
            <div className="row">
              <div className="col col--6">
                <Video src="videos/level_graph.mp4" timeout={5000} />
              </div>
              <div className="col col--5 col--offset-1 quickOverview__description">
                <h2>2. Describe level structure</h2>
                <p>
                  The second step is to create a so-called level graph. The
                  generator comes with a graph editor where you can specify how
                  many rooms you want and how they are connected. You can also
                  choose that some rooms will have different room templates than
                  other rooms - e.g. a spawn room will look different than a
                  boss room. Just make sure that the graph is not too
                  big/complex.
                </p>
              </div>
            </div>
          </div>
        </div>
        <div className="quickOverview__outter">
          <div className="container quickOverview">
            <div className="row">
              <div className="col col--5 quickOverview__description">
                <h2>3. Generate levels</h2>
                <p>
                  The last step is to add the generator component to a game
                  object, assign the level graph from the previous step and you
                  are ready to generate levels.
                </p>
              </div>
              <div className="col col--6 col--offset-1">
                <Video src="videos/levels.mp4" timeout={5000} />
              </div>
            </div>
          </div>
        </div>

        {/*features && features.length && (
          <section className={styles.features}>
            <div className="container">
              <div className="row">
                {features.map((props, idx) => (
                  <Feature key={idx} {...props} />
                ))}
              </div>
            </div>
          </section>
                )*/}
      </main>
    </Layout>
  );
}

export default Home;
