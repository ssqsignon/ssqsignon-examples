if (process.argv.length != 5) {
    console.log('server.js [your-server-name] [your-app-id] [your-app-secret]');
    process.exit(1);
}

var express = require('express'),
    serveStatic = require('serve-static'),
    bodyParser = require('body-parser'),
    path = require('path'),
    passport = require('passport'),
    SsqSignonStrategy = require('passport-ssqsignon').Strategy,
    SsqSignonProxy = require('ssqsignon-proxy-express'),
    https = require('https'),
    querystring = require('querystring'),
    port = 9902,
    serverName = process.argv[2],
    appId = process.argv[3],
    appSecret = process.argv[4],
    app = express();

passport.use(new SsqSignonStrategy(serverName, scopeAsObject));

app.use(bodyParser.json());

app.use('/auth', SsqSignonProxy(serverName, appId, appSecret));

app.get('/loginwithmaster', function(req, res) {
    var uri = [ 'http://localhost:9901', querystring.stringify({ redirect_uri: 'http://localhost:9902', client_id: appId, scope: 'hamster', state: req.query.state }) ].join('?');
    res.redirect(uri);
});


app.get('/hamster', passport.authenticate('ssqsignon', { session: false }), function (req, res) {
    if (req.user.scope.hamster) {
        res.json({ message: 'Hello, I am the hamster!' });
    } else {
        res.status(403).send('insufficient_permissions');
    }
});


app.use('/app.js', serveStatic(path.join(__dirname, 'client', 'app.js')));

app.get('*', function (req, res) {
    res.sendFile(path.join(__dirname, 'client', 'index.html'));
});

function scopeAsObject(scopeStr) {
    return scopeStr.split(' ').reduce(function(result, s) { result[s] = true; return result; }, {});
}

app.listen(port);
console.log([ 'web app listening on port', port.toString() ].join(' '));
