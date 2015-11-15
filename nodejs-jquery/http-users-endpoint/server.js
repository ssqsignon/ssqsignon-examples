var express = require('express'),
    bodyParser = require('body-parser'),
    path = require('path'),
    passport = require('passport'),
    BasicStrategy = require('passport-http').BasicStrategy,
    port = 9903,
    users = [ { username: 'test1@users.com', password: 'testtest1', permissions: ['cat'] }, { username: 'test2@users.com', password: 'testtest2', permissions: ['cat', 'dog'] }],
    app = express();


passport.use(new BasicStrategy(
    function(userid, password, done) {
        return done(null, userid == 'example' && password == 'testtest');
    }
));

app.use(bodyParser.json());

app.post('/users', passport.authenticate('basic', { session: false }), function (req, res) {

    var user = findUser(req.body.username);

    if (user == null || (req.body.password && user.password != req.body.password)) {
        res.status(400).send('invalid_grant');
    } else {
        res.json({ id: user.username, scope: intersectScope(user, req.body.scope) });
    }
});

app.get('/', function (req, res) {
    res.send('SSQ signon example HTTP users endpoint.');
});

function findUser(username) {
    if (!username) {
        return null;
    }
    var match = users.filter(function(u) { return u.username == username });
    return match.length ? match[0] : null;
}

function intersectScope(user, requestedScope) {
    return (requestedScope ? requestedScope.split(' ').filter(function(p) { return user.permissions.indexOf(p) > -1; }) : user.permissions).join(' ');
}

app.listen(port);
console.log([ 'web app listening on port', port.toString() ].join(' '));

