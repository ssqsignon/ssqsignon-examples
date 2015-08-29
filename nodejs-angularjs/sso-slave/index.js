var express = require('express'),
    serveStatic = require('serve-static'),
    bodyParser = require('body-parser'),
    path = require('path'),
    passport = require('passport'),
    SsqSignonStrategy = require('passport-ssqsignon').Strategy,
    https = require('https'),
    port = 9902,
    ssqSignonConfig = require('./config.js').ssqSignon,
    app = express();

passport.use(new SsqSignonStrategy(ssqSignonConfig.moduleName, scopeAsObject));

app.use(bodyParser.json());

app.post('/swapcode', function(req, res) {
    consumeAuthorizationCode(req.body.code, function(err, access) {
        if (err) {
            res.status(502).send(err);
        } else {
            res.send(access);
        }
    });
});

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

function consumeAuthorizationCode(code, done) {
    var module = ssqSignonConfig.moduleName,
        clientId = ssqSignonConfig.clientId,
        clientSecret = ssqSignonConfig.clientSecret,
        redirectUri = 'http://localhost:9902',
        data = JSON.stringify({ grant_type: 'authorization_code', code: code, redirect_uri: redirectUri, client_id: clientId });
    var req = https.request({
        method: 'POST',
        host: 'tinyusers.azurewebsites.net',
        path: [ '', module, 'auth' ].join('/'),
        auth: [ clientId, clientSecret ].join(':'),
        headers: { 'Content-Type': 'application/json', 'Content-Length': data.length }
    }, function(response) {
        var data = '';
        response.on('data', function(chunk) {
            data += chunk;
        });
        response.on('end', function() {
            var parsed = JSON.parse(data);
            if (response.statusCode == 200) {
                done(null, parsed);
            } else {
                done({ status: response.statusCode, reason: parsed }, null)
            }
        });
    }).on('error', function(e) {
        done({ status: null, reason: e });
    });
    req.write(data);
    req.end();
}

app.listen(port);
console.log([ 'web app listening on port', port.toString() ].join(' '));
