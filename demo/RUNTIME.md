# Node runtime and validation

The demo now requires current stable Node 26.10.0. Its engine range, root `.node-version`, development-container Node feature and existing demo deployment declarations are aligned with Node 26. The new `Node demo build and tests` job needs no Azure access or deployment credentials.

Run from `demo/src`: `npm ci`, `npm run check`, `npm test`. The smoke suite renders the real HTML using jsdom and checks the modern MongoDB collection API without a cloud connection. It does not claim to validate a live Cosmos account.

Existing Azure deployment workflows remain disabled where already disabled; this change does not enable or dispatch them. Before deploying, verify that the selected App Service environment provides Node 26 and that the database satisfies the driver's supported server contract. Repository runtime declarations are not proof of live Azure support.
