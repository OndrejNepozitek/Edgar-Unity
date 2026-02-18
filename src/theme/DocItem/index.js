import React from 'react';
import DocItem from '@theme-original/DocItem';
import styles from './styles.module.css';

export default function DocItemWrapper(props) {
  return (
    <>
      {/* <div className={styles.saleBanner}>
        <div className={styles.saleBannerContent}>
          <span className={styles.saleBannerText}>
            🎉 Asset Store Sale: <strong>50% OFF</strong> on Edgar PRO!
          </span>
          <a 
            href="https://url.ondrejnepozitek.com/edgar-docs-sale" 
            target="_blank" 
            className={styles.saleBannerLink}
          >
            Buy Now →
          </a>
        </div>
      </div> */}
      <DocItem {...props} />
    </>
  );
}

