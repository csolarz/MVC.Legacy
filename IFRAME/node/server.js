const axios = require('axios');
const https = require('https');
const moment = require('moment');
const restify = require('restify');
const httpStatus = require('http-status');
const corsMiddleware = require('restify-cors-middleware');

const API_CURRENT_VERSION = '1.0.0'

const server = restify.createServer({
  name : `Node template api v:${API_CURRENT_VERSION}`,
  acceptable: 'application/json',
  rejectUnauthorized: true,
  ignoreTrailingSlash: true,
  handleUncaughtExceptions: true
});

server.pre(restify.pre.sanitizePath());
server.pre(versioning({ prefix: '/' }));

server.use(restify.plugins.gzipResponse());
server.use(restify.plugins.acceptParser(server.acceptable));
server.use(restify.plugins.queryParser());
server.use(restify.plugins.bodyParser({ mapParams: true }))

server.use(restify.plugins.throttle({
  burst: 100,
  rate: 50,
  ip: true
}));

server.get('/heroes', (req, res, next) => {
    res.setHeader('Content-Type', 'application/json');
    res.send(httpStatus.OK, ["Batman", "Ironman", "Goku"]);

    return next();
});

const validateOnLegacy = async (cookie) => {
    const agent = new https.Agent({
        rejectUnauthorized: false,
      });
    
      const response = await axios.get(
        `http://legacy-app.cl/session-validation`,
        { httpsAgent: agent }
      );
    
      if (response.status !== httpStatus.OK) {
        return undefined;
      }
}

const cors = corsMiddleware({
  preflightMaxAge: 5,
  origins: ['http://localhost:5002']
});

server.pre(cors.preflight);
server.use(cors.actual);

const port = process.env.PORT || 3011;

server.listen(port, "node-template.com", () => {
  console.log("Server startup", { version: API_CURRENT_VERSION, date: moment.utc().format() });
});
