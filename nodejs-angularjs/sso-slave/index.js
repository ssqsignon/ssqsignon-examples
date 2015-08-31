var express = require('express'),
    serveStatic = require('serve-static'),
    bodyParser = require('body-parser'),
    path = require('path'),
    passport = require('passport'),
    SsqSignonStrategy = require('passport-ssqsignon').Strategy,
    SsqSignonAuthProxy = require('ssqsignon-auth-proxy'),
    https = require('https'),
    port = 9902,
    ssqSignonConfig = require('./config.js').ssqSignon,
    app = express();

passport.use(new SsqSignonStrategy(ssqSignonConfig.moduleName, scopeAsObject));

app.use(bodyParser.json());

app.use('/auth', SsqSignonAuthProxy(ssqSignonConfig.moduleName, ssqSignonConfig.clientId, ssqSignonConfig.clientSecret));

app.get('/hamster', passport.authenticate('ssqsignon', { session: false }), function (req, res) {
    if (req.user.scope.hamster) {
        res.json({ message: 'Hello, I am the hamster!' });
    } else {
        res.status(403).send('insufficient_permissions');
    }
});


app.use('/app.js', serveStatic(path.join(__dirname, 'client', 'app.js')));
app.use('/config.js', serveStatic(path.join(__dirname, 'client', 'config.js')));
app.use('/hamster.jpg', serveStatic(path.join(__dirname, 'client', 'hamster.jpg')));

app.get('*', function (req, res) {
    res.sendFile(path.join(__dirname, 'client', 'index.html'));
});

function scopeAsObject(scopeStr) {
    return scopeStr.split(' ').reduce(function(result, s) { result[s] = true; return result; }, {});
}

app.listen(port);
console.log([ 'web app listening on port', port.toString() ].join(' '));
