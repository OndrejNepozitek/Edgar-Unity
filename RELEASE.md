### New
- upgrade version in package.json
- merge dev to master
- merge master to upm
- git tag v2.0.0-alpha.2
- git push origin v2.0.0-alpha.2
- wait for build to finish
- release should be created as draft, add changelog

### PRO
- git push pro PRO-v2.0.0-alpha.2

### Old
- export Unity package
- test exported package in different Unity project
- merge dev to master
- prepare new release
- tag and name v2.0.0-alpha.X
- create changelog
- attach unity package
- npm run version 2.0.0-alpha.1
- cmd /C "set "GIT_USER=OndrejNepozitek" && npm run deploy"
