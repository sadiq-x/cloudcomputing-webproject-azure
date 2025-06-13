
export default {
  bootstrap: () => import('./main.server.mjs').then(m => m.default),
  inlineCriticalCss: true,
  baseHref: '/',
  locale: undefined,
  routes: [
  {
    "renderMode": 2,
    "redirectTo": "/cars",
    "route": "/"
  },
  {
    "renderMode": 2,
    "route": "/cars"
  }
],
  entryPointToBrowserMapping: undefined,
  assets: {
    'index.csr.html': {size: 433, hash: '600abfad3464c7002f7dc9d7a1bf0e768cabdb3f627e17bad88182923d66ef0c', text: () => import('./assets-chunks/index_csr_html.mjs').then(m => m.default)},
    'index.server.html': {size: 946, hash: '18aaf8fc9bab0c18121d9ae74b2db2dc4d6a0d39d19b1ff33ebb208a87eb48d7', text: () => import('./assets-chunks/index_server_html.mjs').then(m => m.default)},
    'cars/index.html': {size: 9680, hash: '577d01be6f86d2e3332c1a79ff1311cb2858c42c8f668f7ea28ee58a0b21098e', text: () => import('./assets-chunks/cars_index_html.mjs').then(m => m.default)},
    'styles-5INURTSO.css': {size: 0, hash: 'menYUTfbRu8', text: () => import('./assets-chunks/styles-5INURTSO_css.mjs').then(m => m.default)}
  },
};
