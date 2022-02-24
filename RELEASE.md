### Git

- upgrade version in package.json
- merge dev to master
- merge master to upm
- git tag v2.0.0-alpha.2
- git push origin v2.0.0-alpha.2
- wait for build to finish
- release should be created as draft, add changelog

### PRO

- git tag PRO-v2.0.0-alpha.2
- git push pro PRO-v2.0.0-alpha.2

### Online docs

- make sure to update code blocks with `npm run codeBlocks`
- npm run version 2.0.0-alpha.1
- `cmd /C "set "GIT_USER=OndrejNepozitek" && npm run deploy"`

### Offline docs

- make sure that the `next` version contains docs that should be published
- run the documentation locally `npm start`
- then run `npx mr-pdf --initialDocURLs="http://localhost:3000/Edgar-Unity/docs/next/offline/unity/,http://localhost:3000/Edgar-Unity/docs/next/basics/quickstart/" --contentSelector="article" --paginationSelector=".pagination-nav__item--next > a" --excludeSelectors=".margin-vert--xl a" --coverTitle="Edgar" --outputPDFFilename="documentation.pdf" --coverSub="Offline documentation for version 2.0.0"`
- the command above generates a `documentation.pdf` file